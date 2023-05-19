using UnityEngine;

public class FakeChild : MonoBehaviour
{
    private Transform fakeParent;

    private Vector3 lastPos;
    private Quaternion lastRot;

    private Vector3 childLastPos;
    private void LateUpdate()
    {
        if (fakeParent == null)
            return;

        childLastPos = transform.position;

        //gets change in rotation of fake parent
        var rotChange = fakeParent.rotation * Quaternion.Inverse(lastRot);

        //change object rotation
        if (fakeParent.CompareTag("GravityChanger"))
        {
            transform.rotation = rotChange * transform.rotation;
        }
        else
        {
            transform.Rotate(rotChange.eulerAngles.y * Vector3.up);
        }

        // gets the vector of the distance from parent to object and rotate it by change in parent rotation, then adds new parent position
        transform.position = rotChange * (transform.position - lastPos) + fakeParent.position;

        lastRot = fakeParent.rotation;
        lastPos = fakeParent.position;
    }

    public void SetFakeParent(Transform parent)
    {
        if(parent == null)
        {
            fakeParent = null;
            return;
        }

        fakeParent = parent;
        lastRot = fakeParent.rotation;
        lastPos = fakeParent.position;
    }

    public Vector3 getLeaveVelocity()
    {
        return (transform.position - childLastPos) / Time.deltaTime;
    }
}