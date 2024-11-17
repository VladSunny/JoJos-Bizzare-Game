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
        UniTask Stop();
    }

    public interface ISkillController
    {
        void Activate();

        CombatType GetCombatType();
    }
}
