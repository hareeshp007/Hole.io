
using UnityEngine;

namespace Holeio.Interact
{
    public class Interactable : MonoBehaviour
    {
        public LayerMask PlaneLayer;
        public LayerMask nothing;
        [SerializeField]
        private Collider mCollider;
        [SerializeField]
        private int interactableSize;
        private void Start()
        {
            mCollider = GetComponent<Collider>();
        }
        public void OnInteract(int holeSize,bool IsInside)
        {
            if (interactableSize <= holeSize)
            {
                if (IsInside)
                {
                    mCollider.excludeLayers = PlaneLayer;
                }
                else
                {
                    mCollider.excludeLayers = nothing;
                }
            }
        }

        public int GetScore()
        {
            return interactableSize;
        }
    }
}

