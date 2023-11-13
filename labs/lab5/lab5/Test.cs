using System;
using lab4;

namespace lab4
{
    [Serializable]
    public class Test
    {
        protected string nameOfDiscipline; // Поле, содержащее название дисциплины теста.
        protected bool examResult; // Поле, указывающее, был ли сдан экзамен.

        public Test(string _nameOfDiscipline, bool _examResult)
        {
            nameOfDiscipline = _nameOfDiscipline;
            examResult = _examResult;
        }

        // Значения по умолчанию для нового экземпляра Test.
        public Test()
        {
            nameOfDiscipline = "Math";
            examResult = true;
        }

        // Аксессор для поля nameOfDiscipline
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

        // Аксессор для поля examResult
        public bool ExamResult
        {
            get { return this.examResult; }
            set { this.examResult = value; }
        }

        // Возвращает строку с информацией о тесте.
        public override string ToString()
        {
            return string.Format("Test {0} is {1}\n", this.nameOfDiscipline, this.examResult);
        }

        // Создает глубокую (deep) копию теста.
        public Test DeepCopy()
        {
            Test deepCopyTest = new Test(this.NameOfDiscipline, this.ExamResult);
            return deepCopyTest;
        }
    }
}
