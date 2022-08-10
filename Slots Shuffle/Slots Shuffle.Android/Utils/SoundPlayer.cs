using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Slots_Shuffle.Droid.Utils.SoundEffect;
using Slots_Shuffle.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using static Android.Provider.MediaStore;

[assembly: Dependency(typeof(SoundPlayer))]

namespace Slots_Shuffle.Droid.Utils.SoundEffect
{
    class SoundPlayer : ISoundPlayer
    {
        public void Play(string file)
        {
            var player = new MediaPlayer();
            AssetFileDescriptor fd = global::Android.App.Application.Context.Assets.OpenFd(file);
            if (fd != null)
            {
                player.Prepared += (s, e) =>
                {
                    player.Start();
                };
                player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
                player.Prepare();
            }
        }
    }
}