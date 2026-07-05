using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Rossoforge.Audio.Data
{
    [CreateAssetMenu(fileName = nameof(AudioConfigData), menuName = "Rossoforge/Audio/Audio Config Data")]
    public class AudioConfigData : ScriptableObject
    {
        [field: SerializeField]
        public AudioChannelData Channel { get; private set; }

        [field: SerializeField]
        public AudioClip Clip { get; private set; }

        [field: SerializeField]
        public AudioMixerGroup MixerGroup { get; private set; }

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

        [field: Range(0f, 5f)]
        [field: SerializeField]
        public float DopplerLevel { get; private set; } = 1f;

        [field: Range(0f, 360f)]
        [field: SerializeField]
        public float Spread { get; private set; } = 0f;

        [field: SerializeField]
        public AudioRolloffMode VolumeRolloff { get; set; }

        [field: SerializeField]
        public float MinDistance { get; private set; } = 1f;

        [field: SerializeField]
        public float MaxDistance { get; private set; } = 500f;
    }
}
