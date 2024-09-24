using Plugins.MonoCache;
using UnityEngine;

namespace WorldScene
{
    public class Environs : MonoCache
    {
        [SerializeField] private Transform _heroSpawnPoint;

        public Vector3 GetHeroSpawnPoint =>
            _heroSpawnPoint.position;
    }
}