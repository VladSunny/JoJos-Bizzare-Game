using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

using JJBG.Movement;
using JJBG.Audio;

namespace JJBG.Combat
{
    public class SummonSkill : MonoBehaviour, ISkill
    {
        [Header("Dependencies")]
        [SerializeField] private SPMovement _spMovement;
        [SerializeField] private GameObject _spObj;
        [SerializeField] private CombatCore _combatCore;
        [SerializeField] private AudioManager _audioManager;
        

        [Header("Settings")]
        [Tooltip("In milliseconds"), SerializeField] private int _hideDuration;

        [Header("Audio")]
        [SerializeField] private int _summonSoundCount = 0;
        [SerializeField] private string _summonSoundPref = "Summon_";
        [SerializeField] private int _unsummonSoundCount = 0;
        [SerializeField] private string _unsummonSoundPref = "Unsummon_";

        public async UniTask Attack()
        {
            if (_combatCore.GetCombatType() == CombatType.Stand) {
                if (!_spMovement.SetMovementState(MovementState.Hiding)) return;
                _combatCore.SetCombatType(CombatType.Standless);

                if (_unsummonSoundCount > 0)
                    _audioManager.Play(_unsummonSoundPref + UnityEngine.Random.Range(1, _unsummonSoundCount + 1));

                await Task.Delay(_hideDuration);
                
                _spObj.SetActive(false);
            }
            else {
                if (!_spMovement.SetMovementState(MovementState.Idle)) return;
                _combatCore.SetCombatType(CombatType.Stand);

                if (_summonSoundCount > 0)
                    _audioManager.Play(_summonSoundPref + UnityEngine.Random.Range(1, _summonSoundCount + 1));
                
                _spObj.SetActive(true);
            }
        }

        public UniTask Stop()
        {
            return UniTask.CompletedTask;
        }
    }
}
