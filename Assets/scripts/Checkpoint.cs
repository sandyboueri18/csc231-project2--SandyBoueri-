using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform respawnPosition;

    private bool isActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GuardianRespawn guardian =
            other.GetComponentInParent<GuardianRespawn>();

        if (guardian != null && !isActivated)
        {
            isActivated = true;

            animator.SetBool("isActivated", true);

            guardian.SetCheckpoint(respawnPosition);

            Debug.Log("Checkpoint activated!");
        }
    }
}