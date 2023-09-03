using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;

namespace Triode.Desktop
{
    public class DesktopPlatform : Platform
    {
        private string AssetDirectory = "../../../../../Assets/";
        IView view;
        public override GL CreateGL() => GL.GetApi(
            view ?? throw new Exception("View was not initialized"));

        public override IView CreateView()
            => view = Window.Create(new()
            {
                Title = "The world is empty",
                WindowBorder = WindowBorder.Resizable,
                WindowState = WindowState.Normal,
                Size = new Vector2D<int>(500, 500),
                API = new GraphicsAPI(ContextAPI.OpenGL, new(3, 2)),
                VideoMode = VideoMode.Default,
                FramesPerSecond = 60,
                UpdatesPerSecond = 60,
                VSync = true,
                IsVisible = true,
                PreferredDepthBufferBits = 8
            });

        public override void LinkToApp(IView view)
        {
            try
            {
                /*try
                {
                    AssetDirectory = GetAssetStreamReader(
                        "assetDirectory.ini", "").ReadToEnd();
                }
                catch (FileNotFoundException e)
                {
                    _03310.Program.ReportException(e);
                }*/

                Debug("Asset directory is " + AssetDirectory);
                new Game.Main.GameCore(this, view);
                
            }
            catch (Exception e)
            {
                Debug("[LAUNCHER] " + e);
                Environment.Exit(e.HResult);
            }
        }

        public override void Debug(object text)
        {
            Console.WriteLine("[GAME] " + text);
        }


        public override FileStream GetWriteableStream(string name)
        {

            return new FileStream(AssetDirectory + name, FileMode.Open);
        }

        public override StreamReader GetWriteableStreamReader(string name)
        {
            return new StreamReader(AssetDirectory + name);
        }

        public override StreamWriter GetWriteableStreamWriter(string name)
        {
            return new StreamWriter(AssetDirectory + name);
        }

        public override bool WriteableExists(string name)
        {
            return File.Exists(AssetDirectory + name);
        }

        public override FileStream CreateWriteable(string name)
        {

            return new FileStream(AssetDirectory + name, FileMode.OpenOrCreate);
        }

        public override string[] ReadAllLinesWriteable(string name)
        {
            return File.ReadAllLines(AssetDirectory + name);
        }

        public override string ReadAllTextWriteable(string name)
        {
            return File.ReadAllText(AssetDirectory + name);
        }


        public override (int, int, byte[]) GetTextureData(string path)
        {
            Bitmap b = new(AssetDirectory + path);

            byte[] data = new byte[b.Width * b.Height * 4];

            for (int i = 0; i < b.Width; i++)
            {
                for (int p = 0; p < b.Height; p++)
                {
                    Color c = b.GetPixel(i, p);
                    int pos = (p * b.Width + i) * 4;
                    data[pos] = c.R;
                    data[pos + 1] = c.G;
                    data[pos + 2] = c.B;
                    data[pos + 3] = c.A;
                }
            }

            (int, int, byte[]) res = (b.Width, b.Height, data);
            b.Dispose();
            return res;
        }

        public override FileStream GetReadableStream(string name)
        {
            return GetWriteableStream(name);
        }

        public override StreamReader GetReadableStreamReader(string name)
        {
            return GetWriteableStreamReader(name);
        }

        public override bool ReadableExists(string name)
        {
            return WriteableExists(name);
        }

        public override string[] ReadAllLinesReadable(string name)
        {
            return ReadAllLinesWriteable(name);
        }

        public override string ReadAllTextReadable(string name)
        {
            return ReadAllTextWriteable(name);
        }
    }
}
