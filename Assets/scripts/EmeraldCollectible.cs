
using UnityEngine;

public class EmeraldCollectible : MonoBehaviour
{
    public Animator portalAnimator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ForestGuardianMovement>() != null)
        {
            Debug.Log("Emerald Crystal Collected!");

            if (portalAnimator != null)
            {
                portalAnimator.SetBool("isActivated", true);
                Debug.Log("Enchanted Portal Activated!");
            }

            Destroy(gameObject);
        }
    }
}
