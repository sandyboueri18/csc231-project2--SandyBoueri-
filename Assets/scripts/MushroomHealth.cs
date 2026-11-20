using UnityEngine;

public class MushroomHealth : MonoBehaviour
{
    [SerializeField] private int maximumHealth = 3;
    [SerializeField] private float damageCooldown = 0.5f;

    private int currentHealth;
    private float nextDamageTime;
    private bool isDead;

    private void Awake()
    {
        currentHealth = maximumHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead || Time.time < nextDamageTime)
        {
            return;
        }

        nextDamageTime = Time.time + damageCooldown;

        currentHealth = Mathf.Max(0, currentHealth - damage);

        Debug.Log("Mushroom Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Mushroom defeated!");
        Destroy(gameObject);
    }
}