using Silk.NET.OpenGLES;
using Silk.NET.Windowing;
using Silk.NET.Windowing.Sdl.Android;
using Android.Util;
using Android.Graphics;
using Android.Content.Res;

namespace Triode.Android 
{ 

    public class AndroidPlatform : Platform
    {
        private AssetManager Assets;
        private string WriteableDirectory = "";

        private readonly SilkActivity Activity;

        IView view;

        public AndroidPlatform(SilkActivity silkActivity)
        {
            Activity = silkActivity;
        }

        public override GL CreateGL() => GL.GetApi(
            view ?? throw new Exception("View was not initialized"));

        public override IView CreateView() => view = Window.GetView(new()
        {
            API = new GraphicsAPI(ContextAPI.OpenGLES, new(3, 0))
        });

        public override void LinkToApp(IView view)
        {
            try
            {
                WriteableDirectory = Activity.ApplicationInfo.DataDir;
                Assets = Activity.Assets;
                new Game.Main.GameCore(this, view);
            }
            catch (Exception e)
            {
                Debug("[LAUNCHER] " + e);
                view.Close();
            }
        }

        public override void Debug(object text)
        {
            Log.Debug("[GAME] ", text.ToString());
        }

        public override FileStream GetWriteableStream(string name)
        {
            return new FileStream(WriteableDirectory + "/" + name, FileMode.Open);
        }

        public override StreamReader GetWriteableStreamReader(string name)
        {
            return new StreamReader(GetWriteableStream(name));
        }

        public override StreamWriter GetWriteableStreamWriter(string name)
        {
            return new StreamWriter(GetWriteableStream(name));
        }

        public override bool WriteableExists(string name)
        {
            return File.Exists(WriteableDirectory + "/" + name);
        }

        public override FileStream CreateWriteable(string name)
        {
            return new FileStream(WriteableDirectory + "/" + name, FileMode.OpenOrCreate);
        }

        public override string[] ReadAllLinesWriteable(string name)
        {
            return File.ReadAllLines(WriteableDirectory + "/" + name);
        }

        public override string ReadAllTextWriteable(string name)
        {
            return File.ReadAllText(WriteableDirectory + "/" + name);
        }

        public override (int, int, byte[]) GetTextureData(string name)
        {
            Bitmap b = BitmapFactory.DecodeStream(Assets.Open(name));

            Debug(b is null);

            byte[] data = new byte[b.Width * b.Height * 4];

            for (int i = 0; i < b.Width; i++)
            {
                for (int p = 0; p < b.Height; p++)
                {
                    Color c = new(b.GetPixel(i, p));
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
            return Assets.Open(name) as FileStream;
        }

        public override StreamReader GetReadableStreamReader(string name)
        {
            return new(Assets.Open(name));
        }

        public override bool ReadableExists(string name)
        {
            try
            {
                Assets.Open(name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override string[] ReadAllLinesReadable(string name)
        {
            List<string> res = new();
            StreamReader str = GetReadableStreamReader(name);

            while (!str.EndOfStream)
            {
                res.Add(str.ReadLine());
            }

            return res.ToArray();
        }

        public override string ReadAllTextReadable(string name)
        {
            return GetReadableStreamReader(name).ReadToEnd();
        }
    }
}
