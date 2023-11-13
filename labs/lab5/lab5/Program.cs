using System.IO;
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
            Console.WriteLine("---------------1--------------");
            Student student = new Student();
            Student copyOfStudent = student.DeepCopy();
            Console.WriteLine("Original student:");
            Console.WriteLine(student.ToString());
            Console.WriteLine("Copy of the student:");
            Console.WriteLine(copyOfStudent.ToString());

            Console.WriteLine("---------------2--------------");
            Console.WriteLine("Enter the filename to load or create:");
            string filename = Console.ReadLine();

            if (!File.Exists(filename))
            {
                File.Create(filename).Close();
            }
            if (student.Load(filename))
            {
                Console.WriteLine("Student loaded successfully from the file.");
                Console.WriteLine(student.ToString());
            }
            else
            {
                Console.WriteLine("Error loading student from the file.");
            }

            Console.WriteLine("---------------3--------------");
            Console.WriteLine("Current student after loading:");
            Console.WriteLine(student.ToString());

            Console.WriteLine("---------------4--------------");
            if (student.AddFromConsole())
            {
                if (student.Save(filename))
                {
                    Console.WriteLine("Student saved successfully to the file.");
                }
                else
                {
                    Console.WriteLine("Error saving student to the file.");
                }
            }

            Console.WriteLine("Current student after adding and saving:");
            Console.WriteLine(student.ToString());

            Console.WriteLine("---------------5,6--------------");
            Student loadedStudent = new Student();
            if (loadedStudent.Load(filename))
            {
                if (loadedStudent.AddFromConsole())
                {
                    if (loadedStudent.Save(filename))
                    {
                        Console.WriteLine("Student saved successfully to the file.");
                    }
                    else
                    {
                        Console.WriteLine("Error saving student to the file.");
                    }
                }
                Console.WriteLine("Current student after loading, adding, and saving:");
                Console.WriteLine(loadedStudent.ToString());
            }
        }
    }
}
