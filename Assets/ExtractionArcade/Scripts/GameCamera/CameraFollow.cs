using Cinemachine;
using ExtractionArcade.Scripts.Player;
using Plugins.MonoCache;
using Reflex.Attributes;
using Reflex.Extensions;
using Reflex.Injectors;
using UnityEngine;

namespace ExtractionArcade.Scripts.GameCamera
{
    public class CameraFollow : MonoCache
    {
        [SerializeField] private CinemachineVirtualCamera _cameraFollow;
        [SerializeField] private CinemachineVirtualCamera _zoomFollow;

        [SerializeField] private Camera _camera;
        
        private bool _isShowMarker;
        private bool _isShowBoss;
        private Transform _cameraRoot;

        [Inject]
        public void Construct(Hero hero) =>
            _cameraRoot = hero.GetCameraRoot;
        
        private void OnValidate() => 
            _camera ??= GetComponent<Camera>();

        private void Start()
        {
            GameObjectInjector.InjectObject(gameObject, gameObject.scene.GetSceneContainer());

            _cameraFollow.Follow = _cameraRoot;
            _zoomFollow.Follow = _cameraRoot;

            _cameraFollow.LookAt = _cameraRoot;
            _zoomFollow.LookAt = _cameraRoot;
            
            OffZoom();
        }

        public void OnZoom()
        {
            _zoomFollow.gameObject.SetActive(true);
            _cameraFollow.gameObject.SetActive(false);
        }

        public void OffZoom()
        {
            _cameraFollow.gameObject.SetActive(true);
            _zoomFollow.gameObject.SetActive(false);
        }
    }
}