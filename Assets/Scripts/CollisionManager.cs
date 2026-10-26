using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioClip explosionAudioClip;
    [SerializeField] private AudioClip landingAudioClip;

    [Header("Particle Systems")]

    [SerializeField] private ParticleSystem explosionParticleSystem;
    [SerializeField] private ParticleSystem landingParticleSystem;




    AudioSource audioSource;

    bool isControlable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isControlable) return;
        switch (collision.gameObject.tag)
        {
            case "LaunchPad":
                Debug.Log("LaunchPad collision");
                break;
            case "LandingPad":
                ManageCollision(landingAudioClip,landingParticleSystem);
                break;
            case "Explosion":
                ManageCollision(explosionAudioClip,explosionParticleSystem);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Manages the collision of what the player collides
    /// Playing the specifies audio of what kind of things collides 
    /// </summary>
    /// <param name="audioClip"></param>
    private void ManageCollision(AudioClip audioClip,ParticleSystem particleSystem)
    {
        isControlable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
        particleSystem.Play();
        GetComponent<PlayerMovement>().enabled = false;
    }
}
