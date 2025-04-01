using ExtractionArcade.Scripts.Inputs;
using ExtractionArcade.Scripts.Services.Inputs;
using Plugins.MonoCache;
using Reflex.Core;

namespace ExtractionArcade.Scripts.Reflex
{
    public class ProjectInstaller : MonoCache, IInstaller
    {
        
        public void InstallBindings(ContainerBuilder descriptor)
        {
            descriptor.AddSingleton(new InputService(), typeof(IInputService));
        }
    }
}