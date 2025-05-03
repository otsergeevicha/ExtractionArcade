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

        [Header("Rotation offset at A/D")] 
        [SerializeField] private float _strafeOffsetAngle = 45f;
        [SerializeField] private float _rotationSmoothSpeed = 180f;
        
        private IInputService _input;
        private float _verticalVelocity;
        private float _moveX;
        private float _moveY;
        private Vector3 _moveVector;

        public void Construct(IInputService inputService) =>
            _input = inputService;

        private void OnValidate() => 
            _controller ??= Get<CharacterController>();

        protected override void UpdateCached()
        {
            base.UpdateCached();
            HandleMovement(_input.GetMoveAxis);
        }

        private void HandleMovement(Vector2 moveAxis)
        {
            _moveX = moveAxis.x;
            _moveY = moveAxis.y;
            
            _animator.SetBool(Constants.HASH_HERO_IS_WALK, Mathf.Abs(_moveY) > Mathf.Epsilon);
            transform.rotation = Quaternion.Euler(0f, Mathf.MoveTowardsAngle(transform.eulerAngles.y, GetYaw(), _rotationSmoothSpeed * Time.deltaTime), 0f);
            _moveVector = transform.forward * _moveY;
            _moveVector.y = GetVerticalVelocity();
            _controller.Move(_moveVector * (Constants.HERO_SPEED * Time.deltaTime));
        }

        private float GetVerticalVelocity() => 
            _controller.isGrounded 
                ? Constants.DownforceValue 
                : GetVelocity();

        private float GetYaw() => 
            Mathf.Abs(_moveX) > Mathf.Epsilon && _moveY >= 0f 
                ? transform.eulerAngles.y + Mathf.Sign(_moveX) * _strafeOffsetAngle 
                : transform.eulerAngles.y;

        private float GetVelocity() => 
            _verticalVelocity += Constants.Gravity * Time.deltaTime;
    }
}