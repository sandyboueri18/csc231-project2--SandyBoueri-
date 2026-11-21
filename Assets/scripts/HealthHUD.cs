
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    [Header("Guardian")]
    [SerializeField] private ForestGuardianMovement guardian;

    [Header("Heart Images")]
    [SerializeField] private Image[] hearts;

    private void Update()
    {
        if (guardian == null)
            return;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
            {
                hearts[i].enabled = i < guardian.CurrentHealth;
            }
        }
    }
}
