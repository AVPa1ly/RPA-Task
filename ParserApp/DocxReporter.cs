using System;
using System.Collections.Generic;
using System.Linq;
using Word = Microsoft.Office.Interop.Word;

namespace ParserApp
{
    class DocxReporter : IFormatReporter
    {
        public void CreateReport(List<ModelItem> items)
        {
            var application = new Word.Application();

            Word.Document document = application.Documents.Add();

            foreach (var item in items)
            {
                Word.Paragraph titleParagraph = document.Paragraphs.Add();
                Word.Range titleRange = titleParagraph.Range;
                titleRange.Text = item.Title;
                titleParagraph.set_Style("Title");
                titleRange.InsertParagraphAfter();

                Word.Paragraph metaParagraph = document.Paragraphs.Add();
                Word.Range metaRange = metaParagraph.Range;
                metaRange.Text = $"Создано в {item.PubDate.ToString("ddd, dd MMM yyy HH:mm:ss")} в категории {item.Category}.";
                metaParagraph.set_Style("Intense Quote");
                metaRange.Font.Color = Word.WdColor.wdColorRed;
                metaRange.InsertParagraphAfter();

                Word.Paragraph linkParagraph = document.Paragraphs.Add();
                Word.Range linkRange = linkParagraph.Range;
                linkRange.Text = item.Link;
                linkRange.InsertParagraphAfter();

                Word.Paragraph descriptionParagraph = document.Paragraphs.Add();
                Word.Range descriptionRange = descriptionParagraph.Range;
                descriptionRange.Text = item.Description;
                descriptionParagraph.set_Style("Normal");
                descriptionRange.InsertParagraphAfter();

                linkRange.Hyperlinks.Add(linkRange, item.Link);

                if (item != items.LastOrDefault())
                {
                    document.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
                }

                string reportTime = DateTime.Now.ToString().Replace(' ', '_').Replace(':', '.');
                string path = PathConstructor.BuildPath("Reports", $"Report_{reportTime}.docx");

                document.SaveAs2(path);
                document.Close();
            }
        }
    }
}
