from ReadWriteMemory import ReadWriteMemory
import ctypes as ctypes
from ctypes import wintypes as w
import io as io
import numpy as np
import cv2 as cv2
import PIL.Image as Image


rwm = ReadWriteMemory()
k32 = ctypes.windll.kernel32

OpenProcess = k32.OpenProcess
OpenProcess.argtypes = [w.DWORD,w.BOOL,w.DWORD]
OpenProcess.restype = w.HANDLE

GetLastError = k32.GetLastError
GetLastError.argtypes = None
GetLastError.restype = w.DWORD

ReadProcessMemory = k32.ReadProcessMemory
ReadProcessMemory.argtypes = [w.HANDLE,w.LPCVOID,w.LPVOID,ctypes.c_size_t,ctypes.POINTER(ctypes.c_size_t)]
ReadProcessMemory.restype = w.BOOL

process = rwm.get_process_by_name('Unity.exe')
process.open()
processHandle = OpenProcess(0x10, False, process.pid)
data = ctypes.c_int64()
aberto = 1
bytesRead = ctypes.c_ulonglong()
bytesRead1 = ctypes.c_ulonglong()

while aberto:
    
    result = ReadProcessMemory(processHandle,  2475742008916, ctypes.byref(data), 4, ctypes.byref(bytesRead))
    e = GetLastError()
    print('result: {}, err code: {}, bytesRead: {}'.format(result,e,bytesRead.value))
    print(data.value)
    data2 = (ctypes.c_uint8 * (1280 * 720))()
    result2 = ReadProcessMemory(processHandle, 2475742008920, ctypes.byref(data2), data.value, ctypes.byref(bytesRead1))
    image = Image.open(io.BytesIO(data2))
    image.show()
    
    print('result: {}, err code: {}, bytesRead: {}'.format(result2,e,bytesRead1.value))

# #print(base64.b64decode(data2))