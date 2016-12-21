using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example
{
    class Global
    {        
        public const int MAX_NAXIS = 16;
        public static IntPtr g_handle;
        public static int g_naxis;

        private Global()
        {
            g_handle = IntPtr.Zero;
            g_naxis = 0;
        }

        public static bool isOpen()
        {
            return g_handle != IntPtr.Zero;
        }
    }
}
