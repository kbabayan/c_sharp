using System;
using System.Diagnostics;
using lab3;

namespace lab3
{
    public enum Education { Specialist, Bachelor, SecondEducation }

    class Program
    {
        static void Main(string[] args)
        {
            // 1. Создание объекта StudentCollection и добавление студентов.
            Console.WriteLine("--------------------------1------------------------");
            StudentCollection studentCollection = new StudentCollection();

            Person p1 = new Person("Oleg", "Ivanov", new DateTime(2001, 02, 11));
            Student st1 = new Student(p1, Education.Bachelor, 23);
            st1.AddExams(new Exam("Math", 5, new DateTime(2023, 08, 13)), new Exam("Physics", 5, new DateTime(2023, 08, 10)));
            st1.Tests.Add(new Test("Math", true));
            st1.Tests.Add(new Test("Philosophy", true));

            Person p2 = new Person("Daniil", "Kozlov", new DateTime(2000, 08, 13));
            Student st2 = new Student(p2, Education.Bachelor, 35);
            st2.AddExams(new Exam("Math", 4, new DateTime(2023, 06, 1)), new Exam("Physics", 3, new DateTime(2023, 06, 5)));
            st2.Tests.Add(new Test("Math", true));
            st2.Tests.Add(new Test("TFKP", false));

            Person p3 = new Person("Evgeniy", "Alexeev", new DateTime(2002, 10, 9));
            Student st3 = new Student(p3, Education.Specialist, 11);
            st3.AddExams(new Exam("Math", 3, new DateTime(2023, 10, 4)), new Exam("OOP", 5, new DateTime(2023, 10, 19)));
            st3.Tests.Add(new Test("Math", true));
            st3.Tests.Add(new Test("History", true));

            Person p4 = new Person("Konstantin", "Babayan", new DateTime(2004, 10, 19));
            Student st4 = new Student(p4, Education.Bachelor, 22);
            st4.AddExams(new Exam("Math", 5, new DateTime(2023, 10, 4)), new Exam("OOP", 5, new DateTime(2023, 10, 6)));
            st4.Tests.Add(new Test("Math", true));
            st4.Tests.Add(new Test("Philosophy", true));

            Person p5 = new Person("Zachar", "Zalandin", new DateTime(2004, 11, 16));
            Student st5 = new Student(p5, Education.Specialist, 23);
            st5.AddExams(new Exam("Math", 4, new DateTime(2023, 10, 4)), new Exam("OOP", 4, new DateTime(2023, 10, 19)));
            st5.Tests.Add(new Test("Math", true));
            st5.Tests.Add(new Test("History", true));

            studentCollection.AddStudents(st1, st2, st3, st4, st5); // Добавление 5 студентов по умолчанию.
            Console.WriteLine("StudentCollection после добавления студентов:\n");
            Console.WriteLine(studentCollection.ToString());

            // 2. Выполнение сортировки и вывод данных после каждой сортировки.
            Console.WriteLine("--------------------------2------------------------");
            studentCollection.SortBySurname();
            Console.WriteLine("StudentCollection после сортировки по фамилии:\n");
            Console.WriteLine(studentCollection.ToString());

            studentCollection.SortByDateOfBirth();
            Console.WriteLine("StudentCollection после сортировки по дате рождения:\n");
            Console.WriteLine(studentCollection.ToString());

            studentCollection.SortByAverageScore();
            Console.WriteLine("StudentCollection после сортировки по среднему баллу:\n");
            Console.WriteLine(studentCollection.ToString());

            // 3. Выполнение операций и вывод результатов.
            Console.WriteLine("--------------------------3------------------------");
            double maxAvgMark = studentCollection.MaxAverageMark;
            Console.WriteLine($"Максимальный средний балл в StudentCollection: {maxAvgMark:0.00} \n");

            var specialistStudents = studentCollection.SpecialistStudents;
            Console.WriteLine("Студенты со специалистической формой обучения: \n");
            foreach (var student in specialistStudents)
            {
                Console.WriteLine(student.ToShortString());
            }

            var groupedStudents = studentCollection.AverageMarkGroup(5.0);
            Console.WriteLine("Группы студентов по среднему баллу: \n");
            foreach (var group in groupedStudents)
            {
                Console.WriteLine($"Средний балл {group.AvgRate:0.00}:\n{group.ToShortString()}");
            }

            // 4. Создание объекта TestCollections и измерение времени поиска.
            Console.WriteLine("--------------------------4------------------------");
            TestCollections testCollections = new TestCollections(1000);

            Console.WriteLine(testCollections);
            //Console.WriteLine(studentCollection.Students[5]);

            // Поиск первого элемента
            Person firstPerson = studentCollection.Students[0].Person;
            TimeSpan searchTime1 = testCollections.SearchInPersonList(firstPerson);
            Console.WriteLine($"Время поиска первого элемента в List<Person>: {searchTime1}");

            // Поиск центрального элемента
            Person centralPerson = studentCollection.Students[2].Person;
            TimeSpan searchTime2 = testCollections.SearchInPersonList(centralPerson);
            Console.WriteLine($"Время поиска центрального элемента в List<Person>: {searchTime2}");

            // Поиск последнего элемента
            Person lastPerson = studentCollection.Students[4].Person;
            TimeSpan searchTime3 = testCollections.SearchInPersonList(lastPerson);
            Console.WriteLine($"Время поиска последнего элемента в List<Person>: {searchTime3}");

            // Поиск элемента, не входящего в коллекцию
            Person nonExistentPerson = new Person("Nonexistent", "Person", DateTime.Now);
            TimeSpan searchTime4 = testCollections.SearchInPersonList(nonExistentPerson);
            Console.WriteLine($"Время поиска элемента, не входящего в List<Person>: {searchTime4}");

        }
    }
}
