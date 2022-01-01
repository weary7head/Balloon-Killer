using UnityEngine;

namespace BubbleLogic
{
    public class BubblesDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Bubble bubble))
            {
                Destroy(bubble.gameObject);
            }
        }
    }
}