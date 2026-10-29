using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Ammo Settings")]
    [SerializeField] private int currentAmmo = 10;
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private bool infiniteAmmo = false;


    [Header("Shooting Settings")]
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float bulletRate = 0.25f;
    [SerializeField] private int bulletDamage = 1;

    private Transform bulletSpawnPoint;

    private ObjectPool bulletPool;

    private float lastShotTime;

    private void Start()
    {
        bulletSpawnPoint = transform.Find("ShootPoint");
        bulletPool = GetComponent<ObjectPool>();
    }


    /// <summary>
    /// Check if the player can shoot based on the bullet rate and ammo avaibalility
    /// </summary>
    /// <returns></returns>
    public bool CanShoot()
    {
        //Check if enough time has passed since the last shot and if there is ammo available
        //or infinite ullets
        return ((Time.time - lastShotTime >= bulletRate) && (infiniteAmmo || currentAmmo > 0));
    }

    /// <summary>
    /// Shot a bullet from the object pool and apply
    /// </summary>
    public void Shoot() 
    {
        if(CanShoot())
        {
            lastShotTime = Time.time;

            if(!infiniteAmmo)currentAmmo--;

            GameObject bullet = bulletPool.GetObjectFromPool();
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.transform.rotation = bulletSpawnPoint.rotation;

            Rigidbody bulletRigidBody = bullet.GetComponent<Rigidbody>();

            bulletRigidBody.linearVelocity = bulletSpawnPoint.up * bulletSpeed;




        }
    }

}
