using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    
    public int currentHealth { get; private set; }

    [SerializeField]
    private string name;

    public event Action<int> OnHealthChanged;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        name = transform.name;
    }

    public void GetHeal(int heal)
    {
        currentHealth += heal;
        OnHealthChanged?.Invoke(currentHealth);
    }
    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        OnHealthChanged?.Invoke(currentHealth);



        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    { 
        Debug.Log(name + " has died.");
        if(name != "Player")
            Destroy(gameObject);
    }

}
