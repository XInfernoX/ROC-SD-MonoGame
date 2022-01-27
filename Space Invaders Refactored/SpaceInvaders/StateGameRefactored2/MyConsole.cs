#define SCHOOL

using System;

namespace StateGameRefactored2
{
    public static class MyConsole
    {
#if SCHOOL
        public static void Log(string pMessage)
        {
            Console.WriteLine(pMessage);
        }
#else
        public static void Log(string pMessage)
        {
            System.Diagnostics.Debug.WriteLine(pMessage);
        }
#endif
    }
}