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

}
