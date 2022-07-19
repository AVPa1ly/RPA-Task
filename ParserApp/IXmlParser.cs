using System.Collections.Generic;

namespace ParserApp
{
    interface IXmlParser
    {
        List<ModelItem> ParseXmlData(string xmlSourcePath);
    }
}
