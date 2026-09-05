using TMPro;
using UnityEngine;

namespace Rossoforge.Audio.Samples.Demo
{
    public class AudioPlayerDemo : MonoBehaviour
    {
        public TextMeshProUGUI _label;
        public AudioSource _audioSource;

        private bool _isPlaying;

        public void OnButtonClicked()
        {
            if (_isPlaying)
            {
                _audioSource.Stop();
                _label.text = "PLAY";
            }
            else
            {
                _audioSource.Play();
                _label.text = "STOP";
            }
            _isPlaying = !_isPlaying;
        }
    }
}