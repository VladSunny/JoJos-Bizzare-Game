using UnityEngine;
using Unity.VisualScripting;

using JJBG.Movement;
using JJBG.Controller;
using JJBG.Combat;

namespace JJBG.Bootstraps
{
    public class SPBootstrap : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _SPPrefab;
        [SerializeField] private CombatState _combatState;
        [SerializeField] private Transform _playerObj;
        [SerializeField] private Transform _idlePosition;
        [SerializeField] private Transform _attackPosition;

        [Header("Settings")]
        [SerializeField] private bool _controller;

        private void Awake()
        {
            GameObject sp = Instantiate(_SPPrefab);
            sp.GetComponent<StarPlatinumMovement>().Initialize(_idlePosition, _playerObj, _attackPosition);

            if (_controller)
            {
                transform.AddComponent<SPController>().Initialize(sp);
            }

            SkillBase[] skills = sp.GetComponentsInChildren<SkillBase>();

            for (int i = 0; i < skills.Length; i++)
            {
                Debug.Log(skills[i]);
                skills[i].Initialize(_combatState);
            }
        }
    }
}
