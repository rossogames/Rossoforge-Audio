using Rossoforge.Core.Services;

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
    }
}
