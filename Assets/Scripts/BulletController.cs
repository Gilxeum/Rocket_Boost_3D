using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Bullet settings")]
    [SerializeField] private float bulletLifeTime = 1f;

    private void OnEnable()
    {
        Invoke(nameof(DeactivateBullet), bulletLifeTime);
    }


    private void DeactivateBullet()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        DeactivateBullet();

        switch(other.tag)
        {
            case "Barrel":
               other.GetComponent<BarrelController>().DestroyObject();
            break;
            case "Enemy":
                other.GetComponent<HealthManager>().TakeDamage(20);
                Debug.Log("Enemy hit! 20 damage dealt.");
                break;
            default:
            break; 
        }

        
    }
}
