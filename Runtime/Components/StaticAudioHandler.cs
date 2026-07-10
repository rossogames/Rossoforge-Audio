namespace Rossoforge.Audio.Components
{
    public class StaticAudioHandler : AudioHandler
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            _configData.OnPlayRequested += OnPlayRequest;
            _configData.OnStopRequested += OnStopRequest;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            _configData.OnPlayRequested -= OnPlayRequest;
            _configData.OnStopRequested -= OnStopRequest;
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
