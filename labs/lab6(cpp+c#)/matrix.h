// #pragma once
#include <vector>

class Matrix {
public:
    Matrix(int width, int height);  // Конструктор по умолчанию
    Matrix(const std::vector<int>& data);  // Конструктор для пользовательской инициализации
    Matrix(const Matrix& other);  // Конструктор копирования
    ~Matrix();  // Деструктор

    Matrix& operator=(const Matrix& other);  // Операция присваивания

    void SolveSystem(const std::vector<double>& rhs, std::vector<double>& solution) const;  // Метод для решения системы линейных уравнений

private:
    int width;  // ширина матрицы
    int height; // высота матрицы
    std::vector<std::vector<int> > data;
};
