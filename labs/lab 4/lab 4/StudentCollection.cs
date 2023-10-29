using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using lab4;

namespace lab4
{
    public class StudentCollection
    {
        private List<Student> students = new List<Student>();
        private string collectionName;

        public StudentCollection(string collectionName)
        {
            this.collectionName = collectionName;
        }

        public List<Student> Students
        {
            get { return students; }
            set { students = value; }
        }

        public string CollectionName
        {
            get { return collectionName; }
            set { collectionName = value; }
        }

        public void AddDefaults(int count)
        {
            for (int i = 0; i < count; i++) students.Add(new Student());
        }

        public void AddStudents(params Student[] newStudents)
        {
            students.AddRange(newStudents);
            foreach (Student student in newStudents)
            {
                OnStudentsCountChanged("Added", student);
            }
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

        public delegate void StudentListHandler(object source, StudentListHandlerEventArgs args);

        public event StudentListHandler StudentsCountChanged;
        public event StudentListHandler StudentReferenceChanged;

        public bool Remove(int j)
        {
            if (j >= 0 && j < students.Count)
            {
                Student removedStudent = students[j];
                students.RemoveAt(j);
                OnStudentsCountChanged("Removed", removedStudent);
                return true;
            }
            return false;
        }

        public Student this[int index]
        {
            get
            {
                if (index >= 0 && index < students.Count)
                {
                    return students[index];
                }
                return null;
            }
            set
            {
                if (index >= 0 && index < students.Count)
                {
                    Student removedStudent = students[index];
                    students[index] = value;
                    OnStudentReferenceChanged("Replaced", removedStudent, value);
                }
            }
        }

        private void OnStudentsCountChanged(string changeType, Student student)
        {
            StudentsCountChanged?.Invoke(this, new StudentListHandlerEventArgs(collectionName, changeType, student));
        }

        private void OnStudentReferenceChanged(string changeType, Student oldStudent, Student newStudent)
        {
            StudentReferenceChanged?.Invoke(this, new StudentListHandlerEventArgs(collectionName, changeType, oldStudent, newStudent));
        }

    }
}