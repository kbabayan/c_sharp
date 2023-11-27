#include "matrix.h"
#include <iostream>
#include <vector>
#include <cmath>

using namespace std;

Matrix::Matrix(int width, int height) {
    data.resize(height, vector<int>(width, 0));

    for (int h = 0; h < height; ++h) {
        for (int w = 0; w < width; ++w) {
            data[h][w] = height - h + w;
        }
    }
}

Matrix::Matrix(const vector<int>& initData) {
    data.resize(initData.size(), vector<int>(initData.size(), 0));

    for (int h = 0; h < initData.size(); ++h) {
        for (int w = 0; w < initData.size(); ++w) {
            data[h][w] = initData[w] - h;
        }
    }
}

Matrix::Matrix(const Matrix& other) {
    width = other.width;
    height = other.height;
    data = other.data;  // Используем конструктор копирования вектора
}

Matrix::~Matrix() {
    // Деструктор
    // В данном случае, нет необходимости в явном освобождении ресурсов,
    // так как векторы будут автоматически уничтожены при выходе из области видимости
}

Matrix& Matrix::operator=(const Matrix& other) {
    if (this != &other) {
        width = other.width;
        height = other.height;
        data = other.data;  // Используем оператор присваивания вектора
    }
    return *this;
}

void Matrix::SolveSystem(const vector<double>& rhs, vector<double>& solution) const {
    if (rhs.size() != height || solution.size() != height) {
        cerr << "Error: Input sizes do not match matrix dimensions." << endl;
        return;
    }

    vector<double> alpha(height, 0.0);
    vector<double> beta(height, 0.0);
    vector<double> gamma(height, 0.0);

    // Прямой ход алгоритма Левинсона
    alpha[0] = data[0][0];
    beta[0] = data[0][1] / alpha[0];
    gamma[0] = rhs[0] / alpha[0];

    for (int i = 1; i < height; ++i) {
        double sum = 0.0;
        for (int j = 0; j < i; ++j) {
            sum += data[i][j] * beta[j];
        }

        alpha[i] = data[i][i] - sum;
        beta[i] = data[i][i + 1] / alpha[i];
        gamma[i] = (rhs[i] - sum) / alpha[i];
    }

    // Обратный ход алгоритма Левинсона
    solution[height - 1] = gamma[height - 1];

    for (int i = height - 2; i >= 0; --i) {
        solution[i] = gamma[i] - beta[i] * solution[i + 1];
    }
}
