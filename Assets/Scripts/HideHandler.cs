using System.Collections.Generic;
using UnityEngine;

namespace JJBG
{
    public class HideHandler : MonoBehaviour
    {
        private List<MeshRenderer> _meshRenderers;
        private List<SkinnedMeshRenderer> _skinnedMeshRenderers;

        private void Awake() {
            _meshRenderers = new List<MeshRenderer>(GetComponentsInChildren<MeshRenderer>());
            _skinnedMeshRenderers = new List<SkinnedMeshRenderer>(GetComponentsInChildren<SkinnedMeshRenderer>());
        }

        public void Hide() {
            foreach (var meshRenderer in _meshRenderers)
                meshRenderer.enabled = false;
            foreach (var skinnedMeshRenderer in _skinnedMeshRenderers)
                skinnedMeshRenderer.enabled = false;
        }

        public void Show() {
            foreach (var meshRenderer in _meshRenderers)
                meshRenderer.enabled = true;
            foreach (var skinnedMeshRenderer in _skinnedMeshRenderers)
                skinnedMeshRenderer.enabled = true;
        }
    }
}
