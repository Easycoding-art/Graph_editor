#pragma once
//#ifndef MY_CLASS_H // include guard
#define MY_CLASS_H
#include <iostream>
using namespace std;
template<typename T> class Sequence
{
private:
    T* arr = NULL;
    int sequense_size = 0;
public:
    Sequence() {
        arr = new T[0];
        sequense_size = 0;
    }

    Sequence(T* array, int size) {
        arr = new T[size];
        sequense_size = size;
        for (int i = 0; i < size; i++)
        {
            arr[i] = array[i];
        }
    }


    int size() const {
        return sequense_size;
    }

    T peek(int count) const {
        return arr[count];
    }

   Sequence<T> swap(int i, int j) {
        T a = arr[i];
        T b = arr[j];
        T* array = new T[sequense_size];
        for (int n = 0; n < sequense_size; n++) {
            if (n != i && n != j) {
                array[n] = arr[n];
            }
            if (n == i) {
                array[n] = b;
            }
            if (n == j) {
                array[n] = a;
            }
        }
        Sequence sequence = Sequence(array, sequense_size);
        delete[] array;
        return sequence;
    }

    int get_index(T element) {
       for (int n = 0; n < sequense_size; n++) {
           if (arr[n] == element) {
               return n;
           }
       }
   }

   Sequence<T> sort() {
        Sequence sequence = *this;
        for  (int i = 0; i < sequense_size - 1; i++) {
            if (sequence.peek(i) > sequence.peek(i+1)) {
                sequence.swap(i, i + 1);
            }
        }
        return sequence;
    }

   Sequence<T> pop() {
        T* array = new T[sequense_size - 1];
        for (int i = 0; i < sequense_size - 1; i++)
        {
            array[i] = arr[i];
        }
        Sequence sequence = Sequence(array, sequense_size - 1);
        delete[] array;
        return sequence;
    }

   Sequence<T> add(T value) {
        T* result = new T[sequense_size + 1];
        for (int i = 0; i < sequense_size; i++) {
            result[i] = arr[i];
        }
        result[sequense_size] = value;
        Sequence sequence = Sequence(result, sequense_size + 1);
        delete[] result;
        return sequence;
    }
    void erase() {
        arr = (T*)realloc(arr, 1);
    }

   Sequence<T> where(bool (*func)(T)) {
        Sequence result = Sequence();
        for (int n = 0; n < sequense_size; n++) {
            if (func(arr[n]) == true) {
                result = result.add(arr[n]);
            }
        }
        return result;
    }

    template<typename t>
    Sequence<t> map(t(*func)(T)) {
        Sequence result = Sequence();
        for (int n = 0; n < sequense_size; n++) {
            result = result.add(func(arr[n]));
        }
        return result;
    }

    bool contains(T value) {
        bool result = false;
        for (int i = 0; i < sequense_size; i++) {
            if (arr[i] == value) {
                result = true;
                break;
            }
            
        }
        return result;
    }

    friend bool operator == (const Sequence<T>& seq1, const Sequence<T>& seq2) {
        bool result = true;
        if (seq1.size() == seq2.size()) {
            for (int i = 0; i < seq1.size(); i++)
            {
                if (seq1.peek(i) != seq2.peek(i)) {
                    result = false;
                    break;
                }
            }
        }
        else {
            result = false;
        }
        return result;
    };
    friend bool operator >=(const Sequence<T>& seq1, const Sequence<T>& seq2) {
        return seq1.size() >= seq2.size();
    };

    friend bool operator !=(const Sequence<T>& seq1, const Sequence<T>& seq2) {
        bool result = false;
        if (seq1.size() == seq2.size()) {
            for (int i = 0; i < seq1.size(); i++)
            {
                if (seq1.peek(i) != seq2.peek(i)) {
                    result = true;
                    break;
                }
            }
        }
        else {
            result = true;
        }
        return result;
    };
    friend bool operator <=(const Sequence<T>& seq1, const Sequence<T>& seq2) {
        return seq1.size() <= seq2.size();
    };

    friend bool operator >(const Sequence<T>& seq1, const Sequence<T>& seq2) {
        return seq1.size() > seq2.size();
    };

    friend bool operator <(const Sequence<T>& seq1, const Sequence<T>& seq2) {
        return seq1.size() < seq2.size();
    };
};