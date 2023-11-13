using System;
using lab4;

namespace lab4
{
	public class StudentListHandlerEventArgs : EventArgs
	{
		public string CollectionName { get; }
		public string ChangeType { get; }
		public Student Student { get; }
        public Student NewStudent { get; }

        public StudentListHandlerEventArgs(string collectionName, string changeType, Student oldStudent, Student newStudent)
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            Student = oldStudent;
            NewStudent = newStudent;
        }

        public StudentListHandlerEventArgs(string collectionName, string changeType, Student student)
		{
            CollectionName = collectionName;
            ChangeType = changeType;
            Student = student;
            NewStudent = null;
        }

		public StudentListHandlerEventArgs()
		{
			CollectionName = string.Empty;
			ChangeType = string.Empty;
			Student = null;
            NewStudent = null;
        }

        public override string ToString()
        {
			return string.Format("Collection: {0}\n Change Type: {1}\n Student: {2}\n", CollectionName, ChangeType, Student);
        }
    }
}

