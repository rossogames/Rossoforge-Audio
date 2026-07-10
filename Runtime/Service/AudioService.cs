using Rossoforge.Audio.Components;
using Rossoforge.Core.Audio;
using Rossoforge.Core.Pool;
using Rossoforge.Core.Services;
using Rossoforge.Services;
using Rossoforge.Utils.Logger;
using UnityEngine;

namespace Rossoforge.Audio.Services
{
    public class AudioService : IAudioService, IInitializable
    {
        private AudioServiceData _serviceData;
        private IPoolService _poolService;

        public AudioService(AudioServiceData serviceData)
        {
            _serviceData = serviceData;
        }

        public void Initialize()
        {
            _poolService = ServiceLocator.Get<IPoolService>();
        }

        public void SetChannelVolume(AudioChannelData channel, float volume)
        {
            if (channel == null)
            {
                RossoLogger.Error($"{nameof(SetChannelVolume)}: channel is null");
                return;
            }

            channel.Volume = volume;
        }

        public void SetChannelMute(AudioChannelData channel, bool isMuted)
        {
            if (channel == null)
            {
                RossoLogger.Error($"{nameof(SetChannelMute)}: channel is null");
                return;
            }

            channel.IsMuted = isMuted;
        }

        public void PlayOneShot(AudioConfigData config, Transform parent, Vector3 position, Space relativeTo)
        {
            var audioSource = _poolService.Get<PooledAudioHandler>(
                _serviceData.AssetReference_GenericAudioSource,
                parent,
                position,
                relativeTo
            );

            audioSource.Play(config);
        }
    }
}
