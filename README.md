# Rosso Games

<table>
  <tr>
    <td><img src="https://github.com/rossogames/Rossoforge-Audio/blob/main/logo.png?raw=true" alt="Rossoforge" width="64"/></td>
    <td><h2>Rossoforge - Audio</h2></td>
  </tr>
</table>

**Rossoforge-Audio** A lightweight and data-driven audio management system for RossoForge. Features decoupled audio channels and configuration-based audio sources.

#
**Version:** Unity 6 or higher

#
```csharp
// Setup the Service Locator instance
ServiceLocator.SetLocator(new DefaultServiceLocator());

// Instantiate Core Services
var eventService = new EventService();
var poolService = new PoolService();
var audioService = new AudioService(_audioServiceData); // Requires your AudioServiceData asset

// Register Services to the Locator
ServiceLocator.Register<IEventService>(eventService);
ServiceLocator.Register<IPoolService>(poolService);
ServiceLocator.Register<IAudioService>(audioService);

// Initialize all registered services
ServiceLocator.Initialize();

// Pooled One-Shot audio
_audioService = ServiceLocator.Get<IAudioService>();
_audioService.PlayOneShot(_audioConfig, transform, transform.position, Space.World);

// Audio channel control
_audioService = ServiceLocator.Get<IAudioService>();
_audioService.SetChannelVolume(_channel, volume);
_audioService.SetChannelMute(_channel, isMuted);
```

#
This package is part of the **Rossoforge** suite, designed to streamline and enhance Unity development workflows.

Developed by Agustin Rosso
https://www.linkedin.com/in/rossoagustin/
