using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csv;

namespace GoogleFormsCsvToPdf
{
    static class CsvImporter
    {
        public static FormData ReadCsv(Stream fileStream)
        {
            var csv = CsvReader.ReadFromStream(fileStream);

            var formFields = new List<FormField>();

            foreach (var field in csv.First().Headers)
                formFields.Add(new FormField(field));

            var formData = new FormData(formFields.ToArray());

            foreach (var row in csv)
                formData.AddRecord(row.Values);

            return formData;
        }
    }
}
