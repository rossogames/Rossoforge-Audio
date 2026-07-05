using System;
using UnityEngine;

namespace Rossoforge.Audio.Data
{
    [CreateAssetMenu(fileName = nameof(AudioConfigData), menuName = "Rossoforge/Audio/Audio Config Data")]
    public class AudioConfigData : ScriptableObject
    {
        [field: SerializeField] 
        public AudioChannelData Channel { get; private set; }

        [field: SerializeField]
        public AudioClip Clip { get; private set; }

        [field: Range(0, 256)]
        [field: SerializeField]
        public byte Priority { get; private set; } = 128;

        [field: Range(0f, 1f)]
        [field: SerializeField]
        public float Volume { get; private set; } = 1f;

        [field: Range(-3f, 3f)]
        [field: SerializeField]
        public float Pitch { get; private set; } = 1f;

        [field: Range(-1f, 1f)]
        [field: SerializeField]
        public float StereoPan { get; private set; } = 0f;

        [field: Range(0f, 1f)]
        [field: SerializeField]
        public float SpatialBlend { get; private set; } = 0f;

        [field: Range(0f, 1.1f)]
        [field: SerializeField]
        public float ReverbZoneMix { get; private set; } = 1f;

        [field: SerializeField]
        public bool Mute { get; private set; } = false;

        [field: SerializeField]
        public bool BypassEffects { get; private set; } = false;

        [field: SerializeField]
        public bool BypassListenerEffects { get; private set; } = false;

        [field: SerializeField]
        public bool BypassReverbZones { get; private set; } = false;

        [field: SerializeField]
        public bool Loop { get; private set; } = false;

        [field: SerializeField]
        public bool Autoplay { get; private set; } = false;
    }
}
