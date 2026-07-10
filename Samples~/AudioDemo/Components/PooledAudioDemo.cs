using Rossoforge.Core.Audio;
using Rossoforge.Services;
using UnityEngine;

namespace Rossoforge.Audio.Samples.Demo
{
    public class PooledAudioDemo : MonoBehaviour
    {
        [SerializeField]
        private AudioConfigData _audioConfig;

        private IAudioService _audioService;

        public void Start()
        {
            _audioService = ServiceLocator.Get<IAudioService>();
        }

        public void OnPlayButtonClicked()
        {
            _audioService.PlayOneShot(_audioConfig, transform, transform.position, Space.World);
        }
    }
}
