using UnityEngine;
using System;

namespace JJBG.Core
{
    public class DynamicHitBox : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _playerObj;
        [SerializeField] private LayerMask _enemyLayer;

        [Header("Debug")]
        [SerializeField] private bool _drawDebugBoxInEditor = true;
        [SerializeField] private bool _debugPunch = true;
        [SerializeField] private KeyCode _punchKey = KeyCode.Mouse0;
        [SerializeField] private bool _debugDraw = true;


        public void CreateHitBox(
            Vector3 relativeBoxPosition,
            Vector3 boxSize, Action<Collider> actionOnHit,
            bool debugDraw = false)
        {
            Vector3 boxPosition = _playerObj.position + _playerObj.forward * relativeBoxPosition.z +
                                  _playerObj.right * relativeBoxPosition.x +
                                  _playerObj.up * relativeBoxPosition.y;

            Quaternion boxRotation = _playerObj.rotation;

            Collider[] hitColliders = Physics.OverlapBox(boxPosition, boxSize / 2, boxRotation, _enemyLayer);

            foreach (Collider hitCollider in hitColliders)
            {
                actionOnHit?.Invoke(hitCollider);
            }

            if (debugDraw)
                DebugDrawBox(boxPosition, boxSize, boxRotation, Color.red, 2f);
        }


        private void DebugDrawBox(
            Vector3 position,
            Vector3 boxSize,
            Quaternion orientation,
            Color color,
            float duration)
        {
            Vector3 halfBoxSize = boxSize / 2.0f;

            Vector3[] points = new Vector3[8]
            {
                orientation * new Vector3(halfBoxSize.x, halfBoxSize.y, halfBoxSize.z),
                orientation * new Vector3(-halfBoxSize.x, halfBoxSize.y, halfBoxSize.z),
                orientation * new Vector3(-halfBoxSize.x, -halfBoxSize.y, halfBoxSize.z),
                orientation * new Vector3(halfBoxSize.x, -halfBoxSize.y, halfBoxSize.z),
                orientation * new Vector3(halfBoxSize.x, halfBoxSize.y, -halfBoxSize.z),
                orientation * new Vector3(-halfBoxSize.x, halfBoxSize.y, -halfBoxSize.z),
                orientation * new Vector3(-halfBoxSize.x, -halfBoxSize.y, -halfBoxSize.z),
                orientation * new Vector3(halfBoxSize.x, -halfBoxSize.y, -halfBoxSize.z)
            };

            for (int i = 0; i < 4; i++)
            {
                Debug.DrawLine(position + points[i], position + points[(i + 1) % 4], color, duration);
                Debug.DrawLine(position + points[i + 4], position + points[((i + 1) % 4) + 4], color, duration);
                Debug.DrawLine(position + points[i], position + points[i + 4], color, duration);
            }
        }


        private void Update() {
            if (_debugPunch && Input.GetKeyDown(_punchKey))
            {
                CreateHitBox(Vector3.forward * 1f, new Vector3(1f, 1f, 1f), (collider) =>
                {
                    Debug.Log(collider);
                }, _debugDraw);
            }
        }

        private void OnDrawGizmos() {
            if (_playerObj != null && _drawDebugBoxInEditor)
            {
                Vector3 relBoxPos = Vector3.forward * 1f;
                Vector3 boxSize = new Vector3(1f, 1f, 1f);

                Vector3 boxPosition = _playerObj.position + _playerObj.forward * relBoxPos.z +
                                      _playerObj.right * relBoxPos.x +
                                      _playerObj.up * relBoxPos.y;
                Quaternion boxRotation = _playerObj.rotation;

                DebugDrawBox(boxPosition, boxSize, boxRotation, Color.blue, 0);
            }
        }
    }
}