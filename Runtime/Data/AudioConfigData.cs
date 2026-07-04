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

        [field: Range(0f, 1f)]
        [field: SerializeField]
        public float BaseVolume { get; private set; } = 1f;

        [field: Range(-3f, 3f)]
        [field: SerializeField]
        public float Pitch { get; private set; } = 1f;

        [field: SerializeField]
        public bool Loop { get; private set; } = false;
    }
}
