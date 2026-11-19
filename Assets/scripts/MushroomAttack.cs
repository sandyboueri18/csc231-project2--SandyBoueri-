
using UnityEngine;

public class MushroomAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private ParticleSystem poisonParticles;

    private Transform guardian;
    private float attackTimer;

    private void Start()
    {
       ForestGuardianMovement player = FindAnyObjectByType<ForestGuardianMovement>();

        if (player != null)
        {
            guardian = player.transform;
        }
    }

    private void Update()
    {
        if (guardian == null || poisonParticles == null)
            return;

        attackTimer -= Time.deltaTime;

        float distance = Vector2.Distance(
            transform.position,
            guardian.position
        );

        if (distance <= attackRange && attackTimer <= 0f)
        {
            poisonParticles.Play();
            attackTimer = attackCooldown;
            poisonParticles.Stop();
        }
    }
}
