using UnityEngine;

using JJBG.Combat.Standless.Skills;

namespace JJBG.Controller
{
    public class PlayerCombatController : MonoBehaviour
    {
        [Header("Skills")]
        [SerializeField] private BasePunches _basePunches;

        private PlayerControls _playerControls;

        private void Awake() {
            _playerControls = new PlayerControls();
        }

        private void OnEnable() {
            _playerControls.Enable();
        }

        private void OnDisable() {
            _playerControls.Disable();
        }

        private void Update() {
            if (_playerControls.Player.BasePunches.triggered) {
                _basePunches.Activate();
            }
        }
    }
}
