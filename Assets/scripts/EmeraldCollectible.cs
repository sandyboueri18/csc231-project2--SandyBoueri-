
using UnityEngine;

public class EmeraldCollectible : MonoBehaviour
{
    public Animator portalAnimator;
    public CrystalCounter crystalCounter;

    [Header("Crystal Spawn Points")]
    public Transform[] spawnPoints;

    private int collectedCrystals = 0;

    private void Start()
    {
        if (spawnPoints.Length > 0)
        {
            transform.position = spawnPoints[0].position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ForestGuardianMovement>() != null)
        {
            collectedCrystals++;

            crystalCounter.CollectCrystal();

            Debug.Log("Crystal Collected: " + collectedCrystals);

            if (collectedCrystals >= spawnPoints.Length)
            {
                portalAnimator.SetBool("isActivated", true);

                Debug.Log("All Crystals Collected! Portal Activated!");

                gameObject.SetActive(false);
            }
            else
            {
                MoveCrystal();
            }
        }
    }

    private void MoveCrystal()
    {
        transform.position =
            spawnPoints[collectedCrystals].position;

        Debug.Log("Crystal moved to: " +
            spawnPoints[collectedCrystals].name);
    }
}
