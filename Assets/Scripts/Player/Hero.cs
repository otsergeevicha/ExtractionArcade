using Player.Module.Parent;
using Plugins.MonoCache;
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
        
        public HeroModule HeroModule { get; private set; }

        public Transform GetRootCamera =>
            _rootCamera;

        public void Construct(IInputService input, Camera cacheCamera, HeroData heroData,
            Vector3 newPoint)
        {
            SetPosition(newPoint);
            
            HeroModule = new HeroModule(heroData);
            _heroMovement.Construct(input, cacheCamera, heroData.Speed, heroData.Blend);
        }

        private void OnValidate() => 
            _heroMovement ??= Get<HeroMovement>();

        private void SetPosition(Vector3 point) => 
            transform.position = point;
    }
}