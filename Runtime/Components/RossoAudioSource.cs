using Rossoforge.Audio.Data;
using Rossoforge.Utils.Logger;
using UnityEngine;

namespace Rossoforge.Audio.Components
{
    [RequireComponent(typeof(AudioSource))]
    public class RossoAudioSource : MonoBehaviour
    {
        [SerializeField]
        private AudioConfigData _configData;

        private AudioSource _audioSource;
        private AudioChannelData _currentChannel;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            Initialize();
        }
        private void OnDisable()
        {
            Cleanup();
        }

        private void UpdateVolume(float channelVolume)
        {
            _audioSource.volume = _configData.BaseVolume * channelVolume;
        }
        private void UpdateMuteState(bool isMuted)
        {
            _audioSource.mute = isMuted;
        }

        private void Initialize()
        {
            if (_configData == null)
                return;

            _audioSource.clip = _configData.Clip;
            _audioSource.loop = _configData.Loop;
            _audioSource.pitch = _configData.Pitch;

            _currentChannel = _configData.Channel;
            if (_currentChannel == null)
            {
                RossoLogger.Error($"AudioConfigData '{_configData.name}' missing required AudioChannelData reference on GameObject '{gameObject.name}'.");
                return;
            }

            UpdateVolume(_currentChannel.Volume);
            UpdateMuteState(_currentChannel.IsMuted);

            _currentChannel.OnVolumeChanged += UpdateVolume;
            _currentChannel.OnMutedChanged += UpdateMuteState;
        }
        public void Cleanup()
        {
            if (_currentChannel == null)
                return;

            _currentChannel.OnVolumeChanged -= UpdateVolume;
            _currentChannel.OnMutedChanged -= UpdateMuteState;
        }
    }
}
