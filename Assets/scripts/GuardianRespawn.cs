
using UnityEngine;
using System.Collections;

public class GuardianRespawn : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float respawnDelay = 2f;

    [Header("Game Over System")]
    public GameOverManager gameOverManager;

    private ForestGuardianMovement guardian;
    private Rigidbody2D body;
    private bool isRespawning;

    private void Awake()
    {
        guardian = GetComponent<ForestGuardianMovement>();
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (guardian.IsDead && !isRespawning)
        {
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        isRespawning = true;

        // Remove one life and check for Game Over.
        bool isGameOver = gameOverManager.PlayerDied();

        if (isGameOver)
        {
            yield break;
        }

        // Wait before respawning.
        yield return new WaitForSeconds(respawnDelay);

        // Return to the starting point or checkpoint.
        body.position = spawnPoint.position;
        body.linearVelocity = Vector2.zero;

        // Restore the Guardian.
        guardian.Revive();

        isRespawning = false;
    }

    public void SetCheckpoint(Transform newCheckpoint)
    {
        spawnPoint = newCheckpoint;
    }
}
