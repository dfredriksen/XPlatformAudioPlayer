using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Android.Media;
using Android.Content;
using Android.Runtime;
using System.Threading.Tasks;
using Android.Widget;
using CYINT.XPlatformMediaPlayer;

[assembly: Xamarin.Forms.Dependency (typeof (CYINT.XPlatformAudioPlayer.AndroidAudioPlayerImplementation))]
namespace CYINT.XPlatformAudioPlayer
{
    public class AndroidAudioPlayerImplementation : CYINT.XPlatformMediaPlayer.AndroidMediaPlayerImplementation, IXPlatformAudioPlayer
    {
        protected AudioPlayerObject _audioPlayerObject;

        public AndroidAudioPlayerImplementation()
        {
            _audioPlayerObject = null;
            _mediaType = XPlatformMediaPlayerImplementation.MEDIA_AUDIO;
        }

        public override void SetMediaPlayer(IXPlatformMediaObject mediaPlayerObject)
        {
            _audioPlayerObject = (AudioPlayerObject)mediaPlayerObject;
        }

        public override MediaPlayerObject GetSpecificPlayerObject()
        {
            return (MediaPlayerObject)_audioPlayerObject;
        }

        public override MediaPlayerObject CreateSpecificPlayerObject()
        {
            _audioPlayerObject = new AudioPlayerObject();
            return (MediaPlayerObject)_audioPlayerObject;
        }

        public AudioPlayerObject GetAudioPlayer()
        {
            return _audioPlayerObject;
        }
        
        public async override void LoadMedia(string resource)
        {
            SetPlayFlag(false);
            await Task.Run(
                () =>
                {
                    if(GetPlayerState() != XPlatformMediaPlayerImplementation.PLAYER_STATE_NONE)
                        ResetResources();

                    SetPlayerState(XPlatformMediaPlayerImplementation.PLAYER_STATE_LOADING);                 
                    GetAudioPlayer().SetAudioStreamType(Stream.Music);
                    GetAudioPlayer().SetDataSource(resource);             
                    GetAudioPlayer().PrepareAsync();
                }
            );
        }

    }

    public class AndroidAudioPlayerImplementationException : Exception
    {
        public AndroidAudioPlayerImplementationException(string message) : base (message)
        {}
    }
}
