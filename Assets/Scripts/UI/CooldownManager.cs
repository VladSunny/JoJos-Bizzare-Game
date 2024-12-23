using UnityEngine;

using JJBG.Combat;
using TMPro;

namespace JJBG.UI
{
    public class CooldownManager : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private GameObject _cooldownUIPrefab;
        [SerializeField] private GameObject _parentObject;

        [Header("Settings")]
        [SerializeField] private string _actionName;

        private ISkillController _skillController;

        private void Awake() {
            _skillController = GetComponent<ISkillController>();
        }

        private void OnEnable() {
            _skillController.onUsed += ShowUI;
        }

        private void OnDisable() {
            _skillController.onUsed -= ShowUI;
        }

        private void ShowUI() {
            if (_cooldownUIPrefab != null) {
                GameObject cooldownUI = Instantiate(_cooldownUIPrefab, transform.position, Quaternion.identity, _parentObject.transform);
                cooldownUI.GetComponentInChildren<TextMeshProUGUI>().text = _actionName;
                cooldownUI.GetComponent<Cooldown>().Initilize(_skillController.GetCooldown());
            }
        }
    }
}
