
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AreaAudioManager : MonoBehaviour
{
    [Header("Main Background Music")]
    [SerializeField] private AudioClip backgroundMusic;

    [Range(0f, 1f)]
    [SerializeField] private float backgroundVolume = 0.5f;

    [Header("Cave Background Music Adjustment")]
    [Range(0f, 1f)]
    [SerializeField] private float caveMusicReduction = 0.015f;

    [Header("Garden Ambiance")]
    [SerializeField] private AudioClip gardenMusic;

    [Range(0f, 1f)]
    [SerializeField] private float gardenVolume = 0.5f;

    [Header("Cave Ambiance")]
    [SerializeField] private AudioClip caveMusic;

    [Range(0f, 1f)]
    [SerializeField] private float caveVolume = 0.5f;

    [Header("Transition Settings")]
    [SerializeField] private float fadeDuration = 1.5f;

    private AudioSource backgroundSource;
    private AudioSource areaSource;

    private Coroutine fadeCoroutine;
    private Coroutine backgroundFadeCoroutine;

    private void Awake()
    {
        // Main background music
        backgroundSource = GetComponent<AudioSource>();
        backgroundSource.playOnAwake = false;
        backgroundSource.loop = true;
        backgroundSource.spatialBlend = 0f;
        backgroundSource.volume = backgroundVolume;

        // Separate AudioSource for area ambiance
        areaSource = gameObject.AddComponent<AudioSource>();
        areaSource.playOnAwake = false;
        areaSource.loop = true;
        areaSource.spatialBlend = 0f;
        areaSource.volume = 0f;
    }

    private void Start()
    {
        PlayBackgroundMusic();
        PlayGardenMusic();
    }

    private void PlayBackgroundMusic()
    {
        if (backgroundMusic == null)
        {
            Debug.LogWarning("Background music is missing!");
            return;
        }

        backgroundSource.clip = backgroundMusic;
        backgroundSource.Play();
    }

    public void PlayGardenMusic()
    {
        PlayAreaMusic(gardenMusic, gardenVolume);
        ChangeBackgroundVolume(backgroundVolume);
    }

    public void PlayCaveMusic()
    {
        PlayAreaMusic(caveMusic, caveVolume);

        float caveBackgroundVolume = Mathf.Clamp01(
            backgroundVolume - caveMusicReduction);

        ChangeBackgroundVolume(caveBackgroundVolume);
    }

    private void ChangeBackgroundVolume(float targetVolume)
    {
        if (backgroundFadeCoroutine != null)
        {
            StopCoroutine(backgroundFadeCoroutine);
        }

        backgroundFadeCoroutine = StartCoroutine(
            FadeBackgroundVolume(targetVolume));
    }

    private IEnumerator FadeBackgroundVolume(float targetVolume)
    {
        float startVolume = backgroundSource.volume;
        float elapsed = 0f;
        float duration = Mathf.Max(0.01f, fadeDuration);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            backgroundSource.volume = Mathf.Lerp(
                startVolume, targetVolume, elapsed / duration);

            yield return null;
        }

        backgroundSource.volume = targetVolume;
        backgroundFadeCoroutine = null;
    }

    private void PlayAreaMusic(AudioClip newMusic, float targetVolume)
    {
        if (newMusic == null)
        {
            Debug.LogWarning("Area ambiance is missing!");
            return;
        }

        if (areaSource.clip == newMusic && areaSource.isPlaying)
        {
            return;
        }

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(
            FadeAreaMusic(newMusic, targetVolume));
    }

    private IEnumerator FadeAreaMusic(
        AudioClip newMusic, float targetVolume)
    {
        // Fade out current ambiance
        float startVolume = areaSource.volume;
        float elapsed = 0f;
        float duration = Mathf.Max(0.01f, fadeDuration);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            areaSource.volume = Mathf.Lerp(
                startVolume, 0f, elapsed / duration);

            yield return null;
        }

        // Switch ambiance
        areaSource.Stop();
        areaSource.clip = newMusic;
        areaSource.Play();

        // Fade in new ambiance
        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            areaSource.volume = Mathf.Lerp(
                0f, targetVolume, elapsed / duration);

            yield return null;
        }

        areaSource.volume = targetVolume;
        fadeCoroutine = null;
    }
}
