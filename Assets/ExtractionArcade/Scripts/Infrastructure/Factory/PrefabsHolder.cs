using ExtractionArcade.Scripts.GameCamera;
using ExtractionArcade.Scripts.Player;
using Plugins.MonoCache;
using UnityEngine;

namespace ExtractionArcade.Scripts.Infrastructure.Factory
{
    public class PrefabsHolder : MonoCache
    {
        [SerializeField] private Hero _hero;
        [SerializeField] private CameraFollow _cameraFollow;

        public Hero GetHero =>
            _hero;

        public CameraFollow GetCameraFollow =>
            _cameraFollow;
    }
}