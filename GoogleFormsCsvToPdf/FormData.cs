using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleFormsCsvToPdf
{
    public class FormData
    {
        public FormField[] Headers { get; }

        private readonly List<string[]> recordsValue = new List<string[]>();
        public IReadOnlyList<string[]> Records { get => recordsValue.AsReadOnly(); }

        public FormData(FormField[] headers)
        {
            if (headers.Length < 1)
                throw new ArgumentException("Cannot import data with less than 1 fields.");

            Headers = headers;
        }

        public void AddRecord(string[] record)
        {
            if (record is null)
                throw new ArgumentNullException(nameof(record));
            if (record.Length != Headers.Length)
                throw new ArgumentException($"Record did not have the correct number of fields (expected = {Headers.Length}, actual = {record.Length}.");

            InsertNotAvailableOnEmptyFields(record);

            recordsValue.Add(record);
        }

        private static void InsertNotAvailableOnEmptyFields(string[] record)
        {
            for (int i = 0; i < record.Length; i++)
                if (string.IsNullOrWhiteSpace(record[i]))
                    record[i] = "N/A";
        }
    }
}
