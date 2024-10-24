using Cysharp.Threading.Tasks;

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
    }

    public interface ISkillController
    {
        void Activate();

        CombatType GetCombatType();
    }
}
