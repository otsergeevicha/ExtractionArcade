using Infrastructure.Factory;
using Plugins.MonoCache;
using Reflex.Core;
using Services.Factory;

namespace Reflex
{
    public class MainSceneInstaller : MonoCache, IInstaller
    {
        private ContainerBuilder _descriptor;

        public void InstallBindings(ContainerBuilder descriptor)
        {
            _descriptor = descriptor;
            descriptor.OnContainerBuilt += LoadLevel;
        }

        private void LoadLevel(Container container)
        {
            var paths = container.Single<PrefabsHolder>();

            IGameFactory factory = new GameFactory();
            
            _descriptor.AddSingleton(factory.CreateHero(paths.GetHero));
            _descriptor.AddSingleton(factory.CreateMainCamera(paths.GetCamera));
        }
    }
}