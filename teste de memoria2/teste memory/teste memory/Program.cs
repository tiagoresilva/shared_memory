using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

public class MemoryRead
{
    const int PROCESS_WM_READ = 0x0010;
    const int PROCESS_VM_WRITE = 0x0020;
    const int PROCESS_VM_OPERATION = 0x0008;

    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    public static extern bool ReadProcessMemory(int hProcess,
    long lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool WriteProcessMemory(int hProcess, long lpBaseAddress,
      byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);
   
    


    public static void Main()
    {
        Process process = Process.GetProcessesByName("Unity")[0];
        IntPtr processHandleRead = OpenProcess(PROCESS_WM_READ, false, process.Id);
        IntPtr processHandleWrite = OpenProcess(0x1F0FFF, false, process.Id);

        int bytesWritten = 0;
        byte[] bufferWrite = Encoding.Unicode.GetBytes("testedememoria");
        //'\0' marks the end of string
        //replace 0x0046A3B8 with your address
        //WriteProcessMemory((int)processHandleWrite, 0x187A7D60A76, bufferWrite, bufferWrite.Length, ref bytesWritten);
        int bytesRead = 0;
        byte[] bufferRead = new byte[24]; //'Hello World!' takes 12*2 bytes because of Unicode 
        // 0x0046A3B8 is the address where I found the string, replace it with what you found

        IntPtr hglobal =  Marshal.AllocHGlobal(100);
        

        Console.WriteLine(Encoding.Unicode.GetString(bufferRead) +
           " (" + bytesRead.ToString() + "bytes)");
        Console.WriteLine("endereço base" + process.MainModule.BaseAddress.ToString("x8"));
        //Console.WriteLine("pointer to allocate" + hglobal);
        WriteProcessMemory((int)processHandleWrite, 0x0020 + hglobal.ToInt64(), bufferWrite, bufferWrite.Length, ref bytesWritten);
        

        ReadProcessMemory((int)processHandleRead, 0x0010 + hglobal.ToInt64(), bufferRead, bufferRead.Length, ref bytesRead);
        Console.WriteLine(Encoding.Unicode.GetString(bufferRead) +
           " (" + bytesRead.ToString() + "bytes)");
        //Console.WriteLine(Encoding.Unicode.GetString(bufferRead, 0, bufferRead.Length));
        //Console.ReadLine();

        //{
        //    // Allocate 1 byte of unmanaged memory.
        //    IntPtr hGlobal = Marshal.AllocHGlobal(1);

        //    // Create a new byte.
        //    byte b = 10;
        //    byte c = 13;
        //    byte d = 67;
        //    Console.WriteLine("Byte written to unmanaged memory: " + b);

        //    // Write the byte to unmanaged memory.
        //    Marshal.WriteByte(hGlobal + 0, b);
        //    Marshal.WriteByte(hGlobal + 1, c);
        //    Marshal.WriteByte(hGlobal + 2, d);

        //    // Read byte from unmanaged memory.
        //    byte e = Marshal.ReadByte(hGlobal+0);
        //    byte f = Marshal.ReadByte(hGlobal + 1);
        //    byte g = Marshal.ReadByte(hGlobal + 2);
        //    Console.WriteLine("Byte read from unmanaged memory0: " + e + "address " + hGlobal + 0);
        //    Console.WriteLine("Byte read from unmanaged memory1: " + f + "address " + hGlobal + 1);
        //    Console.WriteLine("Byte read from unmanaged memory2: " + g + "address " + hGlobal + 2);

        //    // Free the unmanaged memory.
        //    Marshal.FreeHGlobal(hGlobal);
        //    Console.WriteLine("Unmanaged memory was disposed.");
        //}
    }

}