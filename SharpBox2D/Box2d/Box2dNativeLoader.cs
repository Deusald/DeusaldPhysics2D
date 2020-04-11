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
            Windows, Linux, MacOS, Android, iOS
        }
        
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

                case System.Linux:
                case System.MacOS:
                case System.Android:
                case System.iOS:
                {
                    libName = "box2d.so";
                    break;
                }
            }
            
            string       embeddedResource = "SharpBox2D.Box2d.Libs.";
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