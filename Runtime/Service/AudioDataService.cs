using Rossoforge.Pool.Data;
using UnityEngine;

namespace Rossoforge.Audio.Services
{
    [CreateAssetMenu(fileName = nameof(AudioDataService), menuName = "Rossoforge/Service Data/Audio")]
    public class AudioDataService : ScriptableObject
    {
        [field: SerializeField]
        public PooledGameobjectData AssetReferenceGenericAudioSource { get; private set; }
    }
}
