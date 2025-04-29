using System;
using ExtractionArcade.Scripts.Services.Inputs;
using Plugins.MonoCache;
using UnityEngine;

namespace ExtractionArcade.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroMovement : MonoCache
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private Animator _animator;

        private IInputService _input;
        private bool _isMove;

        public void Construct(IInputService inputService) =>
            _input = inputService;

        private void OnValidate() =>
            _controller ??= Get<CharacterController>();

        protected override void UpdateCached()
        {
            base.UpdateCached();
            BaseLogic(_input.GetMoveAxis);
        }

        private void BaseLogic(Vector2 moveAxis)
        {
            Vector3 movementDirection = Vector3.zero;

            if (moveAxis.sqrMagnitude > Single.Epsilon)
            {
                _animator.SetBool(Constants.HASH_HERO_IS_WALK, true);
                movementDirection = new Vector3(moveAxis.x, Single.Epsilon, moveAxis.y);
            }
            else
            {
                _animator.SetBool(Constants.HASH_HERO_IS_WALK, false);
            }

            Rotate(movementDirection.normalized);
            movementDirection += Physics.gravity;
            _controller.Move(movementDirection * (Constants.HERO_SPEED * Time.deltaTime));
        }

        private void Rotate(Vector3 targetDirection)
        {
            if (targetDirection == Vector3.zero) 
                return;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Constants.HERO_ROTATE_SPEED * Time.deltaTime);
        }
    }
}