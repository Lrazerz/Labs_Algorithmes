//
// Created by yuriy on 28.07.20.
//

#include <cassert>
#include "test_array.h"
#include "../../src/array.h"

namespace array_test {

    namespace {

        void deduct() {
#ifdef ENABLE_COMPILATION_ERRORS
            {
                std_c::array arr{1,2,3};  // array(T, U...)
                static_assert(std::is_same_v<decltype(arr), std::array<int, 3>>, "");
                assert(arr[0] == 1);
                assert(arr[1] == 2);
                assert(arr[2] == 3);
            }
            {
                constexpr long l1 = 42;
                std_c::array arr{1L, 4L, 9L, l1}; // array(T, U...)
                static_assert(std::is_same_v<decltype(arr)::value_type, long>, "");
                static_assert(arr.size() == 4, "");
                assert(arr[0] == 1);
                assert(arr[1] == 4);
                assert(arr[2] == 9);
                assert(arr[3] == l1);
            }
#endif // ENABLE_COMPILATION_ERRORS
            {
                std_c::array<double, 2> source = {4.0, 5.0};
                std_c::array arr(source);   // array(array)
                static_assert(std::is_same_v<decltype(arr), decltype(source)>);
                static_assert(std::is_same_v<decltype(arr), std_c::array<double, 2>>);
                assert(arr[0] == 4.0);
                assert(arr[1] == 5.0);
            }
        }


        void implicit_copy(){
            struct NoDefault {
                NoDefault(int) { }
            };

            struct NonTrivialCopy {
                NonTrivialCopy() { }
                NonTrivialCopy(NonTrivialCopy const&) { }
                NonTrivialCopy& operator=(NonTrivialCopy const&) { return *this; }
            };

            {
                typedef std_c::array<double, 3> Array;
                Array array = {1.1, 2.2, 3.3};
                Array copy = array;
                copy = array;
                static_assert(std::is_copy_constructible<Array>::value);
                static_assert(std::is_copy_assignable<Array>::value);
            }
            {
                typedef std_c::array<double, 0> Array;
                Array array = {};
                Array copy = array;
                copy = array;
                static_assert(std::is_copy_constructible<Array>::value, "");
                static_assert(std::is_copy_assignable<Array>::value, "");
            }
#ifdef ENABLE_COMPILATION_ERRORS
            {
                typedef std_c::array<double const, 3> Array;
                Array array = {1.1, 2.2, 3.3};
                Array copy = array; (void)copy;
                static_assert(std::is_copy_constructible<Array>::value, "");
                TEST_NOT_COPY_ASSIGNABLE(Array);
            }
            {
                // const arrays of size 0 should disable the implicit copy assignment operator.
                typedef std_c::array<double const, 0> Array;
                Array array = {};
                Array copy = array; (void)copy;
                static_assert(std::is_copy_constructible<Array>::value, "");
                TEST_NOT_COPY_ASSIGNABLE(Array);
            }
                        {
                typedef std_c::array<NoDefault const, 0> Array;
                Array array = {};
                Array copy = array; (void)copy;
                static_assert(std::is_copy_constructible<Array>::value, "");
                TEST_NOT_COPY_ASSIGNABLE(Array);
            }
#endif //ENABLE_COMPILATION_ERRORS
            {
                typedef std_c::array<NoDefault, 0> Array;
                Array array = {};
                Array copy = array;
                copy = array;
                static_assert(std::is_copy_constructible<Array>::value, "");
                static_assert(std::is_copy_assignable<Array>::value, "");
            }

            // Make sure we can implicitly copy a std::array of a non-trivially copyable type
            {
                typedef std_c::array<NonTrivialCopy, 0> Array;
                Array array = {};
                Array copy = array;
                copy = array;
                static_assert(std::is_copy_constructible<Array>::value, "");
            }
            {
                typedef std_c::array<NonTrivialCopy, 1> Array;
                Array array = {};
                Array copy = array;
                copy = array;
                static_assert(std::is_copy_constructible<Array>::value, "");
            }
            {
                typedef std_c::array<NonTrivialCopy, 2> Array;
                Array array = {};
                Array copy = array;
                copy = array;
                static_assert(std::is_copy_constructible<Array>::value, "");
            }

        }
    }

    void run() {
        deduct();
        implicit_copy();
    }
}
