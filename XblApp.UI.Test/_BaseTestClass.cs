using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Text.Json;
using XblApp.Infrastructure.XboxLiveServices.Models;

namespace XblApp.Test
{
    public class BaseTestClass
    {
        //The equivalent JSON would be:
        //{
        //  "Key1": "Value1",
        //  "Nested": {
        //    "Key1": "NestedValue1",
        //    "Key2": "NestedValue2"
        //  }
        //}
        internal readonly Dictionary<string, string> inMemorySettings = new()
        {
            {"Key1", "Value1"},
            {"ConnectionStrings:MsSqlConnection", "Server=(localdb)\\mssqllocaldb;Database=XblApp;Trusted_Connection=True;MultipleActiveResultSets=true"},
            {"Nested:Key2", "NestedValue2"}
        };

        internal readonly IConfiguration _config;

        public BaseTestClass()
        {
            // test against this configuration
            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }

        /// <summary>
        /// Возвращает путь к папке 'nameFolder'(по умолчанию TestData)
        /// </summary>
        /// <param name="nameFolder"></param>
        /// <param name="callingAssembly"></param>
        /// <returns></returns>
        internal string GetPathToDir(string nameFolder = "TestData", Assembly callingAssembly = null)
        {
            string? pathToProject = GetCallingAssemblyTopLevelDir(callingAssembly ?? Assembly.GetCallingAssembly());
            string pathTpFolder = Path.GetFullPath(pathToProject + Path.DirectorySeparatorChar + nameFolder);

            return pathTpFolder;
        }

        /// <summary>
        /// Возвращает путь к 
        /// </summary>
        /// <param name="pathToDir"></param>
        /// <param name="nameFile"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        internal string GetJsonFilePath(string pathToDir, string nameFile)
        {
            var fileList = Directory.GetFiles(pathToDir, nameFile);

            if (fileList.Length == 0)
                throw new FileNotFoundException(
                    $"Could not find a file with the search name of {nameFile} in directory {pathToDir}");

            //If there are many then we take the most recent
            return fileList.ToList().OrderBy(x => x).Last();
        }

        internal string GetJsonFilePath(string nameFile)
        {
            string? pathToDir = GetPathToDir();

            var fileList = Directory.GetFiles(pathToDir, nameFile);

            if (fileList.Length == 0)
                throw new FileNotFoundException(
                    $"Could not find a file with the search name of {nameFile} in directory {pathToDir}");

            //If there are many then we take the most recent
            return fileList.ToList().OrderBy(x => x).Last();
        }

        internal T GetXJson<T>(string nameFile)
        {
            var filePath = GetJsonFilePath(nameFile);
            T? jsonDecoded = JsonSerializer.Deserialize<T>(File.ReadAllText(filePath));

            return jsonDecoded;
        }

        private string GetCallingAssemblyTopLevelDir(Assembly callingAssembly)
        {
            string text = $"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}";
            string location = (callingAssembly).Location;
            int num = location.IndexOf(text, StringComparison.OrdinalIgnoreCase);
            
            if (num <= 0)
                throw new Exception("Did not find '" + text + "' in the assembly. Do you need to provide the callingAssembly parameter?");

            return location.Substring(0, num);
        }
    }
}
