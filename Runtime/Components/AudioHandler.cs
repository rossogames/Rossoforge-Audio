using Rossoforge.Core.Audio;
using Rossoforge.Utils.Logger;
using UnityEngine;

namespace Rossoforge.Audio.Components
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioHandler : MonoBehaviour
    {
        [SerializeReference]
        protected AudioConfigData _configData;

        private AudioConfigData _lastAppliedConfig;
        private AudioChannelData _currentChannel;
        protected AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }

        protected virtual void OnEnable()
        {
            Initialize();
        }
        protected virtual void OnDisable()
        {
            UnregisterEvents();
        }

        protected void Initialize()
        {
            if (_configData == null)
                return;

            if (CheckCurrentConfig())
                return;

            //--default--
            if (!SetChannel())
                return;

            if (_configData is IMixerAudioConfig mixerAudioConfig)
                SetMixerGroup(mixerAudioConfig);

            if (_configData is IPriorityAudioConfig priorityAudioConfig)
                SetPriority(priorityAudioConfig);

            //--main--
            SetClip();
            SetVolume(_currentChannel.Volume);
            SetPitch();
            SetMuteState(_currentChannel.IsMuted);
            SetLoop();
            RegisterEvents();

            //--Spatial Settings--
            if (_configData is ISpatialAudioConfig spatialAudioConfig)
            {
                SetSpatialBlend(spatialAudioConfig);
                SetStereoPan(spatialAudioConfig);
                SetReverbZoneMix(spatialAudioConfig);
                SetDopplerLevel(spatialAudioConfig);
                SetSpread(spatialAudioConfig);
                SetRolloffMode(spatialAudioConfig);
                SetMaxDistance(spatialAudioConfig);
                SetMinDistance(spatialAudioConfig);
            }

            //--Bypass Settings--
            if (_configData is IBypassAudioConfig bypassAudioConfi)
            {
                SetBypassEffects(bypassAudioConfi);
                SetBypassListenerEffects(bypassAudioConfi);
                SetBypassReverbZones(bypassAudioConfi);
            }
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
        private void SetMixerGroup(IMixerAudioConfig mixerAudioConfig) => _audioSource.outputAudioMixerGroup = mixerAudioConfig.MixerGroup;
        private void SetPriority(IPriorityAudioConfig priorityAudioConfig) => _audioSource.priority = priorityAudioConfig.Priority;

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
            if (_currentChannel != null)
            {
                _currentChannel.OnVolumeChanged -= SetVolume;
                _currentChannel.OnMutedChanged -= SetMuteState;
            }
        }
        private void AutoPlay()
        {
            if (_configData.Main.Autoplay)
                _audioSource.Play();
        }

        //--Spatial Settings--
        private void SetSpatialBlend(ISpatialAudioConfig spatialAudioConfig) => _audioSource.spatialBlend = spatialAudioConfig.Spatial.SpatialBlend;
        private void SetStereoPan(ISpatialAudioConfig spatialAudioConfig) => _audioSource.panStereo = spatialAudioConfig.Spatial.StereoPan;
        private void SetReverbZoneMix(ISpatialAudioConfig spatialAudioConfig) => _audioSource.reverbZoneMix = spatialAudioConfig.Spatial.ReverbZoneMix;
        private void SetDopplerLevel(ISpatialAudioConfig spatialAudioConfig) => _audioSource.dopplerLevel = spatialAudioConfig.Spatial.DopplerLevel;
        private void SetSpread(ISpatialAudioConfig spatialAudioConfig) => _audioSource.spread = spatialAudioConfig.Spatial.Spread;
        private void SetRolloffMode(ISpatialAudioConfig spatialAudioConfig)
        {
            if (spatialAudioConfig.Spatial.RolloffMode == AudioRolloffMode.Custom)
            {
                RossoLogger.Warning($"AudioConfigData '{_configData.name}': Custom Rolloff mode is not supported without curves. Falling back to Logarithmic.");
                _audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
            }
            else
            {
                _audioSource.rolloffMode = spatialAudioConfig.Spatial.RolloffMode;
            }
        }
        private void SetMinDistance(ISpatialAudioConfig spatialAudioConfig)
        {
            if (spatialAudioConfig.Spatial.MinDistance > spatialAudioConfig.Spatial.MaxDistance)
            {
                RossoLogger.Error($"AudioConfigData '{_configData.name}': Min Distance must be less than Max Distance.");
                return;
            }
            _audioSource.minDistance = spatialAudioConfig.Spatial.MinDistance;
        }
        private void SetMaxDistance(ISpatialAudioConfig spatialAudioConfig) => _audioSource.maxDistance = spatialAudioConfig.Spatial.MaxDistance;

        //--Bypass Settings--
        private void SetBypassEffects(IBypassAudioConfig bypassAudioConfig) => _audioSource.bypassEffects = bypassAudioConfig.Bypass.Effects;
        private void SetBypassListenerEffects(IBypassAudioConfig bypassAudioConfig) => _audioSource.bypassListenerEffects = bypassAudioConfig.Bypass.ListenerEffects;
        private void SetBypassReverbZones(IBypassAudioConfig bypassAudioConfig) => _audioSource.bypassReverbZones = bypassAudioConfig.Bypass.ReverbZones;
    }
}
