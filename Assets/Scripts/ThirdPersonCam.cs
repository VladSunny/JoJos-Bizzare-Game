using UnityEngine;
using UnityEngine.InputSystem;

namespace JJBG.Camera
{
    public class ThirdPersonCam : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _orientation;
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _playerObj;
        [SerializeField] private Transform _combatLookAt;
        [SerializeField] private Rigidbody _rigidbody;

        [Header("Settings")]
        [SerializeField] private float _rotationSpeed = 7f;
        [SerializeField] private float _updateOrientationSpeed = 10f;
        [SerializeField] private CameraStyle _currentStyle = CameraStyle.Basic;

        private enum CameraStyle {
            Basic,
            Combat
        }

        private void Awake() {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update() {
            if (_currentStyle == CameraStyle.Basic) {
                Vector3 viewDir = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z);
                _orientation.forward = Vector3.Slerp(_orientation.forward, viewDir.normalized, Time.deltaTime * _updateOrientationSpeed);

                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                Vector3 inputDir = _orientation.forward * verticalInput + _orientation.right * horizontalInput;

                if (inputDir != Vector3.zero)
                    _playerObj.forward = Vector3.Slerp(
                        _playerObj.forward,
                        inputDir.normalized,
                        Time.deltaTime * _rotationSpeed
                    );
            }
            else if (_currentStyle == CameraStyle.Combat) {
                Vector3 dirToCombatLookAt = _combatLookAt.position - new Vector3(transform.position.x, _combatLookAt.position.y, transform.position.z);
                _orientation.forward = Vector3.Slerp(_orientation.forward, dirToCombatLookAt.normalized, Time.deltaTime * _updateOrientationSpeed);
            
                _playerObj.forward = Vector3.Slerp(
                    _playerObj.forward,
                    dirToCombatLookAt.normalized,
                    Time.deltaTime * _updateOrientationSpeed
                );
            }
        }
    }
}
