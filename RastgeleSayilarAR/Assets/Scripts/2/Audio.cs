using UnityEngine;

public class ButtonSoundToggle : MonoBehaviour
{
    public AudioSource audioSource; 

    public void ToggleSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop(); 
        }
        else
        {
            audioSource.time = 0; 
            audioSource.Play();  
        }
    }
}
