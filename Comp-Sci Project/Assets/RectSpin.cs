using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RectSpin : MonoBehaviour
{
    public float speed;
    public bool random;
    private RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        if(random) rect.Rotate(Vector3.forward * Random.Range(0, 360f));
    }

    // Update is called once per frame
    void Update()
    {
        rect.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
}
