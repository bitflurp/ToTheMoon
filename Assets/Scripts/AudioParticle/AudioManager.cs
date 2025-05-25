using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip beepSFX;


    public void PlaySFX() {

        audioSource.PlayOneShot(beepSFX);
    
    
    
    }
}
