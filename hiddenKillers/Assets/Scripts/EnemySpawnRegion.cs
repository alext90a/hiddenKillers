using UnityEngine;
using System.Collections;

public class EnemySpawnRegion : MonoBehaviour {

    [SerializeField]
    Camera mPlayerCamera = null;
    [SerializeField]
    GameObject mEnemyPrefab = null;
    
    Collider mCollider;

    //bool mIsInsideWall = false;
    Plane[] mPlanes;


    // Use this for initialization
    void Start () {
        mCollider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameConstants.kWallTag))
        {
            mIsInsideWall = true;
            Debug.Log("Spawner inside wall");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(GameConstants.kWallTag))
        {
            mIsInsideWall = false;
            Debug.Log("Spawner outside wall");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(GameConstants.kWallTag))
        {
            mIsInsideWall = true;
            Debug.Log("Spawner inside wall");
        }
    }

    public bool isInsideWall()
    {
        return mIsInsideWall;
    }
    */

    public bool isVisibleByPlayer()
    {
        mPlanes = GeometryUtility.CalculateFrustumPlanes(mPlayerCamera);
        if (GeometryUtility.TestPlanesAABB(mPlanes, mCollider.bounds))
        {
            return true;
        }
        return false;
    }

    public void instantiateEnemy()
    {
        GameObject instantiatedObj = GameObject.Instantiate(mEnemyPrefab, transform.position, transform.rotation) as GameObject;
        instantiatedObj.SetActive(true);
    }
}
