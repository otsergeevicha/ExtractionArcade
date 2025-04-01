using Plugins.MonoCache;
using UnityEngine;

namespace ExtractionArcade.Scripts.Holders
{
    public class SpawnPointHolder : MonoCache
    {
        [SerializeField] private Transform _heroPoint;

        public Vector3 Hero =>
            _heroPoint.position;
    }
}