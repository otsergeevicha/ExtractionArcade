using Plugins.MonoCache;
using UnityEngine;

namespace Player
{
    public class Hero : MonoCache
    {
        [SerializeField] private Transform _rootCamera;

        public Transform GetRootCamera =>
            _rootCamera;
    }
}