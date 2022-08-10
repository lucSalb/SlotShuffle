using AVFoundation;
using Foundation;
using Slots_Shuffle.iOS.Utils;
using Slots_Shuffle.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SoundPlayer))]
namespace Slots_Shuffle.iOS.Utils
{
    public class SoundPlayer : ISoundPlayer
    {
 
        public void Play(string fileName)
        {
            string sFilePath = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
            var url = NSUrl.FromString(sFilePath);
            var _player = AVAudioPlayer.FromUrl(url);
            _player.FinishedPlaying += (object sender, AVStatusEventArgs e) =>
            {
                _player = null;
            };
            _player.Play();
        }

    }
}