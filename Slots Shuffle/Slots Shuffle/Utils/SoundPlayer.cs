using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace Slots_Shuffle.Utils
{
    public static class AudioPlayer
    {
        public static void Play(string fileName)
        {
            DependencyService.Get<ISoundPlayer>().Play(fileName);
            Thread.Sleep(300);
        }
    }
    public interface ISoundPlayer
    {
        void Play(string file);
    }
}
