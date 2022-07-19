using System;
using System.Collections.Generic;
using System.IO;

namespace ParserApp
{
    class TxtReporter : IFormatReporter
    {
        public void CreateReport(List<ModelItem> items)
        {

            string reportTime = DateTime.Now.ToString().Replace(' ', '_').Replace(':', '.');
            string path = PathConstructor.BuildPath("Reports", $"Report_{reportTime}.txt");

            using (StreamWriter writer = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                foreach (var item in items)
                {
                    writer.WriteLine(item.Title);
                    writer.WriteLine(item.Link);
                    writer.WriteLine(item.Description);
                    writer.WriteLine($"Создано в {item.PubDate.ToString("ddd, dd MMM yyy HH:mm:ss")} в категории {item.Category}");
                    writer.WriteLine("\n");
                }
            }
        }
    }
}
