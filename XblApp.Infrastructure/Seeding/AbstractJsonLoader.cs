namespace XblApp.Database.Seeding
{
    public static class AbstractJsonLoader
    {
        internal static string GetJsonFilePath(string fileDir, string searchPattern)
        {
            string fileList = Path.Combine(AppContext.BaseDirectory, fileDir, searchPattern);

            if (fileList.Length == 0)
                throw new FileNotFoundException(
                    $"Could not find a file with the search name of {searchPattern} in directory {fileDir}");

            //If there are many then we take the most recent
            //return fileList.ToList().OrderBy(x => x).Last();

            return fileList;
        }
    }
}
