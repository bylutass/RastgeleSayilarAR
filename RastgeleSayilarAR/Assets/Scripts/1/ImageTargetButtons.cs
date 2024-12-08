using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public Canvas targetCanvas; 
    public AudioSource audioSource; 
    public AudioClip soundClip; 

    private bool isAudioPlaying = false; 

    private void Awake()
    {
        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(false); 
        }
        else
        {
            Debug.LogWarning("Canvas atanmadı!");
        }
    }

    public void ToggleSound()
    {
        if (audioSource == null || soundClip == null)
        {
            Debug.LogWarning("AudioSource veya AudioClip atanmadı!");
            return;
        }

        if (isAudioPlaying)
        {
            audioSource.Stop();
            isAudioPlaying = false;
        }
        else
        {
            audioSource.clip = soundClip;
            audioSource.Play();
            isAudioPlaying = true;
        }
    }

    public void ShowCanvas()
    {
        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Canvas atanmadı!");
        }
    }
}
