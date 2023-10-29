using System;

namespace lab4
{
	public class JournalEntry
	{
        public string CollectionName { get; }
        public string ChangeType { get; }
        public string StudentInfo { get; }

        public JournalEntry(string collectionName, string changeType, string studentInfo)
        {
            this.CollectionName = collectionName;
            this.ChangeType = changeType;
            this.StudentInfo = studentInfo;
        }

        public override string ToString()
        {
			return string.Format("Collection: {0}\n Change Type: {1}\n Student: {2}\n", CollectionName, ChangeType, StudentInfo);
        }
    }
}

