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
            _mediaType = MEDIA_AUDIO;
        }

        public override void SetMediaPlayer(IXPlatformMediaObject mediaPlayerObject)
        {
            _audioPlayerObject = (AudioPlayerObject)mediaPlayerObject;
        }

        public override MediaPlayerObject GetSpecificPlayerObject()
        {
            if(_audioPlayerObject == null)
                return null;

            return (MediaPlayerObject)_audioPlayerObject;
        }

        public override MediaPlayerObject CreateSpecificPlayerObject()
        {
            _audioPlayerObject = new AudioPlayerObject();
            return (MediaPlayerObject)_audioPlayerObject;
        }

        public AudioPlayerObject GetAudioPlayer()
        {
            if(_audioPlayerObject == null)
                GetMediaPlayer(); //Initializes the audio player

            return _audioPlayerObject;
        }
        
        public async override void LoadMedia(string resource)
        {
            SetPlayFlag(false);
            await Task.Run(
                () =>
                {
                    if(GetPlayerState() != PLAYER_STATE_NONE)
                        ResetResources();

                    SetPlayerState(PLAYER_STATE_LOADING);                 
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
