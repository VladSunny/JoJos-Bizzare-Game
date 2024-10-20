using UnityEngine;

namespace JJBG.Combat
{
    public class AlwaysAttackingDummy : MonoBehaviour
    {
        private ISkillController _skillController;

        private void Awake() {
            _skillController = GetComponent<ISkillController>();
        }

        private void Update() {
            _skillController.Activate();
        }
    }
}
