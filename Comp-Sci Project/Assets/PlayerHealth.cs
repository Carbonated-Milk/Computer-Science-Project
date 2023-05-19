using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    public static PlayerHealth singleton;

    private void Awake()
    {
        singleton = this;
        currentHealth = maxHealth;
    }
    private void Start()
    {
        UpdateHealthBar();
    }
    public void Die()
    {
        LevelManager.singleton.GameOver();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("InstantDeath"))
        {
            Die();
        }
    }

    public void Hurt(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
            return;
        }
        UpdateHealthBar();
        
    }

    public void UpdateHealthBar()
    {
        HealthBar.singleton.SetHealthBar(currentHealth/maxHealth);
    }

}
