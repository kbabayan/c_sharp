using System;
using System.Diagnostics;
using lab4;

namespace lab4
{
    public enum Education { Specialist, Bachelor, SecondEducation }

    class Program
    {
        static void Main()
        {
            // 1.Создание двух коллекций StudentCollection
            StudentCollection collection1 = new StudentCollection("Collection 1");
            StudentCollection collection2 = new StudentCollection("Collection 2");

            // 2.Создание двух объектов Journal
            Journal journal1 = new Journal();
            Journal journal2 = new Journal();

            // Подписка на события
            collection1.StudentsCountChanged += journal1.StudentsCountChangedHandler;
            collection1.StudentReferenceChanged += journal1.StudentReferenceChangedHandler;

            collection2.StudentReferenceChanged += journal1.StudentReferenceChangedHandler;
            collection2.StudentReferenceChanged += journal2.StudentReferenceChangedHandler;

            // 3.Внесение изменений в коллекции
            collection1.AddDefaults(3);
            collection2.AddDefaults(5);

            collection1.Remove(1);
            collection2.Remove(2);

            collection1[0] = new Student();
            collection2[3] = new Student();

            Console.WriteLine("Journal 1 Entries:");
            Console.WriteLine(journal1.ToString());

            Console.WriteLine("Journal 2 Entries:");
            Console.WriteLine(journal2.ToString());
        }
    }
}
