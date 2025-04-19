using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] AudioSource audioSource_wind;
    [SerializeField] AudioSource audioSource_walk;
    [SerializeField] AudioClip windupSound;
    [SerializeField] AudioClip releaseSound;

    public void PlaySound(string sound)
    {
        switch (sound)
        {
            case "Walk":
                audioSource_walk.Play();
                break;
            case "Windup":
                audioSource_wind.clip = windupSound;
                audioSource_wind.Play();
                break;
            case "Release":
                audioSource_wind.clip = releaseSound;
                audioSource_wind.Play();
                break;
        }
    }
}
