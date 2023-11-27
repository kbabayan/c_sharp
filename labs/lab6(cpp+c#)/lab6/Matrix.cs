using System;
using System.Runtime.InteropServices;

public class Matrix
{
    private IntPtr matrixPtr;  // Указатель на объект C++

    // Конструктор по умолчанию
    public Matrix(int n)
    {
        matrixPtr = MatrixInterop.CreateMatrix(n);
    }

    // Конструктор для пользовательской инициализации
    public Matrix(double[] initData)
    {
        matrixPtr = MatrixInterop.CreateMatrixWithData(initData, initData.Length);
    }

    // Метод для решения системы линейных уравнений
    public double[] SolveSystem(double[] rhs)
    {
        double[] solution = new double[rhs.Length];
        MatrixInterop.SolveSystem(matrixPtr, rhs, solution, rhs.Length);
        return solution;
    }

    // Деструктор
    ~Matrix()
    {
        MatrixInterop.DeleteMatrix(matrixPtr);
    }
}

internal static class MatrixInterop
{
    const string dllName = "MatrixLibrary.dll";  // Имя DLL-библиотеки

    // Импорт методов из DLL
    [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr CreateMatrix(int n);

    [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr CreateMatrixWithData(double[] data, int size);

    [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SolveSystem(IntPtr matrixPtr, double[] rhs, double[] solution, int size);

    [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void DeleteMatrix(IntPtr matrixPtr);
}
