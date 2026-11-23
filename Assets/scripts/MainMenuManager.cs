
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject gameHUD;

    private void Start()
    {
        Time.timeScale = 0f;
        mainMenuPanel.SetActive(true);
    }

    public void PlayGame()
    {
        mainMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
