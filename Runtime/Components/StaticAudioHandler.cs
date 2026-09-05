namespace Rossoforge.Audio.Components
{
    public class StaticAudioHandler : AudioHandler
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            _audioDataConfig.OnPlayRequested += OnPlayRequest;
            _audioDataConfig.OnStopRequested += OnStopRequest;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            _audioDataConfig.OnPlayRequested -= OnPlayRequest;
            _audioDataConfig.OnStopRequested -= OnStopRequest;
        }

        private void OnPlayRequest()
        {
            _audioSource.Play();
        }
        private void OnStopRequest()
        {
            _audioSource.Stop();
        }
    }
}
