using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace lab6
{
    class Program
    {
        //// Импорт методов из DLL
        //[DllImport("MatrixLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IntPtr CreateMatrix(int n);

        //[DllImport("MatrixLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern void SolveSystem(IntPtr matrixPtr, double[] rhs, double[] solution, int size);

        //[DllImport("MatrixLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern void DeleteMatrix(IntPtr matrixPtr);

        static void TestCSharpSolver()
        {
            // Создание матрицы и вектора правой части
            Matrix matrixCSharp = new Matrix(3);
            double[] rhsCSharp = { 1, 2, 3 };

            // Решение системы уравнений
            double[] solutionCSharp = matrixCSharp.SolveSystem(rhsCSharp);

            // Вывод результатов
            Console.WriteLine("Matrix (C#):");
            // Вывод матрицы
            Console.WriteLine("Right-hand side (C#):");
            Console.WriteLine("Solution (C#):");
        }

        static void TestCppIntegration()
        {
            // Создание матрицы и вектора правой части
            Matrix matrixCpp = new Matrix(3);
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

        static void WorkWithTimesList(TimesList timesList)
        {
            Console.Write("Enter the filename: ");
            string filename = Console.ReadLine();

            // Загрузка данных из файла, если файл существует
            if (File.Exists(filename))
            {
                timesList.Load(filename);
                timesList.Print();
            }

            do
            {
                // Ввод порядка матрицы и числа повторов
                Console.Write("Enter the matrix order (or 'exit' to finish): ");
                string inputOrder = Console.ReadLine();

                if (inputOrder.ToLower() == "exit")
                {
                    break;
                }

                if (int.TryParse(inputOrder, out int matrixOrder))
                {
                    Console.Write("Enter the repeat count: ");
                    int repeatCount = int.Parse(Console.ReadLine());

                    // Шаг 5: Выполнение вычислений на C# и C++
                    double cSharpExecutionTime = 0; // Вычисление времени выполнения на C#
                    double cppExecutionTime = 0;   // Вычисление времени выполнения на C++

                    for (int i = 0; i < repeatCount; i++)
                    {
                        // Засечка времени перед выполнением
                        // Вычисления на C#
                        // Засечка времени после выполнения

                        // Засечка времени перед выполнением
                        // Вызов экспортируемой функции из DLL-библиотеки C++
                        // Засечка времени после выполнения
                    }

                    // Сохранение результатов в объекте TimeItem
                    TimeItem item = new TimeItem
                    {
                        MatrixOrder = matrixOrder,
                        RepeatCount = repeatCount,
                        CSharpExecutionTime = cSharpExecutionTime,
                        CppExecutionTime = cppExecutionTime,
                        ExecutionTimeRatio = cSharpExecutionTime / cppExecutionTime
                    };

                    // Шаг 6: Добавление объекта TimeItem в TimesList
                    timesList.Add(item);

                    // Вывод данных на экран
                    timesList.Print();
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid matrix order.");
                }
            } while (true);

            // Сохранение данных в файл при завершении
            timesList.Save(filename);
        }

        static void Main(string[] args)
        {
            try
            {
                // Шаг 1: Проверка метода решения системы линейных уравнений в C#
                Console.WriteLine("Step 1:");
                TestCSharpSolver();

                // Шаг 2: Передача данных в код C++
                Console.WriteLine("\nStep 2:");
                TestCppIntegration();

                // Шаг 3-7: Работа с TimesList
                Console.WriteLine("\nSteps 3-7:");
                TimesList timesList = new TimesList();
                WorkWithTimesList(timesList);

                // Шаг 8: Обработка исключений
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}