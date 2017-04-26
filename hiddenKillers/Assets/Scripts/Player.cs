using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [SerializeField]
    FP_Input mPlayerInput;
    [SerializeField]
    Camera mMainCamera = null;

    static Player mInstance;

    Plane[] mPlanes;

    public static Player getInstance()
    {
        return mInstance;
    }

    private void Awake()
    {
        mInstance = this;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(mPlayerInput.Shoot())
        {
            EnemySpawnManager.getInstance().killAllInvisibleEnemies();
        }
	}

    public bool isObjectVisibleByPlayer(Collider collider)
    {
        mPlanes = GeometryUtility.CalculateFrustumPlanes(mMainCamera);
        if (GeometryUtility.TestPlanesAABB(mPlanes, collider.bounds))
        {
            return true;
        }
        return false;
    }
}
