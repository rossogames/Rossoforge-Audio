using Rossoforge.Audio.Data;
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

        public void SetChannelVolume(IAudioChannelData channel, float volume)
        {
            if (channel == null)
            {
                RossoLogger.Error($"{nameof(SetChannelVolume)}: channel is null");
                return;
            }

            channel.SetVolume(volume);
        }

        public void SetChannelMute(IAudioChannelData channel, bool isMuted)
        {
            if (channel == null)
            {
                RossoLogger.Error($"{nameof(SetChannelMute)}: channel is null");
                return;
            }

            channel.SetMute(isMuted);
        }

        public void Play(IAudioConfigData config, Transform parent, Vector3 position, Space relativeTo)
        {
            var obj = _poolService.Get(
                _serviceData.AssetReference_GenericAudioSource,
                parent,
                position,
                relativeTo
            );

        }
    }
}
