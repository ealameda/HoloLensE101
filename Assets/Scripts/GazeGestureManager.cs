using UnityEngine;
using UnityEngine.VR.WSA.Input;
using System.Collections;

public class GazeGestureManager : MonoBehaviour {

    public static GazeGestureManager Instance { get; private set; }

    public GameObject FocusedObject { get; private set; }

    GestureRecognizer recognizer;

	// Use this for initialization
	void Start () {
        Instance = this;

        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            // send an OnSelect message to the focused object and it's anscestors
            if (FocusedObject != null)
            {
                FocusedObject.SendMessageUpwards("OnSelect");
            }

        };
        recognizer.StartCapturingGestures();
	}
	
	// Update is called once per frame
	void Update () {
        //Figure out which hologram is focused this frame
        GameObject oldFocusObject = FocusedObject;

        // do a raycast into the world based on the users head position and orientation
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if(Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            FocusedObject = hitInfo.collider.gameObject;
        } else
        {
            FocusedObject = null;
        }

        if (FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
	}
}
