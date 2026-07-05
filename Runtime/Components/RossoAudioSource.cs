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
            _audioSource.playOnAwake = false;
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

            //--default--
            if (!SetChannel())
                return;

            SetMixerGroup();
            SetPriority();

            //--main--
            SetClip();
            SetVolume(_currentChannel.Volume);
            SetPitch();
            SetMuteState(_currentChannel.IsMuted);
            SetLoop();

            _currentChannel.OnVolumeChanged += SetVolume;
            _currentChannel.OnMutedChanged += SetMuteState;

            //--Spatial Settings--
            SetSpatialBlend();
            SetStereoPan();
            SetReverbZoneMix();
            SetDopplerLevel();
            SetSpread();
            SetVolumeRolloff();
            SetMinDistance();
            SetMaxDistance();

            //--Bypass Settings--
            SetBypassEffects();
            SetBypassListenerEffects();
            SetBypassReverbZones();

            AutoPlay();
        }
        private void Cleanup()
        {
            if (_currentChannel == null)
                return;

            _currentChannel.OnVolumeChanged -= SetVolume;
            _currentChannel.OnMutedChanged -= SetMuteState;
        }

        //--default--
        private bool SetChannel()
        {
            _currentChannel = _configData.Channel;
            if (_currentChannel == null)
            {
                RossoLogger.Error($"AudioConfigData '{_configData.name}' missing required AudioChannelData reference on GameObject '{gameObject.name}'.");
                return false;
            }

            return true;
        }
        private void SetMixerGroup() => _audioSource.outputAudioMixerGroup = _configData.MixerGroup;
        private void SetPriority() => _audioSource.priority = _configData.Priority;

        //--main--
        private void SetClip() => _audioSource.clip = _configData.Main.Clip;
        private void SetVolume(float channelVolume) => _audioSource.volume = _configData.Main.Volume * channelVolume;
        private void SetPitch() => _audioSource.pitch = _configData.Main.Pitch;
        private void SetMuteState(bool isMuted) => _audioSource.mute = _configData.Main.Mute || isMuted;
        private void SetLoop() => _audioSource.loop = _configData.Main.Loop;
        private void AutoPlay()
        {
            if (_configData.Main.Autoplay)
                _audioSource.Play();
        }

        //--Spatial Settings--
        private void SetSpatialBlend() => _audioSource.spatialBlend = _configData.Spatial.SpatialBlend;
        private void SetStereoPan() => _audioSource.panStereo = _configData.Spatial.StereoPan;
        private void SetReverbZoneMix() => _audioSource.reverbZoneMix = _configData.Spatial.ReverbZoneMix;
        private void SetDopplerLevel() => _audioSource.dopplerLevel = _configData.Spatial.DopplerLevel;
        private void SetSpread() => _audioSource.spread = _configData.Spatial.Spread;
        private void SetVolumeRolloff() => _audioSource.rolloffMode = _configData.Spatial.VolumeRolloff;
        private void SetMinDistance()
        {
            if (_configData.Spatial.MinDistance > _configData.Spatial.MaxDistance)
            {
                RossoLogger.Error($"AudioConfigData '{_configData.name}': Min Distance must be less than Max Distance.");
                return;
            }
            _audioSource.minDistance = _configData.Spatial.MinDistance;
        }
        private void SetMaxDistance() => _audioSource.maxDistance = _configData.Spatial.MaxDistance;

        //--Bypass Settings--
        private void SetBypassEffects() => _audioSource.bypassEffects = _configData.Bypass.Effects;
        private void SetBypassListenerEffects() => _audioSource.bypassListenerEffects = _configData.Bypass.ListenerEffects;
        private void SetBypassReverbZones() => _audioSource.bypassReverbZones = _configData.Bypass.ReverbZones;
    }
}
