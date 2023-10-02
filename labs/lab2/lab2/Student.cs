using System;
using System.Collections;
using System.Collections.Generic;
using lab1;

namespace lab1
{
    public class Student : Person, IEnumerable<string>
    {
        protected Person person;
        protected Education education;
        protected int group;
        protected List<Exam> exams = new List<Exam>();
        protected List<Test> tests = new List<Test>();

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
            this.group = 22;
        }

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

        public int Group
        {
            get
            {
                return group;
            }
            set
            {
                if((value <= 100) || (value > 599)){
                    throw new ArgumentOutOfRangeException("Group number must be between 100 and 598.");
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
            get
            {
                return this.Education == index;
            }
        }

        public void AddExams(params Exam[] newExams)
        {
            foreach (var exam in newExams)
            {
                exams.Add(exam);
            }
        }

        public override string ToString()
        {
            // string a = $"Person {Person}"
            return string.Format("Person: {0}\n Education: {1}\n Group: {2}\n exams:\n {3} tests:\n {4}\n", person, education, group, string.Join("", exams), string.Join("", tests));
        }

        public new virtual string ToShortString()
        {
            return string.Format("Person: {0}\n Education: {1}. Group: {2}\n AvgRate: {3:0.00}\n", person, education, group, AvgRate);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj as Student);
        }

        public bool Equals(Student? other)
        {
            if (other is null) return false;
            return (Person == other.Person &&
                    Education == other.Education &&
                    Group == other.Group &&
                    Exams == other.Exams);
        }

        public static bool operator !=(Student student1, Student student2)
        {
            return (student1.Person != student2.Person &&
                    student1.Education != student2.Education &&
                    student1.Group != student2.Group &&
                    student1.Exams != student2.Exams);
        }

        public static bool operator ==(Student student1, Student student2)
        {
            return (student1.Person == student2.Person &&
                    student1.Education == student2.Education &&
                    student1.Group == student2.Group &&
                    student1.Exams == student2.Exams);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Person, Education, Group, Exams);
        }

        public override Student DeepCopy()
        {
            Student deepCopyStudent = new Student(this.Person, this.Education, this.Group);
            deepCopyStudent.Exams = this.Exams.Select(exam => exam.DeepCopy()).ToList();
            deepCopyStudent.Tests = this.Tests.Select(test => test.DeepCopy()).ToList();

            return deepCopyStudent;
        }

        public IEnumerable<object> GetAllItems()
        {
            foreach (var item in exams.Cast<object>().Concat(tests))
            {
                yield return item;
            }
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


        // Доп

        private class StudentEnumerator : IEnumerator<string>
        {
            private Student _student;
            private List<string> _subjects;
            private int _currentIndex = -1;

            public StudentEnumerator(Student student)
            {
                _student = student;
                _subjects = new List<string>();

                // Заполните _subjects предметами из списков экзаменов и зачетов
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Итератор для перебора сданных зачетов и экзаменов (объектов типа object)
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

        // Итератор для перебора всех сданных зачетов (объектов типа Test), для которых сдан и экзамен
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
    }
}

