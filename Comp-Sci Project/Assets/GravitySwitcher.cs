using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitcher : MonoBehaviour
{
    public Transform target;
    private Vector3 targetVector;
    private void Start()
    {
        targetVector = target != null ? (target.position - transform.position).normalized : -transform.up;
        if (target != null)
        {
            transform.rotation = Quaternion.LookRotation(transform.forward, -targetVector);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (Physics.gravity.normalized == targetVector) return;
            WonkGravity.singleton.ChangeGravity(targetVector);
            AudioManager.singleton.Play("GravitySwitch");
        }
    }
}
