using UnityEngine;

namespace JJBG.Combat {
    public interface ISkill
    {
        void Activate();

        void Initialize(CombatState combatState);
    }

    [System.Serializable]
    public abstract class SkillBase : MonoBehaviour, ISkill
    {
        public abstract void Activate();

        public abstract void Initialize(CombatState combatState);
    }

}