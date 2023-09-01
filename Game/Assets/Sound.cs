using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Audio;
using Game.Main;
using SoLoud;

namespace Game.Assets
{
    public class Sound
    {
        private readonly AudioCore AudioCore;

        private readonly SoloudObject Data;

        public Sound(AudioCore audioCore, string filename)
        {
            AudioCore = audioCore;

            WavStream data = new();
            if (data.load(filename) == 2) throw new AssetNotFoundException("sound", filename);
            Data = data;
        }

        public void Play()
        {
            AudioCore.Play(Data);
        }
    }
}
