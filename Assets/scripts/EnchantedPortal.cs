
using UnityEngine;

public class EnchantedPortal : MonoBehaviour
{
    public Animator portalAnimator;
    public LevelCompleteManager levelCompleteManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ForestGuardianMovement>() != null)
        {
            if (portalAnimator.GetBool("isActivated"))
            {
                Debug.Log("LEVEL COMPLETE!");

                levelCompleteManager.ShowLevelComplete();
            }
        }
    }
}
