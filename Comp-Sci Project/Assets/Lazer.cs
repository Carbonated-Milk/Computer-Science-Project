using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    private Transform player;
    private Transform lazer;
    public Transform particles;
    public Transform beam;

    [Header("Lazer Settings")]
    public float lazerSpeed = 5f;
    private float lazerDamage = 5f;
    void Start()
    {
        lazer = transform.GetChild(0);
        player = Player.singleton.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(player.position - transform.position, transform.up), lazerSpeed * Time.deltaTime);

        RaycastHit hit = new RaycastHit();
        Physics.Raycast(transform.position, transform.forward, out hit);

        if (hit.collider != null)
        {
            particles.position = hit.point;
            beam.localScale = new Vector3(1, 1, hit.distance);
            if (hit.collider.transform.CompareTag("Player"))
            {
                PlayerHealth.singleton.Die();
            }
        }
    }
}
