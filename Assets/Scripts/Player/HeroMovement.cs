using System;
using Plugins.MonoCache;
using Services.Inputs;
using UnityEngine;

namespace Player
{
    public class HeroMovement : MonoCache
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;

        private readonly float _rotationSpeed = 5.5f;
        private float _speed;


        private IInputService _input;
        private Camera _camera;
        private int _hashBlend;

        public void Construct(IInputService input, Camera cacheCamera, int heroSpeed, int hashBlend)
        {
            _hashBlend = hashBlend;
            _speed = heroSpeed;
            _camera = cacheCamera;
            _input = input;
            _input.OnControls();
        }

        private void OnValidate()
        {
            _animator ??= Get<Animator>();
            _rigidbody ??= Get<Rigidbody>();
        }

        protected override void FixedUpdateCached()
        {
            Vector3 movementDirection = Vector3.zero;
            _animator.SetFloat(_hashBlend, _input.MoveAxis.sqrMagnitude);

            if (_input.MoveAxis.sqrMagnitude > Single.Epsilon)
            {
                if (Mathf.Approximately(_input.MoveAxis.y, -1f))
                {
                    movementDirection = -_camera.transform.forward;
                }
                else
                {
                    movementDirection =
                        _camera.transform.TransformDirection(new Vector3(_input.MoveAxis.x, Single.Epsilon,
                            _input.MoveAxis.y));
                }

                movementDirection.y = 0f;
                _rigidbody.MovePosition(transform.position + movementDirection.normalized * (_speed * Time.deltaTime));
            }

            Rotate(movementDirection);
        }

        protected override void OnDisabled() =>
            _input.OffControls();

        private void Rotate(Vector3 targetDirection)
        {
            if (Mathf.Approximately(_input.MoveAxis.y, -1f))
                return;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection,
                _rotationSpeed * Time.deltaTime, 0.0f);
            newDirection.y = 0.0f;
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}