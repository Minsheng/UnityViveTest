using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickUpRandom : MonoBehaviour {
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device dev;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void FixedUpdate()
    {
        dev = SteamVR_Controller.Input((int)trackedObj.index);

        if (dev.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            resetScene();
        }
    }

    void OnTriggerStay(Collider col)
    {
        Debug.Log("You collided with " + col.name + " and activated OnTriggerStay.");

        if (dev.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You have collided with " + col.name + " while holding down Touch");

            col.gameObject.transform.SetParent(gameObject.transform); // refers to the controllers
        }

        if (dev.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You have collided with " + col.name + " while holding down Touch");

            col.gameObject.transform.SetParent(null); // refers to the controllers
        }
    }

    void resetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
