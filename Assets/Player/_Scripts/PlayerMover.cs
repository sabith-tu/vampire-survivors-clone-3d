using System;
using SABI.SOA;
using UnityEngine;
using UnityEngine.Events;

namespace SABI.Player
{
    public class PlayerMover : MonoBehaviour
    {
        public CharacterController controller;
        [SerializeField] private FloatValueSO moveSpeed;
        [SerializeField] public bool alwaysRun;
        private Vector3 _moveDirection;
        private Vector3 _previewsMoveDirection;
        private Vector2 _moveInput;


        public void SetInputVector(Vector2 input) => _moveInput = input;


        [SerializeField] private AllPlayerState _currentPlayerState = AllPlayerState.non;

        [SerializeField] private UnityEvent OnStateEnterIdle;
        [SerializeField] private UnityEvent OnStateEnterRunning;


        private void Update()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (_moveInput == Vector2.zero)
            {
                if (alwaysRun == false) DoSetPlayerState(AllPlayerState.Idle);
                else AlwaysRunMovement();
            }
            else
            {
                _moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
                _moveDirection = _moveDirection.normalized;
                //_moveDirection = new Vector2(_moveDirection.x * 0.001f, _moveDirection.y * 0.001f);

                //_moveDirection += transform.forward;
                _moveDirection *= moveSpeed.GetValue();

                //HandleRotation(new Vector3(_moveInput.x * 0.001f, 0, _moveInput.y * 0.001f));

                controller.SimpleMove(_moveDirection);

                DoSetPlayerState(AllPlayerState.Running);
            }
        }

        private void HandleRotation(Vector3 lookAt)
        {
            float targetAngle = Mathf.Atan2(lookAt.x, lookAt.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }

        void AlwaysRunMovement()
        {
            Vector3 transformForward = transform.forward;
            _moveDirection = transformForward;
            _moveDirection *= moveSpeed.GetValue();
            _moveDirection += transformForward;
            controller.SimpleMove(_moveDirection);
            DoSetPlayerState(AllPlayerState.Running);
        }

        public void DoSetPlayerState(AllPlayerState newState)
        {
            if (_currentPlayerState == newState) return;
            _currentPlayerState = newState;

            switch (_currentPlayerState)
            {
                case AllPlayerState.Idle:
                    OnStateEnterIdle.Invoke();
                    break;
                case AllPlayerState.Running:
                    OnStateEnterRunning.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}

public enum AllPlayerState
{
    non,
    Idle,
    Running
}