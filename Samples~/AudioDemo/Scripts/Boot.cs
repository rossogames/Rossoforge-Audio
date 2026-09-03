using Rossoforge.Audio.Service;
using Rossoforge.Core.Events;
using Rossoforge.Core.Pool;
using Rossoforge.Events.Service;
using Rossoforge.Pool.Service;
using Rossoforge.Services;
using UnityEngine;

namespace Rossoforge.Audio.Samples.Demo
{
    public class Boot : MonoBehaviour
    {
        [SerializeField]
        private AudioDataService _audioServiceData;

        private void Awake()
        {
            // Setup
            ServiceLocator.SetLocator(new DefaultServiceLocator());

            var eventService = new EventService();
            var poolService = new PoolService();
            var audioService = new AudioService(_audioServiceData);

            ServiceLocator.Register<IEventService>(eventService);
            ServiceLocator.Register<IPoolService>(poolService);
            ServiceLocator.Register<IAudioService>(audioService);
            ServiceLocator.Initialize();
        }
    }
}