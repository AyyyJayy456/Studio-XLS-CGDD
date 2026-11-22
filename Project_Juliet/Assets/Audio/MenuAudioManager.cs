
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MenuAudioManager : MonoBehaviour
{
    [Header("Background Music")]
    [SerializeField] private AudioClip backgroundMusic;

    [Range(0f, 1f)]
    [SerializeField] private float musicVolume = 0.5f;

    [Header("Button Sound Effects")]
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;

    [Range(0f, 1f)]
    [SerializeField] private float soundEffectVolume = 1f;

    private AudioSource musicSource;
    private AudioSource soundEffectSource;

    private void Awake()
    {
        // Background music AudioSource
        musicSource = GetComponent<AudioSource>();
        musicSource.playOnAwake = false;
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.spatialBlend = 0f;

        // Separate AudioSource for button sounds
        soundEffectSource = gameObject.AddComponent<AudioSource>();
        soundEffectSource.playOnAwake = false;
        soundEffectSource.loop = false;
        soundEffectSource.spatialBlend = 0f;
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic == null)
        {
            Debug.LogWarning("No background music assigned!");
            return;
        }

        if (musicSource.isPlaying)
        {
            return;
        }

        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayHoverSound()
    {
        if (hoverSound != null)
        {
            soundEffectSource.PlayOneShot(
                hoverSound, soundEffectVolume);
        }
    }

    public void PlayClickSound()
    {
        if (clickSound != null)
        {
            soundEffectSource.PlayOneShot(
                clickSound, soundEffectVolume);
        }
    }
}
