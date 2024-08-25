using Cysharp.Threading.Tasks;

namespace JJBG.Combat
{
    public interface IAttack
    {
        UniTask Attack();
    }
}