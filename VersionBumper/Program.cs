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

VersionBumper.VersionBumper.CurrentVersion   = DeusaldPhysics2D.DeusaldPhysics2D.Version;
VersionBumper.VersionBumper.PathToCsProjFile = "\\DeusaldPhysics2D\\DeusaldPhysics2D.csproj";
VersionBumper.VersionBumper.PathToMainCsFile = "\\DeusaldPhysics2D\\DeusaldPhysics2D\\DeusaldPhysics2D.cs";
VersionBumper.VersionBumper.PathsToUnityPackageJsons = new[]
{
    "\\UnityPackage.Net48\\package.json",
    "\\UnityPackage.NetStandard2.0\\package.json",
    "\\UnityPackage.NetStandard2.1\\package.json"
};
VersionBumper.VersionBumper.Run();