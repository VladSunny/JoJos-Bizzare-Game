using JJBG.Combat.Skills;
using JJBG.Combat.StarPlatinum.Skills;
using UnityEngine;

namespace JJBG.Controller
{
    public class SPController : MonoBehaviour
    {
        [Header("Skills")]
        [SerializeField] private Summon _summon;
        [SerializeField] private BasePunches _basePunches;
        [SerializeField] private BasePunches _finisher;

        private PlayerControls _playerControls;
        private GameObject _starPlatinum;

        private void Awake()
        {
            _playerControls = new PlayerControls();
        }

        public void Initialize(GameObject starPlatinum)
        {
            _starPlatinum = starPlatinum;
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
            if (_playerControls.StarPlatinum.Summon.triggered)
            {
                _summon.Activate();
            }

            if (_playerControls.StarPlatinum.BasePunches.triggered)
            {
                _basePunches.Activate();
            }

            if (_playerControls.StarPlatinum.Finisher.triggered)
            {
                _finisher.Activate();
            }
        }
    }
}
