using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    private Transform player;
    private Transform lazer;

    [Header("Game Objects")]
    public Transform particles;
    public Transform beam;

    [Header("Lazer Settings")]
    public float lazerSpeed = 5f;
    public float lazerDamagePerSecond = 5f;
    void Start()
    {
        lazer = transform.GetChild(0);
        player = Player.singleton.transform;

        beam.localScale = new Vector3(1, 1, 1000);
        particles.position = transform.forward * 1000;
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
                PlayerHealth.singleton.Hurt(lazerDamagePerSecond * Time.deltaTime);
            }
        }
    }
}
