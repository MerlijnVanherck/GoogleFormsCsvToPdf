using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csv;

namespace GoogleFormsCsvToPdf
{
    public static class CsvImporter
    {
        public static FormData ReadCsv(string path)
        {
            var options = new CsvOptions()
            {
                AllowNewLineInEnclosedFieldValues = true,
                ReturnEmptyForMissingColumn = true,
                ValidateColumnCount = true
            };
            var csv = File.ReadAllText(path, Encoding.UTF8);
            var data = CsvReader.ReadFromText(csv, options);

            var formFields = new List<FormField>();

            foreach (var field in data.First().Headers)
                formFields.Add(new FormField(field));

            var formData = new FormData(formFields.ToArray());

            foreach (var row in data)
                formData.AddRecord(row.Values);

            return formData;
        }
    }
}
