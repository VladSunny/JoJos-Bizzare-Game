using UnityEngine;

using JJBG.Movement;

namespace JJBG
{
    public class SPInitializer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _SPPrefab;
        [SerializeField] private Transform _playerObj;
        [SerializeField] private Transform _idlePosition;

        private void Awake() {
            GameObject sp = Instantiate(_SPPrefab);
            sp.GetComponent<StarPlatinumMovement>().Initialize(_idlePosition, _playerObj);
        }
    }
}
