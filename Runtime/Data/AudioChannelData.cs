using Rossoforge.Core.Audio;
using System;
using UnityEngine;

namespace Rossoforge.Audio.Data
{
    [CreateAssetMenu(fileName = nameof(AudioChannelData), menuName = "Rossoforge/Audio/Audio Channel Data")]
    public class AudioChannelData : ScriptableObject, IAudioChannelData
    {
        public event Action<float> OnVolumeChanged;
        public event Action<bool> OnActiveChanged;

        [field: Range(0f, 1f)]
        [field: SerializeField]
        public float Volume { get; private set; }

        [field: SerializeField]
        public bool IsActive { get; private set; }

        public void SetVolume(float newVolume)
        {
            newVolume = Mathf.Clamp01(newVolume);

            if (Mathf.Approximately(Volume, newVolume))
                return;

            Volume = newVolume;

            if (!IsActive)
                return;

            OnVolumeChanged?.Invoke(Volume);
        }

        public void SetActive(bool active)
        {
            if (IsActive == active)
                return;

            IsActive = active;
            OnActiveChanged?.Invoke(active);
        }
    }
}
