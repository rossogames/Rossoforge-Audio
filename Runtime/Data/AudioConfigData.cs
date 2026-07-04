using System;
using UnityEngine;

namespace Rossoforge.Audio.Data
{
    [CreateAssetMenu(fileName = nameof(AudioConfigData), menuName = "Rossoforge/Audio/Audio Config Data")]
    public class AudioConfigData : ScriptableObject
    {
        [field: Header("Vínculo del Canal")]
        [field: SerializeField] 
        public AudioChannelData Channel { get; private set; }

        [field: Header("Configuración del Clip nativo")]
        [field: SerializeField]
        public AudioClip Clip { get; private set; }

        [field: Range(0f, 1f)]
        [field: SerializeField]
        public float BaseVolume { get; private set; } = 1f;

        [field: Range(0.5f, 1.5f)]
        [field: SerializeField]
        public float Pitch { get; private set; } = 1f;

        [field: SerializeField]
        public bool Loop { get; private set; } = false;
    }
}
