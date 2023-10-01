using System;
using lab1;

namespace lab1
{
    public class Student : Person
	{
        private Person person;
        private Education education;
        private int group;
        private List<Exam> exams = new List<Exam>();
        private List<Test> tests = new List<Test>();

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
    }
}

