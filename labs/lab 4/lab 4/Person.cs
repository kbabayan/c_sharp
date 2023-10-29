using System;

namespace lab4
{
    public class Person : IComparable<Person>, IComparer<Person>
    {
        protected string name;
        protected string surname;
        protected DateTime dateOfBirth;

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
            get { return this.name; }
            set { this.name = value; }
        }

        public string Surname
        {
            get { return this.surname; }
            set { this.surname = value; }
        }

        public DateTime DateOfBirth
        {
            get { return this.dateOfBirth; }
            set { this.dateOfBirth = value; }
        }

        public int YearOfBirth
        {
            get { return this.dateOfBirth.Year; }
            set { this.dateOfBirth = new DateTime(value, this.dateOfBirth.Month, this.dateOfBirth.Day); }
        }

        public override string ToString()
        {
            return $"{this.name} {this.surname}. Date of birth: {this.dateOfBirth:d}";
        }

        public virtual string ToShortString()
        {
            return $"{this.name} {this.surname}";
        }

        public override bool Equals(object obj)
        {
            if (obj is null || GetType() != obj.GetType())
                return false;

            Person other = (Person)obj;
            return this.name == other.name && this.surname == other.surname && this.dateOfBirth == other.dateOfBirth;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.name, this.surname, this.dateOfBirth);
        }

        public static bool operator ==(Person person1, Person person2)
        {
            return person1.Equals(person2);
        }

        public static bool operator !=(Person person1, Person person2)
        {
            return !person1.Equals(person2);
        }

        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

        public virtual Person DeepCopy()
        {
            return new Person(this.name, this.surname, this.dateOfBirth);
        }

        public int CompareTo(Person other)
        {
            if (other is null)
                return 1;

            int result = this.surname.CompareTo(other.surname);
            if (result == 0)
            {
                result = this.name.CompareTo(other.name);
            }
            return result;
        }

        public int Compare(Person x, Person y)
        {
            if (x is null || y is null)
                throw new ArgumentException("Invalid argument");

            return x.dateOfBirth.CompareTo(y.dateOfBirth);
        }
    }
}