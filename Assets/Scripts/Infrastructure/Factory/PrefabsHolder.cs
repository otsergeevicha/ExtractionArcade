using System;
using GameCamera;
using Player;
using Plugins.MonoCache;
using Reflex.Attributes;
using Reflex.Injectors;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class PrefabsHolder : MonoCache
    {
        [SerializeField] private MainCamera _mainCamera;
        [SerializeField] private Hero _hero;

        public MainCamera GetCamera =>
            _mainCamera;

        public Hero GetHero =>
            _hero;
    }
}