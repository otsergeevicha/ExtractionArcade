using ExtractionArcade.Scripts.Holders;
using ExtractionArcade.Scripts.Inputs;
using ExtractionArcade.Scripts.Services.Inputs;
using Plugins.MonoCache;
using Reflex.Attributes;
using Reflex.Extensions;
using Reflex.Injectors;
using UnityEngine;

namespace ExtractionArcade.Scripts.Player
{
    [RequireComponent(typeof(HeroMovement))]
    public class Hero : MonoCache
    {
        [SerializeField] private HeroMovement _heroMovement;
        
        private SpawnPointHolder _spawnHolder;
        private IInputService _inputService;

        [Inject]
        private void Construct(SpawnPointHolder spawnHolder, IInputService inputService)
        {
            _inputService = inputService;
            _spawnHolder = spawnHolder;
            _heroMovement.Construct(_inputService);
        }

        private void Start()
        {
            GameObjectInjector.InjectObject(gameObject, gameObject.scene.GetSceneContainer());
            transform.position = _spawnHolder.Hero;
        }

        private void OnValidate() => 
            _heroMovement ??= GetComponentInChildren<HeroMovement>();
    }
}