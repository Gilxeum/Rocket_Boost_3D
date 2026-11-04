using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioClip explosionAudioClip;
    [SerializeField] private AudioClip landingAudioClip;

    [Header("Particle Systems")]

    [SerializeField] private ParticleSystem explosionParticleSystem;
    [SerializeField] private ParticleSystem landingParticleSystem;

    private HealthManager healthManager;
    private PlayerShooting playerShooting;

    
    private Renderer[] characterRenderers;




    AudioSource audioSource;

    bool isControlable = true;

    bool canTakeDamage = true;

    bool isVisible = true;

    private void Awake()
    {
        healthManager = GetComponent<HealthManager>();
        characterRenderers = GetComponentsInChildren<Renderer>();
        playerShooting = GetComponent<PlayerShooting>();
    }

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
                if (canTakeDamage)
                    TakeDamage();
                break;
            case "HealingBox":
                healthManager.GetHeal(20);
                Debug.Log("Healed!");
                Destroy(collision.gameObject);
                break;
            case "AmmoBox":
                playerShooting.AddBullets(2);
                Debug.Log("2 more bullets!");
                Destroy(collision.gameObject);
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

    private void TakeDamage() 
    {
        healthManager.TakeDamage(20);
        if (healthManager.currentHealth != 0)
        {
            InvokeRepeating(nameof(ToggleVisibility), 0, 0.1f);
            ActivateInvincibility();
            
        }
        else
        {
            ManageCollision(explosionAudioClip, explosionParticleSystem);
        }
    }

    private void ActivateInvincibility()
    {
        canTakeDamage = false;

        Invoke(nameof(DeactivateInvincibility), 2f);
    }

    private void DeactivateInvincibility()
    {
        CancelInvoke(nameof(ToggleVisibility));
        canTakeDamage = true;
        isVisible = true;
        foreach (Renderer rend in characterRenderers)
        {
            rend.enabled = isVisible;
        }
    }

    private void ToggleVisibility()
    {
        
        isVisible = !isVisible;
        foreach (Renderer rend in characterRenderers)
        {
            rend.enabled = isVisible;
        }
    }
}
