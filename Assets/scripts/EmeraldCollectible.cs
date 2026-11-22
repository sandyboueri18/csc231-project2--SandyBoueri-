
using UnityEngine;

public class EmeraldCollectible : MonoBehaviour
{
    public Animator portalAnimator;
    public CrystalCounter crystalCounter;

    [Header("Crystal Spawn Points")]
    public Transform[] spawnPoints;

    private int collectedCrystals = 0;
    private int lastSpawnIndex = -1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ForestGuardianMovement>() != null)
        {
            collectedCrystals++;

            crystalCounter.CollectCrystal();

            Debug.Log("Crystal Collected: " + collectedCrystals);

            if (collectedCrystals >= 4)
            {
                portalAnimator.SetBool("isActivated", true);
                Debug.Log("All 4 Crystals Collected! Portal Activated!");

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
        ForestGuardianMovement guardian =
            FindAnyObjectByType<ForestGuardianMovement>();

        if (guardian == null || spawnPoints.Length == 0)
            return;

        float guardianX = guardian.transform.position.x;

        System.Collections.Generic.List<Transform> availablePoints =
            new System.Collections.Generic.List<Transform>();

        int remainingSpawns = 4 - collectedCrystals;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i] == null)
                continue;

            float pointX = spawnPoints[i].position.x;

            // The next crystal must appear ahead.
            if (pointX <= guardianX + 2f)
                continue;

            // Count how many spawn points are farther ahead.
            int pointsAfter = 0;

            for (int j = 0; j < spawnPoints.Length; j++)
            {
                if (spawnPoints[j] != null &&
                    spawnPoints[j].position.x > pointX + 2f)
                {
                    pointsAfter++;
                }
            }

            // Reserve enough locations for later collections.
            if (pointsAfter >= remainingSpawns - 1)
            {
                availablePoints.Add(spawnPoints[i]);
            }
        }

        if (availablePoints.Count == 0)
        {
            Debug.LogWarning(
                "No valid crystal spawn point ahead! Check the starting position."
            );
            return;
        }

        int randomIndex = Random.Range(0, availablePoints.Count);

        transform.position = availablePoints[randomIndex].position;

        Debug.Log("Crystal moved to: " +
                  availablePoints[randomIndex].name);
    }



}
