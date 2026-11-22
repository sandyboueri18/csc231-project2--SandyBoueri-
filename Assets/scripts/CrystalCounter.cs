
using UnityEngine;
using TMPro;

public class CrystalCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText;

    private int collectedCrystals = 0;
    private int totalCrystals = 4;

    private void Start()
    {
        UpdateCounter();
    }

    public void CollectCrystal()
    {
        collectedCrystals++;
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        counterText.text = collectedCrystals + " / " + totalCrystals;
    }
}
