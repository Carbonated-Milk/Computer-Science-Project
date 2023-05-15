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
    public bool randRotate;
    public Vector3 rotation;

    public bool random = false;
    public float mult = 1;
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
            if(random)
            {
                obj.transform.position += new Vector3(Random.Range(-1f, 1) * transform.localScale.x, 0, Random.Range(-1f, 1) * transform.localScale.y) * mult;
            }
            obj.GetComponent<Rigidbody>().velocity = transform.up * speed;
            if (randRotate)
            {
                obj.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f)));
            }
            else
            {
                obj.transform.rotation = Quaternion.Euler(rotation);
            }
            Destroy(obj, life);
            yield return new WaitForSeconds(interval);
        }
    }

}
