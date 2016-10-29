using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ParentFixedJoint : MonoBehaviour {

    public Rigidbody rigidBodyAttachPoint;
    public Transform sphere;

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device dev;

    FixedJoint fixedJoint;

    void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
	
	void FixedUpdate () {
        dev = SteamVR_Controller.Input((int)trackedObj.index);

        if (dev.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Debug.Log("PressUp the touchpad");
            Debug.Log("Reset sphere position and velocity...");
            sphere.transform.position = Vector3.zero;
            sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
            sphere.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    void OnTriggerStay(Collider col)
    {
        Debug.Log("You collided with " + col.name + " and activated OnTriggerStay.");

        if ((fixedJoint == null) && (dev.GetTouch(SteamVR_Controller.ButtonMask.Trigger)))
        {
            fixedJoint = col.gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = rigidBodyAttachPoint;
        } else if (fixedJoint != null && dev.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            GameObject go = fixedJoint.gameObject;
            Rigidbody rb = go.GetComponent<Rigidbody>();
            Object.Destroy(fixedJoint);
            fixedJoint = null;
            tossObject(rb);
        }
    }

    void tossObject(Rigidbody rigidBody)
    {
        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;

        if (origin != null)
        {
            rigidBody.velocity = origin.TransformVector(dev.velocity);
            rigidBody.angularVelocity = origin.TransformVector(dev.angularVelocity);
        }
        else
        {
            rigidBody.velocity = dev.velocity;
            rigidBody.angularVelocity = dev.angularVelocity;
        }
    }
}
