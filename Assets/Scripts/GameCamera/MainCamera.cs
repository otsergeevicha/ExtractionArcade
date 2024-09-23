using System;
using Cinemachine;
using Plugins.MonoCache;
using Services.Inputs;
using UnityEngine;

namespace GameCamera
{
    public class MainCamera : MonoCache
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineVirtualCamera _mainFollowingCamera;
        
        private IInputService _input;
        private float _targetYaw;
        private float _targetPitch;
        private Transform _heroRoot;

        public void Construct(Transform heroRoot, IInputService input)
        {
            _heroRoot = heroRoot;
            _input = input;
            _mainFollowingCamera.Follow = _heroRoot;
        }

        public Camera GetCacheCamera => 
            _camera;
    }
}