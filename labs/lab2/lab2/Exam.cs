using System;

namespace lab1
{
    public class Exam
    {
        public string Discipline { get; set; }
        public int Grade { get; set; }
        public System.DateTime ExamDate { get; set; }

        public Exam(string discipline, int grade, DateTime examDate)
        {
            this.Discipline = discipline;
            this.Grade = grade;
            this.ExamDate = examDate;
        }

        public Exam()
        {
            this.Discipline = "TFCP";
            this.Grade = 5;
            this.ExamDate = new System.DateTime(2023, 6, 20);
        }

        public override string ToString()
        {
            return string.Format("According to the discipline {0} assessment {1}. Exam date {2}.\n", this.Discipline, this.Grade, this.ExamDate);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj as Exam);
        }

        public bool Equals(Exam? other)
        {
            if (other is null) return false;
            return (Discipline == other.Discipline &&
                    Grade == other.Grade &&
                    ExamDate == other.ExamDate);
        }

        public static bool operator !=(Exam exam1, Exam exam2)
        {
            return (exam1.Discipline != exam2.Discipline &&
                    exam1.Grade != exam2.Grade &&
                    exam1.ExamDate != exam2.ExamDate);
        }

        public static bool operator ==(Exam exam1, Exam exam2)
        {
            return (exam1.Discipline == exam2.Discipline &&
                    exam1.Grade == exam2.Grade &&
                    exam1.ExamDate == exam2.ExamDate);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Discipline, Grade, ExamDate);
        }

        public Exam DeepCopy()
        {
            Exam deepCopyExam = new Exam(this.Discipline, this.Grade, this.ExamDate);
            return deepCopyExam;
        }

        interface IDateAndCopy
        {
            object DeepCopy();
            DateTime Date { get; set; }
        }
    }
}