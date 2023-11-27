using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Проверка метода решения системы линейных уравнений в C#
            TestCSharpSolution();

            // 2. Передача данных в код C++ и вывод полученного решения
            TestCppSolution();

            // 3. Создание объекта TimesList и загрузка/вывод данных из файла
            TimesList timesList = new TimesList();
            Console.Write("Enter the file name: ");
            string fileName = Console.ReadLine();
            LoadDataFromFile(timesList, fileName);

            // 4-7. Вычисления на C# и вызов C++ функции, сохранение результатов в TimesList
            CollectData(timesList);

            // 8. Вывод всей коллекции и сохранение в файл
            PrintAndSaveData(timesList, fileName);
        }

        static void TestCSharpSolution()
        {
            // Реализуйте проверку метода решения системы линейных уравнений в C#
        }

        static void TestCppSolution()
        {
            // Реализуйте передачу данных в код C++ и вывод полученного решения
        }

        static void TestCSharpSolver()
        {
            // Создание матрицы и вектора правой части
            //Matrix matrixCSharp = new Matrix(3);
            //double[] rhsCSharp = { 1, 2, 3 };

            Matrix matrixCSharp = new Matrix(3, 3);
            List<double> rhsCSharp = new List<double> { 1, 2, 3 };
            List<double> solutionCSharp = new List<double> { };


            // Решение системы уравнений
            solutionCSharp = new matrixCSharp.SolveSystem(rhsCSharp, solutionCSharp);

            // Вывод результатов
            Console.WriteLine("Solution (C#):");
            Console.WriteLine(solutionCSharp);
        }

        static void TestCppIntegration()
        {
            // Создание матрицы и вектора правой части
            MatrixCPP matrixCpp = new CreateMatrix(3, 3);
            double[] rhsCpp = { 1, 2, 3 };

            // Вызов метода из DLL-библиотеки C++
            double[] solutionCpp = new double[rhsCpp.Length];
            SolveSystem(matrixCpp.MatrixPtr, rhsCpp, solutionCpp, rhsCpp.Length);

            // Вывод результатов
            Console.WriteLine("Matrix (C++):");
            // Вывод матрицы
            Console.WriteLine("Right-hand side (C++):");
            Console.WriteLine("Solution (C++):");
        }

        static void LoadDataFromFile(TimesList timesList, string fileName)
        {
            try
            {
                timesList.Load(fileName);
                timesList.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data from file: {ex.Message}");
            }
        }

        static void CollectData(TimesList timesList)
        {
            bool continueCollecting = true;

            while (continueCollecting)
            {
                try
                {
                    Console.Write("Enter the matrix order (or 'exit' to finish): ");
                    string input = Console.ReadLine();

                    if (input.ToLower() == "exit")
                    {
                        continueCollecting = false;
                    }
                    else
                    {
                        int matrixOrder = int.Parse(input);
                        Console.Write("Enter the repeat count: ");
                        int repeatCount = int.Parse(Console.ReadLine());

                        // Выполнение вычислений на C#
                        // Вызов экспортируемой функции из DLL-библиотеки C++

                        // Сохранение результатов в TimesList
                        TimeItem timeItem = new TimeItem
                        {
                            MatrixOrder = matrixOrder,
                            RepeatCount = repeatCount,
                            // Установите остальные значения времени и коэффициента
                        };

                        timesList.Add(timeItem);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static void PrintAndSaveData(TimesList timesList, string fileName)
        {
            // Вывод всей коллекции
            timesList.Print();

            // Сериализация и сохранение в файл
            try
            {
                timesList.Save(fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data to file: {ex.Message}");
            }
        }
    }
}
