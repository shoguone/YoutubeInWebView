using System.IO;
using System.Reflection;

namespace YoutubeInWebView.Services
{
    public class FileHelper
    {
        private Assembly _assembly = Assembly.GetExecutingAssembly();

        public FileHelper()
        {
        }

        public string GetPathToResource(string name, string subfolder = null)
        {
            return $"{_assembly.GetName().Name}.Resources.{(subfolder == null ? "" : subfolder + ".")}{name}";
        }

        public string ReadResourceFile(string path)
        {
            var stream = _assembly.GetManifestResourceStream(path);
            using (var reader = new StreamReader(stream))
            return reader.ReadToEnd();
        }
    }
}
