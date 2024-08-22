using JJBG.Combat.StarPlatinum.Skills;
using UnityEngine;

namespace JJBG.Controller
{
    public class SPController : MonoBehaviour
    {
        private Summon _summon;

        private PlayerControls _playerControls;
        private GameObject _starPlatinum;

        private void Awake()
        {
            _playerControls = new PlayerControls();
        }

        public void Initialize(GameObject starPlatinum) {
            _starPlatinum = starPlatinum;
        }

        private void Start() {
            _summon = _starPlatinum.GetComponentInChildren<Summon>();
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
            if (_playerControls.StarPlatinum.Summon.triggered) {
                _summon.Activate();
            }
        }
    }
}
