
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteManager : MonoBehaviour
{
    public GameObject levelCompletePanel;

    private void Start()
    {
        levelCompletePanel.SetActive(false);
    }

    public void ShowLevelComplete()
    {
        levelCompletePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
