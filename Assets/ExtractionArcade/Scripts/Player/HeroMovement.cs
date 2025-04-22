using ExtractionArcade.Scripts.Services.Inputs;
using Plugins.MonoCache;
using UnityEngine;

namespace ExtractionArcade.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroMovement : MonoCache
    {
        [SerializeField] private CharacterController _characterController;
        
        private IInputService _inputService;

        public void Construct(IInputService inputService) => 
            _inputService = inputService;

        private void Start()
        {
            //_inputService.
        }

        private void OnValidate() => 
            _characterController ??= Get<CharacterController>();
    }
}