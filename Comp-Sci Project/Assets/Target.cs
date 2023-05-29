using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody rb;
    private Transform target;

    [Header("Settings")]
    public float speed = 40f;
    public float randomSpeed = 30;
    public float damage = 5;
    public float secondsToFix = 1f;
    public float time2Live = 10f;

    [Header("Bonus")]
    public GameObject particalPrefab;
    void Start()
    {
        speed += Random.Range(-randomSpeed, randomSpeed);
        target = Player.singleton.transform;
        rb = GetComponent<Rigidbody>();
        Invoke("Die", time2Live);
    }

    void Update()
    {
        var idealSpeed = speed * (target.position - transform.position).normalized;
        var correctionSpeed = idealSpeed - rb.velocity;
        rb.AddForce(correctionSpeed * Time.deltaTime / secondsToFix, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth.singleton.Hurt(damage);
            Die();
        }
    }

    private void Die()
    {
        var particals = Instantiate(particalPrefab) as GameObject;
        particals.transform.position = transform.position;
        Destroy(gameObject);

    }
}
