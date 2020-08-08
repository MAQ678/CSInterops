using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        #region Test 1: Get char array as string
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void MyManagedDelegate(
    [MarshalAs(UnmanagedType.LPStr, SizeParamIndex = 1)]
    string values,
    int valueCount);

        [DllImport("Dll1", CallingConvention = CallingConvention.Cdecl)]
        public static extern void NativeLibCall(MyManagedDelegate callback);
        public static void PrintReceivedData(string values, int valueCount)
        {
            //foreach (var item in values)
            //    Console.WriteLine(item);
            Console.WriteLine(values);
        }
        private static void Test1()
        {
            NativeLibCall(PrintReceivedData);
        }
        #endregion

        #region Test2: getting IntPtr of uchar and convert to byte array

        [DllImport("Dll1", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr ReadDataFileCPP(out int bytesRead);
        private static void Test2()
        {
            int bytesRead;
            IntPtr buffer = ReadDataFileCPP(out bytesRead);
            byte[] bytes = new byte[bytesRead];
            Marshal.Copy(buffer, bytes, 0, bytesRead);

            Console.WriteLine(Encoding.Default.GetString(bytes));
        }

        #endregion

        #region Test3: Final
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ManagedDelegate(
            //[MarshalAs(UnmanagedType.LPStr, SizeParamIndex = 1)]
            IntPtr ptr,
            int bytesRead);

        [DllImport("Dll1", CallingConvention = CallingConvention.Cdecl)]
        public static extern void NativeLibCallBuffer(ManagedDelegate callback);

        public static void ReceivedBufferData(IntPtr ptr, int bytesRead)
        {
            //foreach (var item in values)
            //    Console.WriteLine(item);
            byte[] bytes = new byte[bytesRead];
            Marshal.Copy(ptr, bytes, 0, bytesRead);
            Console.WriteLine("Copied");
        }

        private static void Test3()
        {
            NativeLibCallBuffer(ReceivedBufferData);
        }

        #endregion
        public static void Main()
        {
            //Test1();

            //Test2();
            Test3();

            Console.ReadLine();
        }

        
    }
}
