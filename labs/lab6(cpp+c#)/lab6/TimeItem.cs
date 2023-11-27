using System;
namespace lab6
{
    [Serializable]
    public class TimeItem
	{
        public int MatrixOrder { get; set; }
        public int RepeatCount { get; set; }
        public double CSharpExecutionTime { get; set; }
        public double CppExecutionTime { get; set; }
        public double ExecutionTimeRatio { get; set; }
    }
}