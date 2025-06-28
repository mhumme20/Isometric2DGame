using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int Health, MaxHealth;
    public Slider Healthbar;
    public GameObject LossScreen;
    private void Start()
    {
        Health = MaxHealth;
        Healthbar.maxValue = MaxHealth;
        Healthbar.value = Health;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Damage"))
        {
            Destroy(other.gameObject);
            Health--;
            Healthbar.value = Health;
            if (Health<=0)
            {
              LossScreen.gameObject.SetActive(true);
              Time.timeScale = 0;
            }
        }
    }
    
}
