using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; // Добавлена зависимость для метода Concat
using lab3; // Предполагается, что этот using используется для импорта namespace lab1, где определены другие необходимые классы.

namespace lab3
{
    public class Student : Person, IEnumerable<string> // Класс Student наследует Person и реализует интерфейс IEnumerable<string>.
    {
        // Защищенные поля класса Student.
        protected Person person;
        protected Education education;
        protected int group;
        protected List<Exam> exams = new List<Exam>();
        protected List<Test> tests = new List<Test>();

        // Конструктор класса, инициализирующий поля.
        public Student(Person person, Education education, int group)
        {
            this.person = person;
            this.education = education;
            this.group = group;
        }

        // Конструктор по умолчанию.
        public Student()
        {
            this.person = new Person();
            this.education = Education.Bachelor;
            this.group = 22;
        }

        // Свойство для доступа к полю "Person".
        public Person Person
        {
            get
            {
                return person;
            }
            set
            {
                person = value;
            }
        }

        // Свойство для доступа к полю "Education".
        public Education Education
        {
            get
            {
                return education;
            }
            set
            {
                education = value;
            }
        }

        // Свойство для доступа к полю "Group".
        public int Group
        {
            get
            {
                return group;
            }
            set
            {
                if ((value <= 100) || (value > 599))
                {
                    throw new ArgumentOutOfRangeException("Group number must be between 100 and 598.");
                }
                group = value;
            }
        }

        // Свойство для доступа к полю "Exams".
        public List<Exam> Exams
        {
            get { return exams; }
            set { exams = value; }
        }

        // Свойство для доступа к полю "Tests".
        public List<Test> Tests
        {
            get { return tests; }
            set { tests = value; }
        }

        // Вычисление средней оценки.
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

        // Индексатор для доступа к свойству "Education" по значению enum Education.
        public bool this[Education index]
        {
            get
            {
                return this.Education == index;
            }
        }

        // Метод для добавления экзаменов в список.
        public void AddExams(params Exam[] newExams)
        {
            foreach (var exam in newExams)
            {
                exams.Add(exam);
            }
        }

        // Переопределенный метод для вывода информации об объекте
        public override string ToString()
        {
            return string.Format("Person: {0}\n Education: {1}\n Group: {2}\n exams:\n {3} tests:\n {4}\n", person, education, group, string.Join("", exams), string.Join("", tests));
        }

        // Переопределенный метод для получения краткой информации о студенте.
        public new virtual string ToShortString()
        {
            return string.Format("Person: {0}\n Education: {1}. Group: {2}\n AvgRate: {3:0.00}\n", person, education, group, AvgRate);
        }

        // Переопределенный метод для сравнения объектов типа Student.
        public override bool Equals(object? obj)
        {
            return base.Equals(obj as Student);
        }

        // Метод для сравнения объектов типа Student.
        public bool Equals(Student? other)
        {
            if (other is null) return false;
            return (Person == other.Person &&
                    Education == other.Education &&
                    Group == other.Group &&
                    Exams == other.Exams);
        }

        // Перегрузка оператора "!=" для сравнения объектов типа Student.
        public static bool operator !=(Student student1, Student student2)
        {
            return (student1.Person != student2.Person &&
                    student1.Education != student2.Education &&
                    student1.Group != student2.Group &&
                    student1.Exams != student2.Exams);
        }

        // Перегрузка оператора "==" для сравнения объектов типа Student.
        public static bool operator ==(Student student1, Student student2)
        {
            return (student1.Person == student2.Person &&
                    student1.Education == student2.Education &&
                    student1.Group == student2.Group &&
                    student1.Exams == student2.Exams);
        }

        // Переопределенный метод для вычисления хэш-кода объекта Student.
        public override int GetHashCode()
        {
            return HashCode.Combine(Person, Education, Group, Exams);
        }

        //Метод для создания глубокой(deep) копии объекта Student.
        public override Student DeepCopy()
        {
            Student deepCopyStudent = new Student(this.Person.DeepCopy(), this.Education, this.Group);

            // Создаем глубокие копии экзаменов.
            deepCopyStudent.Exams = new List<Exam>();
            foreach (var exam in this.Exams)
            {
                deepCopyStudent.Exams.Add(exam.DeepCopy());
            }

            // Создаем глубокие копии тестов.
            deepCopyStudent.Tests = new List<Test>();
            foreach (var test in this.Tests)
            {
                deepCopyStudent.Tests.Add(test.DeepCopy());
            }

            return deepCopyStudent;
        }

        // Метод для перебора всех предметов (объектов типа string) из списка зачетов и экзаменов.
        public IEnumerable<object> GetAllItems()
        {
            foreach (var item in exams.Cast<object>().Concat(tests))
            {
                yield return item;
            }
        }

        // Метод для получения экзаменов с оценкой выше заданного значения.
        public IEnumerable<Exam> GetExamsWithScoreGreaterThan(int minScore)
        {
            return exams.Where(exam => exam.Grade > minScore);
        }

        // Вложенный интерфейс IDateAndCopy с определением метода DeepCopy и свойства Date.
        interface IDateAndCopy
        {
            object DeepCopy();
            DateTime Date { get; set; }
        }

        // Дополнительное задание:
        private class StudentEnumerator : IEnumerator<string>
        {
            private Student _student;
            private List<string> _subjects;
            private int _currentIndex = -1;

            public StudentEnumerator(Student student)
            {
                _student = student;
                _subjects = new List<string>();

                // Заполнение списка предметов на основе экзаменов и зачетов студента.
                foreach (var exam in _student.Exams)
                {
                    _subjects.Add(exam.Discipline);
                }

                foreach (var test in _student.Tests)
                {
                    _subjects.Add(test.NameOfDiscipline);
                }
            }

            // Текущий предмет.
            public string Current
            {
                get { return _subjects[_currentIndex]; }
            }

            // Текущий предмет (неявная реализация интерфейса IEnumerator).
            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose() { }

            // Переход к следующему предмету.
            public bool MoveNext()
            {
                _currentIndex++;
                return _currentIndex < _subjects.Count;
            }

            // Сброс индекса.
            public void Reset()
            {
                _currentIndex = -1;
            }
        }

        // Реализация метода GetEnumerator для интерфейса IEnumerable<string>.
        public IEnumerator<string> GetEnumerator()
        {
            HashSet<string> subjects = new HashSet<string>();

            // Заполнение множества предметов на основе экзаменов и зачетов студента.
            foreach (var exam in Exams)
            {
                subjects.Add(exam.Discipline);
            }

            foreach (var test in Tests)
            {
                subjects.Add(test.NameOfDiscipline);
            }

            // Возвращение предметов с помощью итератора.
            foreach (var subject in subjects)
            {
                yield return subject;
            }
        }

        // Реализация неявного метода GetEnumerator для интерфейса IEnumerable.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Итератор для перебора сданных зачетов и экзаменов (объектов типа object).
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

        // Итератор для перебора всех сданных зачетов (объектов типа Test), для которых сдан и экзамен.
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

        // Вспомогательный класс, реализующий интерфейс System.Collections.Generic.IComparer<Student>,
        // который можно использовать для сравнения объектов типа Student по среднему баллу.
        private class ComparisonByAverageScore : IComparer<Student>
        {
            public int Compare(Student? st1, Student? st2)
            {
                if (st1 is null || st2 is null) throw new ArgumentException("Некорректное значение параметра");
                return (int)(st1.AvgRate - st2.AvgRate);
            }
        }
    }
}
