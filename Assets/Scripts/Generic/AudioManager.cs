using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Zombie Sounds")]
    [SerializeField] AudioClip zombieHurt;
    [SerializeField] AudioClip zombieDied;
    [Header("Player Sounds")]
    [SerializeField] AudioClip playerHurt;
    [SerializeField] AudioClip playerDied;
    [Header("Guns Sounds")]
    [SerializeField] AudioClip gunShot;
    [SerializeField] AudioClip ShotgunGunShot;
    [SerializeField] AudioClip pistolReload;
    [SerializeField] AudioClip ARReload;
    [SerializeField] AudioClip ShotgunReload;
    [Header("Item Collecting")]
    [SerializeField] AudioClip keyCollect;
    [SerializeField] AudioClip healthPackCollect;

    [SerializeField] float defaultLevel = 0.3f;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }


    public void AudioGunShot()
    {
        audioSource.PlayOneShot(gunShot, defaultLevel);
    }
    public void AudioZombieHurt()
    {
        audioSource.PlayOneShot(zombieHurt, defaultLevel);
    }
    public void AudioZombieDied()
    {
        audioSource.PlayOneShot(zombieDied, defaultLevel);
    }
    public void AudioPlayerHurt()
    {
        audioSource.PlayOneShot(playerHurt, defaultLevel);
    }
    public void AudioPlayerDied()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(playerDied, defaultLevel);
    }
    public void AudioShotgunShot()
    {
        audioSource.PlayOneShot(ShotgunGunShot, defaultLevel);
    }
    public void AudioPistolReload()
    {
        audioSource.PlayOneShot(pistolReload, defaultLevel);
    }
    public void AudioARReload()
    {
        audioSource.PlayOneShot(ARReload, defaultLevel);
    }
    public void AudioShotgunReload()
    {
        audioSource.PlayOneShot(ShotgunReload, defaultLevel);
    }
    public void AudioKeyCollect()
    {
        audioSource.PlayOneShot(keyCollect, defaultLevel);
    }
    public void AudioHealthPickUp()
    {
        audioSource.PlayOneShot(healthPackCollect, defaultLevel);
    }
   
}
