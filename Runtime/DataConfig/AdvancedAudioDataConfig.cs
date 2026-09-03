using Rossoforge.Core.Audio;
using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Rossoforge.Audio.DataConfig
{
    [CreateAssetMenu(fileName = nameof(AdvancedAudioDataConfig), menuName = "Rossoforge/Data Config/Audio/Advanced")]
    public class AdvancedAudioDataConfig : AudioDataConfig,
        IMixerAudioConfig,
        IPriorityAudioConfig,
        ISpatialAudioConfig,
        IBypassAudioConfig
    {
        [field: SerializeField]
        public AudioMixerGroup MixerGroup { get; private set; }

        [field: Range(0, 256)]
        [field: SerializeField]
        public byte Priority { get; private set; } = 128;

        [field: SerializeField]
        public SpatialSettings Spatial { get; private set; }

        [field: SerializeField]
        public BypassSettings Bypass { get; private set; }
    }
}
