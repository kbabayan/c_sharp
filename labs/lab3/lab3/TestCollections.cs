using System;
using System.Collections.Generic;
using lab1;
using System.Diagnostics;

namespace lab3
{
    public class TestCollections
    {
        private List<Person> personsList = new List<Person>();
        private List<string> strList = new List<string>();
        private Dictionary<Person, Student> personsDict = new Dictionary<Person, Student>();
        private Dictionary<string, Student> strDict = new Dictionary<string, Student>();

        // Конструктор с параметром - количеством элементов в коллекциях
        public TestCollections(int count)
        {
            personsList = new List<Person>();
            strList = new List<string>();
            personsDict = new Dictionary<Person, Student>();
            strDict = new Dictionary<string, Student>();

            GenerateData(count);
        }

        // Метод для автоматической генерации элементов коллекций
        public static Student GenerateStudent(int studentIndex)
        {
            string name = $"Student {studentIndex}";
            string surname = $"Surname {studentIndex}";
            DateTime dateOfBirth = DateTime.Now.AddYears(-20).AddDays(studentIndex);
            Person person = new Person(name, surname, dateOfBirth);
            Education education = (Education)(studentIndex % 4); // Генерация формы обучения
            int group = 100 + studentIndex; // Генерация группы
            Student student = new Student(person, education, group);
            return student;
        }

        // Заполнение коллекций данными
        private void GenerateData(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var student = GenerateStudent(i);
                personsList.Add(student.Person);
                strList.Add(student.Person.Name);
                personsDict.Add(student.Person, student);
                strDict.Add(student.Person.Name, student);
            }
        }

        // Метод для измерения времени поиска элемента в коллекции List<Person>
        public TimeSpan SearchInPersonList(Person person)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            personsList.Contains(person);
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        // Метод для измерения времени поиска элемента в коллекции List<string>
        public TimeSpan SearchInStringList(string name)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            strList.Contains(name);
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        // Метод для измерения времени поиска элемента по ключу в Dictionary<Person, Student>
        public TimeSpan SearchInPersonDictionary(Person person)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            personsDict.ContainsKey(person);
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        // Метод для измерения времени поиска элемента по ключу в Dictionary<string, Student>
        public TimeSpan SearchInStringDictionary(string name)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            strDict.ContainsKey(name);
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        // Метод для измерения времени поиска элемента по значению в Dictionary<Person, Student>
        public TimeSpan SearchByValueInPersonDictionary(Student student)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            personsDict.ContainsValue(student);
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        // Метод для измерения времени поиска элемента по значению в Dictionary<string, Student>
        public TimeSpan SearchByValueInStringDictionary(Student student)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            strDict.ContainsValue(student);
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}

