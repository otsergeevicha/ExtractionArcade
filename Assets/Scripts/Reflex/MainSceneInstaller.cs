using Plugins.MonoCache;
using Reflex.Core;
using Services.Factory;
using Services.Inputs;

namespace Reflex
{
    public class MainSceneInstaller : MonoCache, IInstaller
    {
        private IInputService _input;
        private IGameFactory _gameFactory;

        public void InstallBindings(ContainerBuilder descriptor) => 
            descriptor.OnContainerBuilt += LoadLevel;

        private void LoadLevel(Container container)
        {
            _input = container.Single<IInputService>();
            _gameFactory = container.Single<IGameFactory>();
            
            CreateGame();
        }

        private void CreateGame()
        {
            
        }
    }
}