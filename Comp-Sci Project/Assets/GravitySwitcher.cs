using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitcher : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (Physics.gravity.normalized == -transform.up) return;
            WonkGravity.singleton.ChangeGravity(-transform.up);
            AudioManager.singleton.Play("GravitySwitch");
        }
    }
}
