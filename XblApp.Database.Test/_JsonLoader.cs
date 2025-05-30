using System.Text.Json;

namespace XblApp.Database.Test
{
    public static class JsonLoader<TJson>
    {
        public static TJson? LoadJsonFile(string fileDir, string fileSearchString)
        {
            string filePath = GetJsonFilePath(fileDir, fileSearchString);
            string jsonContent = File.ReadAllText(filePath);

            TJson? jsonDecoded = JsonSerializer.Deserialize<TJson>(jsonContent);

            return jsonDecoded;
        }

        private static string GetJsonFilePath(string fileDir, string searchPattern)
        {
            string fullPath = Path.GetFullPath(Path.Combine(fileDir, searchPattern));

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"File '{searchPattern}' not found in directory '{fullPath}'");

            return fullPath;
        }
    }
}
