using UnityEngine;

namespace JJBG.Combat {
    public interface ISkill
    {
        void Activate();
    }

    [System.Serializable]
    public abstract class SkillBase : MonoBehaviour, ISkill
    {
        public abstract void Activate();
    }

}