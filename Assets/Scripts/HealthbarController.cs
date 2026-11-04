using UnityEngine;
using UnityEngine.UI;

public class HealthbarController : MonoBehaviour
{
    [SerializeField]
    private GameObject character;
    private HealthManager healthManager;
    private int maxHealth;
    private Slider healthSlider;
    private Image healthFill;

    private void Awake()
    {
        if(character == null)
        {
            healthManager = GetComponentInParent<HealthManager>();
        }
        else
        {
            healthManager = character.GetComponent<HealthManager>();
        }
        healthSlider = GetComponent<Slider>();
        healthFill = healthSlider.fillRect.GetComponent<Image>();
    }

    private void OnEnable()
    {
        healthManager.OnHealthChanged += UpdateHealthBar;
    }
    private void OnDisable()
    {
        healthManager.OnHealthChanged -= UpdateHealthBar;
    }

    private void Start()
    {
        maxHealth = healthManager.currentHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        healthFill.color = Color.green;
        Debug.Log("Vida actual: " + maxHealth + " / Vida máxima: " + maxHealth);
    }



    private void UpdateHealthBar(int newHealth)
    {

        healthFill.color = Color.Lerp(Color.red, Color.green, (float)newHealth / maxHealth);
        healthSlider.value = newHealth;

    }

}
