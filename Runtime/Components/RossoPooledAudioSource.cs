using Rossoforge.Core.Audio;

namespace Rossoforge.Audio.Components
{
    public class RossoPooledAudioSource : RossoAudioSource
    {
        private bool _isTracking;

        protected override void OnEnable()
        {
            // Do not call base.OnEnable() 
            // Will be initialized by the pool service when spawned
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

        public void Play(AudioConfigData configData)
        {
            _configData = configData;
            Initialize();

            _isTracking = _configData.Main.Autoplay && !_configData.Main.Loop;
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
