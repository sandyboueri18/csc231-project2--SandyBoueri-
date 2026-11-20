using UnityEngine;

public class GuardianMagicDamage : MonoBehaviour
{
    [SerializeField] private int damage = 1;


    private void OnParticleCollision(GameObject other)
    {
        MushroomHealth mushroom =
            other.GetComponentInParent<MushroomHealth>();

        if (mushroom != null)
        {
            mushroom.TakeDamage(damage);
        }
    }
}