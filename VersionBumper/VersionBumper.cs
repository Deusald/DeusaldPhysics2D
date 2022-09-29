// MIT License

// DeusaldPhysics2D:
// Copyright (c) 2020 Adam "Deusald" Orliński

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

//v1.0.0

namespace VersionBumper;

public static class VersionBumper
{
    public static Version  CurrentVersion           { get; set; } = new();
    public static string   PathToCsProjFile         { get; set; } = "";
    public static string   PathToMainCsFile         { get; set; } = "";
    public static string[] PathsToUnityPackageJsons { get; set; } = Array.Empty<string>();

    public static void Run()
    {
        Console.WriteLine("New version:");
        string newVersionText = Console.ReadLine()!;
        Version.TryParse(newVersionText, out Version? newVersion);

        if (newVersion == null) throw new Exception("Failed to parse new version");
        
        string workingDirectory = Environment.CurrentDirectory;
        string projectDirectory = Directory.GetParent(workingDirectory)!.Parent!.Parent!.Parent!.FullName;

        string fullPathToCsProjFile = projectDirectory + PathToCsProjFile;
        string csProjFile           = File.ReadAllText(fullPathToCsProjFile);

        csProjFile = csProjFile.Replace($"<PackageVersion>{CurrentVersion}</PackageVersion>", $"<PackageVersion>{newVersion}</PackageVersion>");
        csProjFile = csProjFile.Replace($"<AssemblyVersion>{CurrentVersion}</AssemblyVersion>", $"<AssemblyVersion>{newVersion}</AssemblyVersion>");
        
        File.WriteAllText(fullPathToCsProjFile, csProjFile);
        
        string fullPathToMainCsFile = projectDirectory + PathToMainCsFile;
        string mainCsFile           = File.ReadAllText(fullPathToMainCsFile);

        mainCsFile = mainCsFile.Replace($"new Version({CurrentVersion.Major}, {CurrentVersion.Minor}, {CurrentVersion.Build})",
            $"new Version({newVersion.Major}, {newVersion.Minor}, {newVersion.Build})");
        
        File.WriteAllText(fullPathToMainCsFile, mainCsFile);

        foreach (string unityJsonPath in PathsToUnityPackageJsons)
        {
            string fullPathToUnityJson = projectDirectory + unityJsonPath;
            string unityJsonFile       = File.ReadAllText(fullPathToUnityJson);

            unityJsonFile = unityJsonFile.Replace($"\"version\": \"{CurrentVersion}\",", $"\"version\": \"{newVersion}\",");
            
            File.WriteAllText(fullPathToUnityJson, unityJsonFile);
        }
        
        Console.WriteLine($"Successfully updated version from {CurrentVersion} to {newVersion}.");
    }
}