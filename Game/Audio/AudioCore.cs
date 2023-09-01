using SoLoud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Audio
{
    public class AudioCore
    {
        internal Soloud SoLoud;

        public AudioCore()
        {
            SoLoud = new Soloud();
            SoLoud.init();
        }

        ~AudioCore()
        {
            SoLoud.deinit();
        }

        internal void Play(SoloudObject sound)
        {
            SoLoud.play(sound);
           
        }
    }
}
