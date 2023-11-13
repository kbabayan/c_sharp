using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;
using lab4;
// using System.Text.Json;
using Newtonsoft.Json;

namespace lab4
{
    [Serializable]
    public class Student : Person //,IEnumerable<string>, IDateAndCopy
    {
        protected Person person;
        protected Education education;
        protected int group;
        protected List<Exam> exams = new List<Exam>();
        protected List<Test> tests = new List<Test>();

        public DateTime Date { get; set; }

        public Student(Person person, Education education, int group)
        {
            this.person = person;
            this.education = education;
            this.group = group;
        }

        public Student()
        {
            this.person = new Person();
            this.education = Education.Bachelor;
            this.group = 122;
        }

        public Person Person
        {
            get { return person; }
            set { person = value; }
        }

        public Education Education
        {
            get { return education; }
            set { education = value; }
        }

        public int Group
        {
            get { return group; }
            set
            {
                if (value < 100 || value > 599)
                {
                    throw new ArgumentOutOfRangeException("Group number must be between 100 and 599.");
                }
                group = value;
            }
        }

        public List<Exam> Exams
        {
            get { return exams; }
            set { exams = value; }
        }

        public List<Test> Tests
        {
            get { return tests; }
            set { tests = value; }
        }

        public double AvgRate
        {
            get
            {
                double sum = 0;
                foreach (Exam ex in Exams)
                {
                    sum += ex.Grade;
                }

                if (exams.Count == 0)
                    return 0;
                else
                    return sum / exams.Count;
            }
        }

        public bool this[Education index]
        {
            get { return this.Education == index; }
        }

        public void AddExams(params Exam[] newExams)
        {
            exams.AddRange(newExams);
        }

        public override string ToString()
        {
            return $"Person: {person}\nEducation: {education}\nGroup: {group}\nExams:\n{string.Join("\n", exams)}\nTests:\n{string.Join("\n", tests)}";
        }

        public new virtual string ToShortString()
        {
            return $"Person: {person}\nEducation: {education}. Group: {group}\nAvgRate: {AvgRate:0.00}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Student);
        }

        public bool Equals(Student other)
        {
            if (other is null) return false;
            return (Person == other.Person &&
                    Education == other.Education &&
                    Group == other.Group &&
                    Exams.SequenceEqual(other.Exams) &&
                    Tests.SequenceEqual(other.Tests));
        }

        public static bool operator !=(Student student1, Student student2)
        {
            return !(student1 == student2);
        }

        public static bool operator ==(Student student1, Student student2)
        {
            if (ReferenceEquals(student1, student2)) return true;
            if (student1 is null || student2 is null) return false;
            return (student1.Person == student2.Person &&
                    student1.Education == student2.Education &&
                    student1.Group == student2.Group &&
                    student1.Exams.SequenceEqual(student2.Exams) &&
                    student1.Tests.SequenceEqual(student2.Tests));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Person, Education, Group, Exams, Tests);
        }

        //object lab4.IDateAndCopy.DeepCopy => throw new NotImplementedException();

        //DateTime lab4.IDateAndCopy.Date { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public Student DeepCopy()
        //{
        //   using (MemoryStream memoryStream = new MemoryStream())
        //   {
        //       IFormatter formatter = new BinaryFormatter();
        //       formatter.Serialize(memoryStream, this);
        //       memoryStream.Seek(0, SeekOrigin.Begin);
        //       return (Student)formatter.Deserialize(memoryStream);
        //   }
        //}

        public Student DeepCopy()
        {
            // Сериализируем текущий объект Student в JSON
            string json = JsonConvert.SerializeObject(this);

            // Десериализируем JSON, чтобы создать копию объекта Student
            Student copy = JsonConvert.DeserializeObject<Student>(json);

            return copy;
        }

        public IEnumerable<object> GetAllItems()
        {
            return exams.Cast<object>().Concat(tests);
        }

        public IEnumerable<Exam> GetExamsWithScoreGreaterThan(int minScore)
        {
            return exams.Where(exam => exam.Grade > minScore);
        }

        interface IDateAndCopy
        {
            object DeepCopy();
            DateTime Date { get; set; }
        }

        private class StudentEnumerator : IEnumerator<string>
        {
            private Student _student;
            private List<string> _subjects;
            private int _currentIndex = -1;

            public StudentEnumerator(Student student)
            {
                _student = student;
                _subjects = new List<string>();
                foreach (var exam in _student.Exams)
                {
                    _subjects.Add(exam.Discipline);
                }

                foreach (var test in _student.Tests)
                {
                    _subjects.Add(test.NameOfDiscipline);
                }
            }

            public string Current
            {
                get { return _subjects[_currentIndex]; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose() { }

            public bool MoveNext()
            {
                _currentIndex++;
                return _currentIndex < _subjects.Count;
            }

            public void Reset()
            {
                _currentIndex = -1;
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            HashSet<string> subjects = new HashSet<string>();
            foreach (var exam in Exams)
            {
                subjects.Add(exam.Discipline);
            }

            foreach (var test in Tests)
            {
                subjects.Add(test.NameOfDiscipline);
            }

            foreach (var subject in subjects)
            {
                yield return subject;
            }
        }

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}

        public IEnumerable<object> GetPassedExamsAndTests()
        {
            foreach (var exam in Exams)
            {
                if (exam.Grade > 2)
                {
                    yield return exam;
                }
            }

            foreach (var test in Tests)
            {
                if (test.ExamResult)
                {
                    yield return test;
                }
            }
        }

        public IEnumerable<Test> GetPassedTestsWithExams()
        {
            foreach (var exam in Exams)
            {
                foreach (var test in Tests)
                {
                    if (test.NameOfDiscipline == exam.Discipline && exam.Grade > 2)
                    {
                        yield return test;
                    }
                }
            }
        }

        private class ComparisonByAverageScore : IComparer<Student>
        {
            public int Compare(Student st1, Student st2)
            {
                if (st1 is null || st2 is null) throw new ArgumentException("Некорректное значение параметра");
                return (int)(st1.AvgRate - st2.AvgRate);
            }
        }

        public bool Save(string filename)
        {
            try
            {
                string json = JsonConvert.SerializeObject(this);
                File.WriteAllText(filename, json);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving student to file: {ex.Message}");
                return false;
            }
        }

        public bool Load(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    string json = File.ReadAllText(filename);
                    Student loadedStudent = JsonConvert.DeserializeObject<Student>(json);
                    this.person = loadedStudent.Person;
                    this.education = loadedStudent.Education;
                    this.group = loadedStudent.Group;
                    this.exams = loadedStudent.Exams;
                    this.tests = loadedStudent.Tests;
                    return true;
                }
                else
                {
                    Console.WriteLine("File does not exist.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading student from file: {ex.Message}");
                return false;
            }
        }



        public bool AddFromConsole()
        {
            Console.WriteLine("Введите данные в формате: Название предмета;Оценка;Дата экзамена (ГГГГ-ММ-ДД)");

            try
            {
                string input = Console.ReadLine();
                string[] parts = input.Split(';');

                if (parts.Length != 3)
                {
                    Console.WriteLine("Ошибка: неверный формат ввода.");
                    return false;
                }

                string discipline = parts[0];
                int grade = int.Parse(parts[1]);
                DateTime examDate = DateTime.Parse(parts[2]);

                Exam newExam = new Exam(discipline, grade, examDate);
                Exams.Add(newExam);

                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка: неверный формат данных.");
                return false;
            }
        }
    }
}
