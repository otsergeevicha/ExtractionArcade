using ExtractionArcade.Scripts.Player;
using Plugins.MonoCache;
using UnityEngine;

namespace ExtractionArcade.Scripts.Infrastructure.Factory
{
    public class PrefabsHolder : MonoCache
    {
        [SerializeField] private Hero _hero;

        public Hero GetHero =>
            _hero;
    }
}