using UnityEngine;
using System.Collections;

public class GuardianRespawn : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float respawnDelay = 2f;

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

        yield return new WaitForSeconds(respawnDelay);

        body.position = spawnPoint.position;
        body.linearVelocity = Vector2.zero;

        guardian.Revive();

        isRespawning = false;
    }

    public void SetCheckpoint(Transform newCheckpoint)
    {
        spawnPoint = newCheckpoint;
    }
}