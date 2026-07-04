using Rossoforge.Core.Audio;
using System;
using UnityEngine;

namespace Rossoforge.Audio.Data
{
    [CreateAssetMenu(fileName = nameof(AudioChannelData), menuName = "Rossoforge/Audio/Audio Channel Data")]
    public class AudioChannelData : ScriptableObject, IAudioChannelData
    {
        public event Action<float> OnVolumeChanged;
        public event Action<bool> OnMutedChanged;

        [field: Range(0f, 1f)]
        [field: SerializeField]
        public float Volume { get; private set; }

        [field: SerializeField]
        public bool IsMuted { get; private set; }

        public void SetVolume(float newVolume)
        {
            newVolume = Mathf.Clamp01(newVolume);

            if (Mathf.Approximately(Volume, newVolume))
                return;

            Volume = newVolume;
            OnVolumeChanged?.Invoke(Volume);
        }

        public void SetMute(bool isMuted)
        {
            if (IsMuted == isMuted)
                return;

            IsMuted = isMuted;
            OnMutedChanged?.Invoke(isMuted);
        }
    }
}
