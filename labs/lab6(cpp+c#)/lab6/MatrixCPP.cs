using System;
using System.Runtime.InteropServices;

namespace lab6
{
	public class MatrixCPP
	{
        // Здесь указывается имя DLL и используемая конвенция вызова (Calling Convention)
        const string MatrixLibraryDll = "Matrix.dll";

        // Ваши методы из C++ класса Matrix
        [DllImport(MatrixLibraryDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreateMatrix(int width, int height);

        //[DllImport(MatrixLibraryDll, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void DestroyMatrix(IntPtr matrix);

        [DllImport(MatrixLibraryDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SolveSystem(IntPtr matrix, double[] rhs, double[] solution);
    }
}

