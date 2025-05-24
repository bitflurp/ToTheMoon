using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip beep;
    [SerializeField] private AudioSource audioSource1;

    public void PlaySFX() {

        audioSource1.PlayOneShot(beep);
    
    }
}
