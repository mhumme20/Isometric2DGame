using System;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{

    public int Health, MaxHealth;
    public Canvas canvas;
    public Slider Healthbar;
    private Camera PlayerCamera;

    private void Start()
    {
        canvas.worldCamera=Camera.main;
        PlayerCamera=Camera.main;
        Health = MaxHealth;
        Healthbar.maxValue = MaxHealth;
        Healthbar.value = Health;
    }

    private void Update()
    {
        Vector3 direction = PlayerCamera.transform.forward;
        canvas.transform.rotation = Quaternion.LookRotation(direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            Destroy(other.gameObject);
            Health--;
            Healthbar.value = Health;
            if (Health<=0)
            {
                Destroy(gameObject);
            }
        }
    }
}
