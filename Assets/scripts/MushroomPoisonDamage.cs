
using UnityEngine;

public class MushroomPoisonDamage : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnParticleCollision(GameObject other)
    {
        ForestGuardianMovement guardian =
            other.GetComponentInParent<ForestGuardianMovement>();

        if (guardian != null)
        {
            guardian.TakeDamage(damage);
        }
    }
}
