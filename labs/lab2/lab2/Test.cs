using System;

namespace lab1
{
	public class Test
	{
		protected string nameOfDiscipline;
		protected bool examResult;

		public Test(string _nameOfDiscipline, bool _examResult)
		{
			nameOfDiscipline = _nameOfDiscipline;
			examResult = _examResult;
		}

        public Test()
		{
			nameOfDiscipline = "Math";
			examResult = true;
		}

		public string NameOfDiscipline
		{
			get
			{
				return this.nameOfDiscipline;
			}
			set
			{
				this.nameOfDiscipline = value;
			}
		}

		public bool ExamResult
		{
			get { return this.examResult; }
			set { this.examResult = value; }
		}

        public override string ToString()
        {
			return string.Format("Test {0} is {1}\n", this.nameOfDiscipline, this.examResult);
        }

        public Test DeepCopy()
        {
            Test deepCopyTest = new Test(this.NameOfDiscipline, this.ExamResult);
            return deepCopyTest;
        }
    }
}

