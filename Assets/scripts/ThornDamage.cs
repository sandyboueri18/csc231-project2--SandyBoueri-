using UnityEngine;

public class ThornDamage : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnTriggerStay2D(Collider2D other)
    {
        ForestGuardianMovement guardian =
            other.GetComponentInParent<ForestGuardianMovement>();

        if (guardian != null)
        {
            guardian.TakeDamage(damage);
        }
    }
}