using UnityEngine;

public class audio : MonoBehaviour
{
    [Header("=============Audio source ==============")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("=============Audio musique ==============")]
    public AudioClip mainMusique;
    public AudioClip menuWoosh; 

    private void Start()
    {
        musicSource.clip = mainMusique;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
