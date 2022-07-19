using System.Collections.Generic;

namespace ParserApp
{
    interface IFormatReporter
    {
        void CreateReport(List<ModelItem> items);
    }
}
