
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteManager : MonoBehaviour
{
    public GameObject levelCompletePanel;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioClip victorySound;

    private void Start()
    {
        levelCompletePanel.SetActive(false);
    }

    public void ShowLevelComplete()
    {
        levelCompletePanel.SetActive(true);

        backgroundMusic.Stop();
        audioSource.PlayOneShot(victorySound);

        Time.timeScale = 0f;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }
}
