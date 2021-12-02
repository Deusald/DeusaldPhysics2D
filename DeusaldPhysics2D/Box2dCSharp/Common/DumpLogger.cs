using System;

namespace Box2DSharp.Common
{
    internal static class DumpLogger
    {
        public static IDumpLogger Instance { get; set; } = new InternalDumpLogger();

        public static void Log(string message)
        {
            Instance.Log(message);
        }
    }

    internal interface IDumpLogger
    {
        void Log(string message);
    }

    internal class InternalDumpLogger : IDumpLogger
    {
        /// <inheritdoc />
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}