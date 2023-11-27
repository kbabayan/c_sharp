using System;
using System.Collections.Generic;

public class Matrix
{
    private int width;  // ширина матрицы
    private int height; // высота матрицы
    private List<List<int>> data;

    // Конструктор по умолчанию
    public Matrix(int width, int height)
    {
        this.width = width;
        this.height = height;
        InitializeData();
    }

    // Конструктор для пользовательской инициализации
    public Matrix(List<int> initData)
    {
        int size = initData.Count;
        this.width = size;
        this.height = size;
        InitializeData();

        for (int h = 0; h < size; ++h)
        {
            for (int w = 0; w < size; ++w)
            {
                data[h][w] = initData[w] - h;
            }
        }
    }

    // Конструктор копирования
    public Matrix(Matrix other)
    {
        this.width = other.width;
        this.height = other.height;
        this.data = new List<List<int>>(other.data);
    }

    // Метод для решения системы линейных уравнений
    public void SolveSystem(List<double> rhs, List<double> solution)
    {
        if (rhs.Count != height || solution.Count != height)
        {
            Console.WriteLine("Error: Input sizes do not match matrix dimensions.");
            return;
        }

        List<double> alpha = new List<double>(new double[height]);
        List<double> beta = new List<double>(new double[height]);
        List<double> gamma = new List<double>(new double[height]);

        // Прямой ход алгоритма Левинсона
        alpha[0] = data[0][0];
        beta[0] = data[0][1] / alpha[0];
        gamma[0] = rhs[0] / alpha[0];

        for (int i = 1; i < height; ++i)
        {
            double sum = 0.0;
            for (int j = 0; j < i; ++j)
            {
                sum += data[i][j] * beta[j];
            }

            alpha[i] = data[i][i] - sum;
            beta[i] = data[i][i + 1] / alpha[i];
            gamma[i] = (rhs[i] - sum) / alpha[i];
        }

        // Обратный ход алгоритма Левинсона
        solution[height - 1] = gamma[height - 1];

        for (int i = height - 2; i >= 0; --i)
        {
            solution[i] = gamma[i] - beta[i] * solution[i + 1];
        }
    }

    public override string ToString()
    {
        string matrixString = "";
        for (int h = 0; h < height; ++h)
        {
            for (int w = 0; w < width; ++w)
            {
                matrixString += data[h][w] + "\t";
            }
            matrixString += "\n";
        }
        return matrixString;
    }

    // Приватный метод для инициализации данных
    private void InitializeData()
    {
        data = new List<List<int>>(height);
        for (int h = 0; h < height; ++h)
        {
            data.Add(new List<int>(width));
            for (int w = 0; w < width; ++w)
            {
                data[h].Add(0);
            }
        }

        // Заполнение данных (по Тёплицу)
        for (int h = 0; h < height; ++h)
        {
            for (int w = 0; w < width; ++w)
            {
                data[h][w] = height - h + w;
            }
        }
    }
}
