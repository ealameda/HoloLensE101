using UnityEngine;
using System.Collections;

public class SphereCommands : MonoBehaviour {
    Vector3 originalPosition;

	// Use this for initialization
    void Start()
    {
        // Grab the original local position of the sphere when the app starts
        originalPosition = this.transform.localPosition;
    }

	void OnSelect () {
	    // if the sphere has no rigid body component, add one to enable physics
        if (!this.GetComponent<Rigidbody>())
        {
            var rigidBody = this.gameObject.AddComponent<Rigidbody>();
            rigidBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
	}

    void OnReset()
    {
        // if the sphere has a rigidbody component, remove it to disable physics
        var rigidBody = this.GetComponent<Rigidbody>();
        if (rigidBody != null)
        {
            DestroyImmediate(rigidBody);
        }

        // put the sphere back into its origingal position
        this.transform.localPosition = originalPosition;
    }

    // called by speech manager when the user says the drop spehere command
    void OnDrop()
    {
        OnSelect();
    }
}
