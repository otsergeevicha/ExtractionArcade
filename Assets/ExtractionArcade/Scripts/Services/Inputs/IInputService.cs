using System;
using UnityEngine;

namespace ExtractionArcade.Scripts.Services.Inputs
{
    public interface IInputService
    {
        event Action OnJoystick;
        event Action OffJoystick;
        Vector2 GetMoveAxis { get; }
        Vector2 TouchJoystick { get; }
        void OnMove(Action<bool> onMove);
        void OffMove(Action<bool> onMove);
        void OnControls();
        void OffControls();
    }
}