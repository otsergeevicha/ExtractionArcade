using Plugins.MonoCache;
using UnityEngine;

namespace WorldScene
{
    public class Environs : MonoCache
    {
        [SerializeField] private Transform _heroSpawnPoint;
        [SerializeField] private Transform _itemsSpawnPoint;

        public Vector3 GetHeroSpawnPoint =>
            _heroSpawnPoint.position;
        
        public Vector3 GetItemsSpawnPoint =>
            _itemsSpawnPoint.position;
    }
}