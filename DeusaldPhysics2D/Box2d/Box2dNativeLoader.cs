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

// ReSharper disable InconsistentNaming

namespace Box2D
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public static class Box2dNativeLoader
    {
        public enum System
        {
            Windows
        }
        
        public static readonly Version Version = new Version(2, 4, 1);
        
        public static void LoadNativeLibrary(System system)
        {
            string libName = "";

            switch (system)
            {
                case System.Windows:
                {
                    libName = "box2d.dll";
                    break;
                }
            }
            
            string       embeddedResource = "DeusaldPhysics2D.Box2d.Libs.";
            embeddedResource += system + ".";
            embeddedResource += Environment.Is64BitProcess ? "x86_64." : "x86.";
            embeddedResource += libName;

            byte[]   data;
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            using (Stream stream = currentAssembly.GetManifestResourceStream(embeddedResource))
            {
                // Either the file is not existed or it is not mark as embedded resource
                if (stream == null)
                    throw new Exception(embeddedResource + " is not found in Embedded Resources.");

                // Get byte[] from the file from embedded resource
                data = new byte[(int) stream.Length];
                stream.Read(data, 0, (int) stream.Length);
            }

            string       bits     = Environment.Is64BitProcess ? "x64" : "x86";
            AssemblyName name     = Assembly.GetExecutingAssembly().GetName();
            string       tempPath = Path.Combine(Path.GetTempPath(), $"{(object) name.Name}.{(object) bits}.{(object) name.Version}");

            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);

            string environmentVariable = Environment.GetEnvironmentVariable("PATH");

            // ReSharper disable once PossibleNullReferenceException
            string[] envVariables = environmentVariable.Split(';');
            bool     exist        = false;

            foreach (string singlePath in envVariables)
            {
                if (singlePath == tempPath)
                {
                    exist = true;
                    break;
                }
            }

            if (!exist)
                Environment.SetEnvironmentVariable("PATH", tempPath + ";" + environmentVariable);
            
            string path  = Path.Combine(tempPath, libName);
            
            bool   needToRewriteLib = true;
            
            if (File.Exists(path))
            {
                byte[] numArray = File.ReadAllBytes(path);
                
                if (data.SequenceEqual(numArray))
                    needToRewriteLib = false;
            }

            if (!needToRewriteLib)
                return;
            
            try
            {
                File.WriteAllBytes(path, data);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}