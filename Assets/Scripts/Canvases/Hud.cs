using JoystickLogic;
using Plugins.MonoCache;
using Services.Inputs;
using UnityEngine;

namespace Canvases
{
    public class Hud : MonoCache
    {
        [SerializeField] private Joystick _joystick;
        
        public void Construct(Camera cashCamera, IInputService input) => 
            _joystick.Construct(cashCamera, input);
    }
}