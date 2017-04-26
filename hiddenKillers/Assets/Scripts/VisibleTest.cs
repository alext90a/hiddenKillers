using UnityEngine;
using System.Collections;

public class VisibleTest : MonoBehaviour {

    public GameObject anObject;
    public Collider anObjCollider;
    private Camera cam;
    private Plane[] planes;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        anObjCollider = GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update () {
        cam = Camera.main;
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        if (GeometryUtility.TestPlanesAABB(planes, anObjCollider.bounds))
            Debug.Log(anObject.name + " has been detected!");
        else
            Debug.Log("Nothing has been detected");

    }
}
