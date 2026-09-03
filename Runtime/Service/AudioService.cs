using Rossoforge.Audio.Components;
using Rossoforge.Audio.DataConfig;
using Rossoforge.Audio.Services;
using Rossoforge.Core.Pool;
using Rossoforge.Core.Services;
using Rossoforge.Services;
using Rossoforge.Utils.Logger;
using UnityEngine;

namespace Rossoforge.Audio.Service
{
    public class AudioService : IAudioService, IInitializable
    {
        private AudioDataService _serviceData;
        private IPoolService _poolService;

        public AudioService(AudioDataService serviceData)
        {
            _serviceData = serviceData;
        }

        public void Initialize()
        {
            _poolService = ServiceLocator.Get<IPoolService>();
        }

        public void SetChannelVolume(AudioChannelDataConfig channel, float volume)
        {
            if (channel == null)
            {
                RossoLogger.Error($"{nameof(SetChannelVolume)}: channel is null");
                return;
            }

            channel.Volume = volume;
        }

        public void SetChannelMute(AudioChannelDataConfig channel, bool isMuted)
        {
            if (channel == null)
            {
                RossoLogger.Error($"{nameof(SetChannelMute)}: channel is null");
                return;
            }

            channel.IsMuted = isMuted;
        }

        public void PlayOneShot(AudioDataConfig config, Transform parent, Vector3 position, Space relativeTo)
        {
            var audioSource = _poolService.Get<PooledAudioHandler>(
                _serviceData.AssetReferenceGenericAudioSource,
                parent,
                position,
                relativeTo
            );

            audioSource.Play(config);
        }
    }
}
