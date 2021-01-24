using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;

namespace GoogleFormsCsvToPdf
{
    static class PdfExporter
    {
        public static void WritePdf(Stream fileStream, FormData formData)
        {
            var renderer = new PdfDocumentRenderer(true)
            {
                Document = ConstructPdfDocument(formData)
            };

            renderer.RenderDocument();

            renderer.PdfDocument.Save(fileStream);
        }

        private static Document ConstructPdfDocument(FormData formData)
        {
            var document = new Document();

            AddStylesToDocument(document);

            for (int i = 0; i < formData.Records.Count; i++)
            {
                var section = document.AddSection();

                section.AddParagraph($"Response #{i + 1}", "Title");

                for (int j = 0; j < formData.Headers.Length; j++)
                {
                    switch (formData.Headers[j].Export)
                    {
                        case ExportMode.Include:
                            section.AddParagraph(formData.Headers[j].Text, "Question");
                            section.AddParagraph(formData.Records[i][j]);
                            break;
                        case ExportMode.Invisible:
                            section.AddParagraph(formData.Headers[j].Text, "Question");
                            section.AddParagraph(formData.Records[i][j], "Invisible");
                            break;
                        default:
                            break;
                    }
                }
            }

            return document;
        }

        private static void AddStylesToDocument(Document doc)
        {
            var style = doc.AddStyle("Title", "Normal");
            style.Font.Size = 17;
            style.Font.Bold = true;

            style = doc.AddStyle("Question", "Normal");
            style.Font.Size = 13;
            style.Font.Bold = true;
            style.ParagraphFormat.SpaceBefore = 12;
            style.ParagraphFormat.SpaceAfter = 6;
            style.ParagraphFormat.KeepWithNext = true;

            style = doc.AddStyle("Invisible", "Normal");
            style.Font.Color = Color.FromArgb(0x00, 0xFF, 0xFF, 0xFF);
        }
    }
}
