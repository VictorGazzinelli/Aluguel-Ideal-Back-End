using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AluguelIdeal.Api.Utils
{
    public static class FileUtils
    {
        public static string GetSolutionName() => GetSolutionFileName().Replace(".sln", "");
        public static string GetAssemblyDir() => Path.GetDirectoryName(GetAssemblyPath());
        public static string GetAssemblyPath() => Assembly.GetExecutingAssembly().Location;
        public static string GetSolutionFileName() => GetSolutionPath().Split(@"\").Last().Split(@"/").Last();
        public static string GetSolutionPath()
        {
            var currentDirPath = GetAssemblyDir();
            while (currentDirPath != null)
            {
                var fileInCurrentDir = Directory.GetFiles(currentDirPath).Select(f => f.Split(@"\").Last().Split(@"/").Last()).ToArray();
                var solutionFileName = fileInCurrentDir.SingleOrDefault(f => f.EndsWith(".sln", StringComparison.InvariantCultureIgnoreCase));
                if (solutionFileName != null)
                    return Path.Combine(currentDirPath, solutionFileName);

                currentDirPath = Directory.GetParent(currentDirPath)?.FullName;
            }

            throw new FileNotFoundException("Cannot find solution file path");
        }
    }
}
