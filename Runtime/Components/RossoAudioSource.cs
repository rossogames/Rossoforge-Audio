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

        private void Initialize()
        {
            if (_configData == null)
                return;

            _currentChannel = _configData.Channel;
            if (_currentChannel == null)
            {
                RossoLogger.Error($"AudioConfigData '{_configData.name}' missing required AudioChannelData reference on GameObject '{gameObject.name}'.");
                return;
            }

            SetClip();
            SetMixerGroup();

            SetPriority();
            SetVolume(_currentChannel.Volume);
            SetPitch();
            SetStereoPan();
            SetSpatialBlend();
            SetReverbZoneMix();

            SetMuteState(_currentChannel.IsMuted);
            SetBypassEffects();
            SetBypassListenerEffects();
            SetBypassReverbZones();
            SetLoop();

            SetDopplerLevel();
            SetSpread();
            SetRolloffMode();
            SetMinDistance();
            SetMaxDistance();

            _currentChannel.OnVolumeChanged += SetVolume;
            _currentChannel.OnMutedChanged += SetMuteState;

            if (_configData.Autoplay)
                _audioSource.Play();
        }
        private void Cleanup()
        {
            if (_currentChannel == null)
                return;

            _currentChannel.OnVolumeChanged -= SetVolume;
            _currentChannel.OnMutedChanged -= SetMuteState;
        }

        private void SetClip() => _audioSource.clip = _configData.Clip;
        private void SetMixerGroup() => _audioSource.outputAudioMixerGroup = _configData.MixerGroup;
        private void SetPriority() => _audioSource.priority = _configData.Priority;
        private void SetVolume(float channelVolume) => _audioSource.volume = _configData.Volume * channelVolume;
        private void SetPitch() => _audioSource.pitch = _configData.Pitch;
        private void SetStereoPan() => _audioSource.panStereo = _configData.StereoPan;
        private void SetSpatialBlend() => _audioSource.spatialBlend = _configData.SpatialBlend;
        private void SetReverbZoneMix() => _audioSource.reverbZoneMix = _configData.ReverbZoneMix;

        private void SetMuteState(bool isMuted) => _audioSource.mute = _configData.Mute || isMuted;
        private void SetBypassEffects() => _audioSource.bypassEffects = _configData.BypassEffects;
        private void SetBypassListenerEffects() => _audioSource.bypassListenerEffects = _configData.BypassListenerEffects;
        private void SetBypassReverbZones() => _audioSource.bypassReverbZones = _configData.BypassReverbZones;
        private void SetLoop() => _audioSource.loop = _configData.Loop;

        private void SetDopplerLevel() => _audioSource.dopplerLevel = _configData.DopplerLevel;
        private void SetSpread() => _audioSource.spread = _configData.Spread;
        private void SetRolloffMode() => _audioSource.rolloffMode = _configData.VolumeRolloff;
        private void SetMinDistance() => _audioSource.minDistance = _configData.MinDistance;
        private void SetMaxDistance() => _audioSource.maxDistance = _configData.MaxDistance;
    }
}
