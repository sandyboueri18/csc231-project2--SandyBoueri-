using UnityEngine;

public class EmeraldCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ForestGuardianMovement>() != null)
        {
            Debug.Log("Emerald Crystal Collected!");
            Destroy(gameObject);
        }
    }
}
