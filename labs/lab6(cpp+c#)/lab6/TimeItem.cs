using System;
namespace lab6
{
    [Serializable]
    public class TimeItem
	{
        public int MatrixOrder { get; set; }    // порядок матрицы
        public int RepeatCount { get; set; }    // число повторов
        public double CSharpExecutionTime { get; set; }     // время выполнения c# кода
        public double CppExecutionTime { get; set; }    // время выполнения c++ rjlf
        public double ExecutionTimeRatio { get; set; }      // коэффицент
    }
}