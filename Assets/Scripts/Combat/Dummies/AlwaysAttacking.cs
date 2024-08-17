using UnityEngine;

namespace JJBG.Combat.Dummies
{
    public class AlwaysAttacking : MonoBehaviour
    {
        [SerializeReference] private SkillBase _skill;

        private void Update() {
            _skill?.Activate();
        }
    }
}
