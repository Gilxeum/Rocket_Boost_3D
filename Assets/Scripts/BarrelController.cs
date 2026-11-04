using UnityEngine;

public class BarrelController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;


    public void DestroyObject()
    {
       

        particleSystem.transform.parent = null;
        particleSystem.Play();

        Destroy(gameObject);
    }
}
