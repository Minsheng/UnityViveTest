using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickupParent : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device dev;

    public Transform sphere;

    void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
    // Change in Unity: Edit -> Project Settings -> Time -> Fixed Timestep -> 1/90fps
	void FixedUpdate () {
        dev = SteamVR_Controller.Input((int) trackedObj.index);

        if (dev.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You are holding down the trigger");
        }

        if (dev.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("TouchDown the trigger");
        }

        if (dev.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("TouchUp the trigger");
        }

        if (dev.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("PressUp the trigger");
        }

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

        if (dev.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You have collided with " + col.name + " while holding down Touch");
            col.attachedRigidbody.isKinematic = true; // so the rigibody will no longer be affected by the physics system
            col.gameObject.transform.SetParent(gameObject.transform); // refers to the controllers
        }

        if (dev.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You have collided with " + col.name + " while holding down Touch");

            col.gameObject.transform.SetParent(null); // refers to the controllers
            col.attachedRigidbody.isKinematic = false; // so the rigibody will no longer be affected by the physics system

            tossObject(col.attachedRigidbody);
        }
    }

    void tossObject(Rigidbody rigidBody)
    {
        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;

        if (origin != null)
        {
            rigidBody.velocity = origin.TransformVector(dev.velocity);
            rigidBody.angularVelocity = origin.TransformVector(dev.angularVelocity);
        } else
        {
            rigidBody.velocity = dev.velocity;
            rigidBody.angularVelocity = dev.angularVelocity;
        }
    }
}
