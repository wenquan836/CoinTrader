using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Common.Util
{
    public static class SoundUtil
    {
        public static void Play(string path)
        {
            SoundPlayer p = new SoundPlayer();
            p.SoundLocation = path;
            p.LoadAsync();
            p.Play();
        }
    }
}
