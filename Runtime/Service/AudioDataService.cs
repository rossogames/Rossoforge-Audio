using Rossoforge.Pool.DataConfig;
using UnityEngine;

namespace Rossoforge.Audio.Service
{
    [CreateAssetMenu(fileName = nameof(AudioDataService), menuName = "Rossoforge/Data Service/Audio")]
    public class AudioDataService : ScriptableObject
    {
        [field: SerializeField]
        public PooledGameobjectDataConfig AssetReferenceGenericAudioSource { get; private set; }
    }
}
