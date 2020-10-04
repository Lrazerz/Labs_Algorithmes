//
// Created by yuriy on 17.07.20.
//

#ifndef CONTAINERS_ARRAY_H
#define CONTAINERS_ARRAY_H

#include <stdexcept>
#include <iterator>

namespace std_c {
    template<typename T, std::size_t N>
    struct array {
        using value_type = T;
        using difference_type = std::ptrdiff_t;
        using size_type = std::size_t;
        using reference = T &;
        using pointer = T *;
        using iterator = T *;
        using const_reference = const T &;
        using const_pointer = const T *;
        using const_iterator = const T *;
        using reverse_iterator = std::reverse_iterator<iterator>;
        using const_reverse_iterator = std::reverse_iterator<const_iterator>;

        value_type _head[N];

        //Element access
        constexpr reference at(size_type pos) {
            if (pos < N) [[likely]] {
                return _head[pos];
            }
            throw std::out_of_range("array::at");
        }

        constexpr reference operator[](size_type pos) {
            return _head[pos];
        }

        constexpr reference front() {
            return _head[0];
        }

        constexpr reference back() {
            return _head[N - 1];
        }

        constexpr pointer data() noexcept {
            return _head;
        }

        constexpr const_pointer data() const noexcept {
            return _head;
        }


        //Iterators
        constexpr iterator begin() noexcept {
            return iterator(_head);
        }

        constexpr const_iterator begin() const noexcept {
            return const_iterator(_head);
        }

        constexpr const_iterator cbegin() const noexcept {
            return const_iterator(_head);
        }

        constexpr iterator end() noexcept {
            return iterator(_head + N);
        }

        constexpr const_iterator end() const noexcept {
            return const_iterator(_head + N);
        }

        constexpr const_iterator cend() const noexcept {
            return const_iterator(_head + N);
        }

        constexpr reverse_iterator rbegin() noexcept {
            return reverse_iterator(_head + N - 1);
        }

        constexpr const_reverse_iterator rbegin() const noexcept {
            return const_reverse_iterator(_head + N - 1);
        }

        constexpr const_reverse_iterator crbegin() const noexcept {
            return const_reverse_iterator(_head + N - 1);
        }

        constexpr reverse_iterator rend() noexcept {
            return reverse_iterator(_head - 1);
        }

        constexpr const_reverse_iterator rend() const noexcept {
            return const_reverse_iterator(_head - 1);
        }

        constexpr const_reverse_iterator crend() const noexcept {
            return const_reverse_iterator(_head - 1);
        }


        //Capacity
        [[nodiscard]]
        constexpr bool empty() const noexcept {
            return N == 0;
        }

        [[nodiscard]]
        constexpr size_type size() const noexcept {
            return N;
        }

        [[nodiscard]]
        constexpr size_type max_size() const noexcept {
            return N;
        }

        //Operations
        constexpr void fill(const T &value) {
            for (auto &item = begin(); item != end(); ++item) {
                *item = value;
            }
        }

        constexpr void swap(array &other) noexcept(std::is_nothrow_swappable_v<T>) {
            std::swap_ranges(data(), data() + N, other.data());
        }
    };


    template<typename T, std::size_t N>
    void swap(array<T, N> a, array<T, N> b) noexcept(noexcept(a.swap(b))) {
        a.swap(b);
    }
}

#endif //CONTAINERS_ARRAY_H
