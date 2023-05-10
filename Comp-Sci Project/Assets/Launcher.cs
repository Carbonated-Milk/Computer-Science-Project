using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [Header("Object")]
    public GameObject launchObject;
    [Header("Settings")]
    public float interval = 1f;
    public float life = 5f;
    public float speed = 20f;
    void Start()
    {
        StartCoroutine(Launch());
    }

    public IEnumerator Launch()
    {
        while(true)
        {
            var obj = Instantiate(launchObject);
            obj.transform.position = transform.position;
            obj.GetComponent<Rigidbody>().velocity = transform.up * speed;
            obj.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(0,360f), Random.Range(0, 360f), Random.Range(0, 360f)));
            Destroy(obj, life);
            yield return new WaitForSeconds(interval);
        }
    }

}
