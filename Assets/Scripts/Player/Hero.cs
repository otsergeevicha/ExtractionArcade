using System;
using Infrastructure.Factory;
using Plugins.MonoCache;
using Reflex.Attributes;
using Reflex.Core;
using Reflex.Extensions;
using Reflex.Injectors;
using Services.Inputs;
using SO;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(HeroMovement))]
    public class Hero : MonoCache
    {
        [SerializeField] private Transform _rootCamera;
        [SerializeField] private HeroMovement _heroMovement;

        [Inject] private IInputService _input;
        
        private void Start() => 
            GameObjectInjector.InjectSingle(gameObject, gameObject.scene.GetSceneContainer());

        public Transform GetRootCamera =>
            _rootCamera;

        public void Construct(IInputService input, Camera cacheCamera, HeroData heroData,
            Vector3 newPoint)
        {
            SetPosition(newPoint);
            _heroMovement.Construct(input, cacheCamera, heroData.Speed, heroData.Blend);
        }

        private void OnValidate() => 
            _heroMovement ??= Get<HeroMovement>();

        private void SetPosition(Vector3 point) => 
            transform.position = point;
    }
}