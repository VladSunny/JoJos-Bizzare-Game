using Cysharp.Threading.Tasks;

namespace JJBG.Combat
{
    public enum CombatType
    {
        Standless,
        Stand
    }

    interface ISkill
    {
        UniTask Attack();
    }
}
