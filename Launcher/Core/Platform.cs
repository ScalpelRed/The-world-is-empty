using Silk.NET.Core.Native;
using Silk.NET.Windowing;

namespace Triode;

public abstract class Platform
{
    public abstract NativeAPI CreateGL();
    public abstract IView CreateView();
    public abstract void LinkToApp(IView view);
    public abstract void Debug(object text);


    public abstract FileStream GetWriteableStream(string name);
    public abstract StreamReader GetWriteableStreamReader(string name);
    public abstract StreamWriter GetWriteableStreamWriter(string name);
    public abstract bool WriteableExists(string name);
    public abstract FileStream CreateWriteable(string name);
    public abstract string[] ReadAllLinesWriteable(string name);
    public abstract string ReadAllTextWriteable(string name);

    public abstract (int, int, byte[]) GetTextureData(string name);
    public abstract FileStream GetReadableStream(string name);
    public abstract StreamReader GetReadableStreamReader(string name);
    public abstract bool ReadableExists(string name);
    public abstract string[] ReadAllLinesReadable(string name);
    public abstract string ReadAllTextReadable(string name);
}