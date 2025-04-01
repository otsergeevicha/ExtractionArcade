using ExtractionArcade.Scripts.Holders;
using Plugins.MonoCache;
using Reflex.Attributes;
using Reflex.Extensions;
using Reflex.Injectors;

namespace ExtractionArcade.Scripts.Player
{
    public class Hero : MonoCache
    {
        private SpawnPointHolder _spawnHolder;

        [Inject]
        private void Construct(SpawnPointHolder spawnHolder) =>
            _spawnHolder = spawnHolder;

        private void Start()
        {
            GameObjectInjector.InjectObject(gameObject, gameObject.scene.GetSceneContainer());
            transform.position = _spawnHolder.Hero;
        }
    }
}