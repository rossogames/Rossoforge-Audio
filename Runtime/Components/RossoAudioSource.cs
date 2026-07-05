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
        private AudioConfigData _lastAppliedConfig;

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
            UnregisterEvents();
        }

        private void Initialize()
        {
            if (_configData == null)
                return;

            if (CheckCurrentConfig())
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
            RegisterEvents();

            //--Spatial Settings--
            SetSpatialBlend();
            SetStereoPan();
            SetReverbZoneMix();
            SetDopplerLevel();
            SetSpread();
            SetRolloffMode();
            SetMaxDistance();
            SetMinDistance();

            //--Bypass Settings--
            SetBypassEffects();
            SetBypassListenerEffects();
            SetBypassReverbZones();

            AutoPlay();

            _lastAppliedConfig = _configData;
        }

        private bool CheckCurrentConfig()
        {
            if (_configData == _lastAppliedConfig && _currentChannel != null)
            {
                SetVolume(_currentChannel.Volume);
                SetMuteState(_currentChannel.IsMuted);
                RegisterEvents();
                AutoPlay();
                return true;
            }
            return false;
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
        private void RegisterEvents()
        {
            _currentChannel.OnVolumeChanged += SetVolume;
            _currentChannel.OnMutedChanged += SetMuteState;
        }
        private void UnregisterEvents()
        {
            _currentChannel.OnVolumeChanged -= SetVolume;
            _currentChannel.OnMutedChanged -= SetMuteState;
        }
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
        private void SetRolloffMode()
        {
            if (_configData.Spatial.RolloffMode == AudioRolloffMode.Custom)
            {
                RossoLogger.Warning($"AudioConfigData '{_configData.name}': Custom Rolloff mode is not supported without curves. Falling back to Logarithmic.");
                _audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
            }
            else
            {
                _audioSource.rolloffMode = _configData.Spatial.RolloffMode;
            }
        }
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
