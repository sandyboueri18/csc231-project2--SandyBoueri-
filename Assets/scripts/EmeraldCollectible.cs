
using UnityEngine;

public class EmeraldCollectible : MonoBehaviour
{
    public Animator portalAnimator;
    public CrystalCounter crystalCounter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ForestGuardianMovement>() != null)
        {
            Debug.Log("Emerald Crystal Collected!");

            if (crystalCounter != null)
            {
                crystalCounter.CollectCrystal();
                Debug.Log("Crystal Counter Updated!");
            }

            if (portalAnimator != null)
            {
                portalAnimator.SetBool("isActivated", true);
                Debug.Log("Enchanted Portal Activated!");
            }

            Destroy(gameObject);
        }
    }
}
