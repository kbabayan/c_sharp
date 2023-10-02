using System;

namespace lab1
{
    public class Person
    {
        protected string name;
        protected string surname;
        protected System.DateTime dateOfBirth;

        public Person(string name, string surname, DateTime dateOfBirth)
        {
            this.name = name;
            this.surname = surname;
            this.dateOfBirth = dateOfBirth;
        }

        public Person()
        {
            this.name = "Kolya";
            this.surname = "Ivanov";
            this.dateOfBirth = new DateTime(2003, 5, 10);
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public string Surname
        {
            get
            {
                return this.surname;
            }
            set
            {
                this.surname = value;
            }
        }

        public DateTime DateOfBirth
        {
            get => this.dateOfBirth;
            set
            {
                this.dateOfBirth = value;
            }
        }

        public int YearOfBirth
        {
            get
            {
                return Convert.ToInt32(this.dateOfBirth.Year);
            }
            set
            {
                this.DateOfBirth = Convert.ToDateTime(value);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}. Date of birth: {2}", this.name, this.surname, this.dateOfBirth);
        }

        public virtual string ToShortString()
        {
            return string.Format(this.name + " " + this.surname);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj as Person);
        }

        public bool Equals(Person? other)
        {
            if (other is null) return false;
            return (Name == other.Name &&
                    Surname == other.Surname &&
                    DateOfBirth == other.DateOfBirth);
        }

        public static bool operator !=(Person person1, Person person2)
        {
            return (person1.Name != person2.Name &&
                    person1.Surname != person2.Surname &&
                    person1.DateOfBirth != person2.DateOfBirth);
        }

        public static bool operator ==(Person person1, Person person2)
        {
            return (person1.Name == person2.Name &&
                    person1.Surname == person2.Surname &&
                    person1.DateOfBirth == person2.DateOfBirth);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname, DateOfBirth);
        }

        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

        public virtual Person DeepCopy()
        {
            Person deepCopyPerson = new Person(this.Name, this.Surname, this.DateOfBirth);
            return deepCopyPerson;
        }

        interface IDateAndCopy
        {
            object DeepCopy();
            DateTime Date { get; set; }
        }
    }
}