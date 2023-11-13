using System;

namespace lab4
{
    public interface IDateAndCopy
    {
        object DeepCopy { get; }
        DateTime Date { get; set; }
    }
}