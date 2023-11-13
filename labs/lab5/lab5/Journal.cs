using System;

namespace lab4
{
	public class Journal
	{
		private List<JournalEntry> entries = new List<JournalEntry>();

        public void StudentsCountChangedHandler(object source, StudentListHandlerEventArgs args)
        {
            string changeType = args.ChangeType == "Added" ? "added" : "removed";
            string studentInfo = args.Student.ToString();

            JournalEntry entry = new JournalEntry(args.CollectionName, changeType, studentInfo);
            entries.Add(entry);
        }

        public void StudentReferenceChangedHandler(object source, StudentListHandlerEventArgs args)
        {
            string studentInfo = args.Student.ToString();

            JournalEntry entry = new JournalEntry(args.CollectionName, "reference changed", studentInfo);
            entries.Add(entry);
        }

        public override string ToString()
        {
            string result = "Journal Entries:\n";
            foreach (JournalEntry entry in entries)
            {
                result += entry.ToString() + "\n";
            }

            return result;
        }
    }
}

