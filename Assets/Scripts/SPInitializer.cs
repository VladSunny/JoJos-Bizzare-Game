using UnityEngine;
using Unity.VisualScripting;

using JJBG.Movement;
using JJBG.Controller;

namespace JJBG
{
    public class SPInitializer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _SPPrefab;
        [SerializeField] private Transform _playerObj;
        [SerializeField] private Transform _idlePosition;
        [SerializeField] private Transform _attackPosition;

        [Header("Settings")]
        [SerializeField] private bool _controller;

        private void Awake() {
            GameObject sp = Instantiate(_SPPrefab);
            sp.GetComponent<StarPlatinumMovement>().Initialize(_idlePosition, _playerObj, _attackPosition);
            
            if (_controller) {
                transform.AddComponent<SPController>();
                transform.GetComponent<SPController>().Initialize(sp);
            }
        }
    }
}
