using UnityEngine;

public class Collectible : MonoBehaviour
{
    public CollectibleName name;
    private bool isTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER) && !isTriggered)
        {
            isTriggered = true;
            CollectibleManager.Instance.CollectCollectible(name, transform.position);
            Destroy(gameObject);
        }
    }
}
