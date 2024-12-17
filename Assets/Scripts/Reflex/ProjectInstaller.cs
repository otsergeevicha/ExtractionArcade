using Infrastructure.Factory;
using Inputs;
using Plugins.MonoCache;
using Reflex.Core;
using Services.Inputs;
using UnityEngine;

namespace Reflex
{
    public class ProjectInstaller : MonoCache, IInstaller
    {
        [SerializeField] private PrefabsHolder _prefabsHolder;
        
        public void InstallBindings(ContainerBuilder descriptor)
        {
            descriptor.AddSingleton(new InputService(), typeof(IInputService));
            descriptor.AddSingleton(_prefabsHolder);
        }
    }
}