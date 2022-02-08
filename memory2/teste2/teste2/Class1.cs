//UnmanagedExports.cs
using System;
using System.Collections.Generic;
using System.Text;
using RGiesecke.DllExport;
using System.Runtime.InteropServices;

namespace MyNameSpace
{
    public class MyClass
    {
        [DllExport("MyMethod", CallingConvention = CallingConvention.StdCall)]
        public static int MyMethod(int a, int b)
        {
            return a + b;
        }
    }
}
