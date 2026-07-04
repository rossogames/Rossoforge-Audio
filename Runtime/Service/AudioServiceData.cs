using Rossoforge.Audio.Data;
using UnityEngine;

namespace Rossoforge.Audio.Services
{
    [CreateAssetMenu(fileName = nameof(AudioServiceData), menuName = "Rossoforge/Audio/Service Data")]
    public class AudioServiceData : ScriptableObject
    {
        [field: SerializeField]
        public AudioChannelData[] Channels { get; private set; }
    }
}
