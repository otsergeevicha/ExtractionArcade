using ExtractionArcade.Scripts.Holders;
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
        [SerializeField] private Transform _rootCamera;
        
        private SpawnPointHolder _spawnHolder;
        private IInputService _inputService;

        public Transform GetCameraRoot =>
            _rootCamera;

        [Inject]
        private void Construct(SpawnPointHolder spawnHolder, IInputService inputService)
        {
            _inputService = inputService;
            _spawnHolder = spawnHolder;
        }

        private void Start()
        {
            GameObjectInjector.InjectObject(gameObject, gameObject.scene.GetSceneContainer());
            
            _heroMovement.Construct(_inputService);
            transform.position = _spawnHolder.Hero;
        }

        private void OnValidate() => 
            _heroMovement ??= GetComponentInChildren<HeroMovement>();
    }
}