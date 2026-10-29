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

        //TODO Particles
        //TODO if bullets hits an enemy, reduce enemy health
        //TODO : if bullet hits player, reduce player healtch
    }
}
