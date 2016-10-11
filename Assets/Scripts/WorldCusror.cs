using UnityEngine;
using System.Collections;

public class WorldCusror : MonoBehaviour {

    private MeshRenderer meshRenderer;

	// Use this for initialization
	void Start () {
        meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        var headPosition = Camera.main.transform.position;
        var gazePosition = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if(Physics.Raycast(headPosition, gazePosition, out hitInfo))
        {
            // if the Raycast hit a hologram
            // display the cursor mesh
            meshRenderer.enabled = true;

            // Move the cursor to the point where the raycase hit.
            this.transform.position = hitInfo.point;

            //rotate the cursor to hug the surface of the hologram
            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
        else
        {
            // if the raycast did not hit a hologram, hide the cursor mesh
            meshRenderer.enabled = false;
        }
	}
}
