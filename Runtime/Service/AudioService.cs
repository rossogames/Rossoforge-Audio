using Rossoforge.Core.Audio;
using Rossoforge.Core.Services;
using Rossoforge.Utils.Logger;

namespace Rossoforge.Audio.Services
{
    public class AudioService : IAudioService, IInitializable
    {
        private AudioServiceData _serviceData;

        public AudioService(AudioServiceData serviceData)
        {
            _serviceData = serviceData;
        }

        public void Initialize()
        {
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
    }
}
