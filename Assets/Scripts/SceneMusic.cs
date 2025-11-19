using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    private static SceneMusic instance;
    private AudioSource audioSource;

    void Awake()
    {
        // Check if an instance already exists
        if (instance != null && instance != this)
        {
            // Destroy this duplicate instance
            Destroy(gameObject);
            return;
        }

        // Set this as the singleton instance
        instance = this;

        // Prevent this GameObject from being destroyed when loading new scenes
        DontDestroyOnLoad(gameObject);

        // Get or add AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure audio source for music
        audioSource.loop = true;
        audioSource.playOnAwake = true;

        // Start playing if not already playing
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Optional: Method to change music
    public void ChangeMusic(AudioClip newClip)
    {
        if (newClip != null)
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }

    // Optional: Method to stop music
    public void StopMusic()
    {
        audioSource.Stop();
    }

    // Optional: Method to pause/unpause music
    public void TogglePause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }

    // Optional: Method to adjust volume
    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
    }
}
