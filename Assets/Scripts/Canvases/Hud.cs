using System;
using Canvases.Views;
using JoystickLogic;
using Player.Module.Parent;
using Plugins.MonoCache;
using Reflex;
using Services.Inputs;
using UnityEngine;
using UnityEngine.UI;

namespace Canvases
{
    public class Hud : MonoCache
    {
        [SerializeField] private Slider _healthView;
        [SerializeField] private Joystick _joystick;
        
        private HeroHealthView _heroHealthView;
        private IInputService _input;

        public event Action<string> OpenedInventory;
        
        public void Construct(Coroutines coroutine, Camera cashCamera, IInputService input, HeroModule heroModule)
        {
            _input = input;
            _joystick.Construct(cashCamera, input);
            _heroHealthView = new HeroHealthView(coroutine,_healthView, heroModule.HeroHealth);
        }

        public void OnClickInventory(string ownerID)
        {
            OpenedInventory?.Invoke(ownerID);
            _input.OffControls();
        }

        protected override void OnDisabled() => 
            _heroHealthView.Dispose();
    }
}