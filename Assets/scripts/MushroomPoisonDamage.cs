using UnityEngine;

public class MushroomPoisonDamage : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Poison collided with: " + other.name);

        ForestGuardianMovement guardian =
            other.GetComponentInParent<ForestGuardianMovement>();

        if (guardian != null)
        {
            Debug.Log("Guardian received damage!");

            guardian.TakeDamage(damage);
        }
    }
}