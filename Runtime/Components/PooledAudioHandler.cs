using Rossoforge.Audio.DataConfig;
using Rossoforge.Utils.Logger;

namespace Rossoforge.Audio.Components
{
    public class PooledAudioHandler : AudioHandler
    {
        private bool _isTracking;

        protected override void OnEnable()
        {
            // Do not call base.OnEnable() 
            // Will be initialized by the audio service after spawned
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            _isTracking = false;
        }

        private void Update()
        {
            CheckIfFinished();
        }

        public void Play(AudioDataConfig dataConfig)
        {
            _audioDataConfig = dataConfig;
            Initialize();

            if (_audioDataConfig.Main.Loop)
            {
                RossoLogger.Error($"{nameof(PooledAudioHandler)} cannot play the Config '{_audioDataConfig.name}' because 'Loop' is enabled. One-Shot pooled sounds must not loop, as they will never return to the pool.");
                return;
            }

            _isTracking = true;

            if (!_audioDataConfig.Main.Autoplay)
                _audioSource.Play(); // Play the audio if Autoplay is disabled, since Initialize() will not play it automatically
        }

        private void CheckIfFinished()
        {
            if (!_isTracking)
                return;

            if (!_audioSource.isPlaying)
            {
                _isTracking = false;
                gameObject.SetActive(false); // Return to pool
            }
        }
    }
}
