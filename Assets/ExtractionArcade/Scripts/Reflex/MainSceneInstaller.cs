using ExtractionArcade.Scripts.Holders;
using ExtractionArcade.Scripts.Infrastructure.Factory;
using ExtractionArcade.Scripts.Player;
using ExtractionArcade.Scripts.Services.Factory;
using Plugins.MonoCache;
using Reflex.Core;
using Reflex.Injectors;
using UnityEngine;

namespace ExtractionArcade.Scripts.Reflex
{
    public class MainSceneInstaller : MonoCache, IInstaller
    {
        [SerializeField] private PrefabsHolder _prefabHolder;
        [SerializeField] private SpawnPointHolder _spawnPointHolder;
        
        
        public void InstallBindings(ContainerBuilder descriptor)
        {
            IGameFactory factory = new GameFactory();
            
            descriptor.AddSingleton(_spawnPointHolder, typeof(SpawnPointHolder));
            descriptor.AddSingleton(factory.CreateHero(_prefabHolder.GetHero), typeof(Hero));
        }
    }
}