using System;
using lab1;

namespace lab1
{
    public enum Education { Specialist, Bachelor, SecondEducation }

    class Program
    {
        static void Main(string[] args)
        {
            //1.
            Console.WriteLine("..................1..................");

            // Создаём два объекта типа Person с совпадающими данными
            Person person1 = new Person("John", "Doe", new DateTime(1990, 5, 15));
            Person person2 = new Person("John", "Doe", new DateTime(1990, 5, 15));

            // Проверяем, что ссылки на объекты не равны, а объекты равны
            if (person1.GetHashCode != person2.GetHashCode)
            {
                Console.WriteLine("Ссылки на объекты не равны.");
            }

            if (person1.Equals(person2))
            {
                Console.WriteLine("Объекты равны.");
            }

            // Выводим значения хэш-кодов для объектов
            Console.WriteLine($"Хэш-код person1: {person1.GetHashCode()}");
            Console.WriteLine($"Хэш-код person2: {person2.GetHashCode()}");

            // 2.
            Console.WriteLine("..................2..................");

            // Создаем объект Student
            Student student = new Student(person1, Education.Bachelor, 123);

            // Добавляем элементы в список экзаменов и зачетов
            student.AddExams(new Exam("Math", 5, new DateTime(2023, 6, 10)), new Exam("Physics", 5, new DateTime(2023, 6, 15)));
            student.Tests.Add(new Test("Math", true));
            student.Tests.Add(new Test("Philosophy", true));

            // Выводим данные объекта Student
            Console.WriteLine(student);

            // 3.
            Console.WriteLine("..................3..................");

            // Выводим значение свойства типа Person для объекта типа Student
            Console.WriteLine("Person объекта Student:");
            Console.WriteLine(student.Person);

            // 4.
            Console.WriteLine("..................4..................");

            // Создаем полную копию объекта Student
            Student copyStudent = student.DeepCopy();

            // Меняем данные в исходном объекте Student
            student.Group = 456;

            // Выводим копию и исходный объект
            Console.WriteLine("Копия объекта Student:");
            Console.WriteLine(copyStudent);
            Console.WriteLine("Исходный объект Student (с изменением группы):");
            Console.WriteLine(student);

            // 5.
            Console.WriteLine("..................5..................");

            // Присваиваем свойству с номером группы некорректное значение
            try
            {
                student.Group = 999;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Выводим сообщение из объекта-исключения
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            // 6.
            Console.WriteLine("..................6..................");

            // Выводим список всех зачетов и экзаменов с помощью оператора foreach
            Console.WriteLine("Список всех зачетов и экзаменов:");
            foreach (var item in student.GetAllItems())
            {
                Console.WriteLine(item);
            }

            // 7.
            Console.WriteLine("..................7..................");

            // Выводим список всех экзаменов с оценкой выше 3 с помощью оператора foreach с параметром
            Console.WriteLine("Список экзаменов с оценкой выше 3:");
            foreach (var exam in student.GetExamsWithScoreGreaterThan(3))
            {
                Console.WriteLine(exam);
            }

            // 8.
            Console.WriteLine("..................8..................");

            // С помощью оператора foreach выводим список предметов, которые есть как в списке зачетов, так и в списке экзаменов.
            Console.WriteLine("Предметы в списке зачетов и экзаменов:");
            foreach (var subject in student)
            {
                Console.WriteLine(subject);
            }

            // 9.
            Console.WriteLine("..................9..................");

            // С помощью оператора foreach выводим список всех сданных зачетов и сданных экзаменов.
            Console.WriteLine("Список сданных зачетов и экзаменов:");
            foreach (var item in student.GetPassedExamsAndTests())
            {
                Console.WriteLine(item);
            }

            // 10.
            Console.WriteLine("..................10..................");

            // С помощью оператора foreach выводим список сданных зачетов, для которых сдан и экзамен.
            Console.WriteLine("Список сданных зачетов, для которых сдан и экзамен:");
            foreach (var test in student.GetPassedTestsWithExams())
            {
                Console.WriteLine(test);
            }
        }
    }
}