using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    Vector3 ogPos;
    [Header("Bob")]
    public float bobHeight = 1f;
    public float bobSpeed = 1f;
    [Header("Spin")]
    public float spinSpeed = 1;
    public float spinBreak = 1;
    public float spinTime = 1;

    [Header("Random")]
    public bool random;
    private float randomF = 0;
    void Start()
    {
        ogPos = transform.localPosition;
        if (random) randomF = Random.Range(0f, 1) * 2f;
        StartCoroutine(Spin());
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = ogPos + bobHeight * Mathf.Sin(Time.time * bobSpeed + randomF) * Vector3.up;
    }

    public IEnumerator Spin()
    {
        yield return new WaitForSeconds(randomF);
        while (true)
        {
            Quaternion ogRot = transform.rotation;
            Quaternion newRot = transform.rotation * Quaternion.Euler(transform.up * 180);

            float t = 0;
            while (t <= 1)
            {
                transform.rotation = Quaternion.Slerp(ogRot, newRot, Functions.SmoothStep(t));
                t += Time.deltaTime / spinTime;
                yield return null;
            }

            transform.rotation = newRot;

            yield return new WaitForSeconds(spinBreak);
        }
    }
}
