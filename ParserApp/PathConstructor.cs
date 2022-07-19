using System.IO;
using System.Reflection;

namespace ParserApp
{
    static class PathConstructor
    {
        public static string BuildPath(string folderName, string fileName)
        {
            var pathInfo = Directory.GetParent(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            return System.IO.Path.Combine(pathInfo.Parent.Parent.ToString(), folderName, fileName);
        }
    }
}
