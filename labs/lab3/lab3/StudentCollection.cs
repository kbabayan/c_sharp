using System;
using System.Text;
using System.Collections.Generic;
using System.Linq; // Добавим пространство имен System.Linq для использования метода Max

namespace lab3
{
    public class StudentCollection
    {
        private List<Student> students = new List<Student>();

        public List<Student> Students
        {
            get { return students; }
            set { students = value; }
        }

        public void AddDefaults(int count)
        {
            for (int i = 0; i < count; i++) students.Add(new Student());
        }

        public void AddStudents(params Student[] newStudents)
        {
            students.AddRange(newStudents);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var student in students)
            {
                sb.AppendLine(student.ToString());
            }

            return sb.ToString();
        }

        public string ToShortString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var student in students)
            {
                sb.AppendLine(student.ToShortString());
                sb.AppendLine($"Exams Count: {student.Exams.Count}");
                sb.AppendLine($"Tests Count: {student.Tests.Count}");
            }

            return sb.ToString();
        }

        public void SortBySurname()
        {
            students.Sort((s1, s2) => s1.Person.Surname.CompareTo(s2.Person.Surname));
        }

        public void SortByDateOfBirth()
        {
            students.Sort((s1, s2) => s1.Person.DateOfBirth.CompareTo(s2.Person.DateOfBirth));
        }

        public void SortByAverageScore()
        {
            students.Sort((s1, s2) => s1.AvgRate.CompareTo(s2.AvgRate));
        }

        public double MaxAverageMark
        {
            get
            {
                if (students.Count > 0) return students.Max(student => student.AvgRate);
                else return 0.0;
            }
        }

        public IEnumerable<Student> SpecialistStudents
        {
            get
            {
                return students.Where(student => student.Education == Education.Specialist);
            }
        }

        public List<Student> AverageMarkGroup(double value)
        {
            return students.Where(student => student.AvgRate == value).ToList();
        }
    }
}