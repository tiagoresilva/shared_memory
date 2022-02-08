import ctypes 
import io
import PIL.Image as Image
from pymem import *
from pymem.process import *
process = pymem.Pymem("unity.exe")
#imagem = ctypes.cast(2399249858624, ctypes.POINTER(SomeStruct)).contents
imagembyte = read_int(1814731391040)
#read = ctypes.CDLL("C:\\Users\\arware\\source\\repos\\teste3\\teste3\\bin\\Debug\\teste3.dll")

# ActualValue = ctypes.cast(1814456086648,ctypes.c_float).value
# print (ActualValue)
# image = Image.open(io.BytesIO(imagem))
# image.save(r"C:\Users\arware\Desktop")



