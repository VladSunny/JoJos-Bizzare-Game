using Cysharp.Threading.Tasks;
using UnityEngine;

namespace JJBG.Combat
{
    public enum CombatType
    {
        Standless,
        Stand,
        Other
    }

    public interface ISkill
    {
        UniTask Attack();
        UniTask Stop();
    }

    public interface ISkillController
    {
        void Activate();

        CombatType GetCombatType();
    }

    [System.Serializable]
    public class HitInfo
    {
        public float damage;
        public Vector3 force;
        public GameObject attacker;
        public bool isRagdoll;
        public float stunDuration;

        public HitInfo(float damage, Vector3 force, GameObject attacker, bool isRagdoll, float stunDuration)
        {
            this.damage = damage;
            this.force = force;
            this.attacker = attacker;
            this.isRagdoll = isRagdoll;
            this.stunDuration = stunDuration;
        }
    }

}
