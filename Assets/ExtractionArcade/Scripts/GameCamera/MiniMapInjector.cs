using ExtractionArcade.Scripts.Player;
using Plugins.MonoCache;
using Reflex.Attributes;
using UnityEngine;

namespace ExtractionArcade.Scripts.GameCamera
{
    public class MiniMapInjector : MonoCache
    {
        [SerializeField] private bl_MiniMap _map;
        private Transform _transformHero;

        [Inject]
        private void Construct(Hero hero) => 
            _transformHero = hero.transform;

        private void OnValidate() => 
            _map ??= GetComponent<bl_MiniMap>();

        private void Start() => 
            _map.Target = _transformHero;
    }
}