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

        [Header("Settings")]
        [SerializeField] private bool _controller;

        private void Awake() {
            GameObject sp = Instantiate(_SPPrefab);
            sp.GetComponent<StarPlatinumMovement>().Initialize(_idlePosition, _playerObj);
            
            if (_controller) {
                transform.AddComponent<SPController>();
                transform.GetComponent<SPController>().Initialize(sp);
            }
        }
    }
}
