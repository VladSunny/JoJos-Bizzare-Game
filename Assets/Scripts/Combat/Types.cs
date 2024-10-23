using Cysharp.Threading.Tasks;

namespace JJBG.Combat
{
    public enum CombatType
    {
        Standless,
        Stand
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
