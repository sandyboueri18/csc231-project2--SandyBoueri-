
using UnityEngine;
using TMPro;

public class AudioMuteManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI muteButtonText;
  
    private void Start()
    {
        UpdateButtonText();
    }

    public void ToggleMute()
    {
        AudioListener.volume =
            AudioListener.volume == 0f ? 1f : 0f;

        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        if (AudioListener.volume == 0f)
        {
            muteButtonText.text = "UNMUTE AUDIO";
        }
        else
        {
            muteButtonText.text = "MUTE AUDIO";
        }
    }
}
