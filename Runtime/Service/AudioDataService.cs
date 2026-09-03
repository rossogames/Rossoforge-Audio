using Rossoforge.Pool.Data;
using UnityEngine;

namespace Rossoforge.Audio.Service
{
    [CreateAssetMenu(fileName = nameof(AudioDataService), menuName = "Rossoforge/Data Service/Audio")]
    public class AudioDataService : ScriptableObject
    {
        [field: SerializeField]
        public PooledGameobjectData AssetReferenceGenericAudioSource { get; private set; }
    }
}
