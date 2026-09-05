using Rossoforge.Audio.DataConfig;
using Rossoforge.Audio.Service;
using Rossoforge.Services.Locator;
using UnityEngine;

namespace Rossoforge.Audio.Samples.Demo
{
    public class AudioPanelDemo : MonoBehaviour
    {
        public AudioChannelDataConfig _channel;

        private IAudioService _audioService;

        public void Start()
        {
            _audioService = ServiceLocator.Get<IAudioService>();
        }

        public void OnVolumeChanged(float volume)
        {
            _audioService.SetChannelVolume(_channel, volume);
            Debug.Log($"Volume changed to {volume} for channel {_channel.name}");
        }
        public void OnMuteChanged(bool isMuted)
        {
            _audioService.SetChannelMute(_channel, isMuted);
            Debug.Log($"Channel {_channel.name} is now {(isMuted ? "inactive" : "active")}");
        }
    }
}
