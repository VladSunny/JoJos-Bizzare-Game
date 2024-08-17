using UnityEngine;

using JJBG.Movement;
using JJBG.Camera;

namespace JJBG.Controller
{
    [RequireComponent(typeof(CharacterMovement))]

    public class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] ThirdPersonCam _thirdPersonCam;
        [SerializeField] Transform _orientation;

        private PlayerControls _playerControls;
        private CharacterMovement _characterMovement;

        private Vector2 _moveInput;

        private void Awake()
        {
            _playerControls = new PlayerControls();
            _characterMovement = GetComponent<CharacterMovement>();
        }

        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void OnDisable()
        {
            _playerControls.Disable();
        }

        private void Update()
        {
            PlayerInput();
        }

        private void FixedUpdate()
        {
            Vector3 moveDirection = _orientation.forward * _moveInput.y + _orientation.right * _moveInput.x;

            _characterMovement.Move(moveDirection.normalized);
        }

        private void PlayerInput()
        {
            _moveInput = _playerControls.Player.Movement.ReadValue<Vector2>();

            if (_playerControls.Player.Jump.inProgress)
            {
                _characterMovement.Jump();
            }

            if (_playerControls.Player.ChangeCameraStyle.triggered)
            {
                _thirdPersonCam.NextCameraStyle();
            }

            if (_playerControls.Player.SetRunning.triggered)
            {
                if (_characterMovement.GetMovementState() == CharacterMovement.MovementStates.Walking)
                {
                    _characterMovement.ChangeMovementState(CharacterMovement.MovementStates.Running);
                }
                else if (_characterMovement.GetMovementState() == CharacterMovement.MovementStates.Running)
                {
                    _characterMovement.ChangeMovementState(CharacterMovement.MovementStates.Walking);
                }
            }
        }
    }
}
