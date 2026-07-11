using Rossoforge.Pool.Data;
using UnityEngine;

namespace Rossoforge.Audio.Services
{
    [CreateAssetMenu(fileName = nameof(AudioServiceData), menuName = "Rossoforge/Audio/Service Data")]
    public class AudioServiceData : ScriptableObject
    {
        [field: SerializeField]
        public PooledGameobjectData AssetReferenceGenericAudioSource { get; private set; }
    }
}
