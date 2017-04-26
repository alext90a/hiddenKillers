using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnManager : MonoBehaviour {

    [SerializeField]
    Collider mSpawnArea = null;
    [SerializeField]
    EnemySpawnRegion mEnemySpawnRegion = null;

    static EnemySpawnManager mInstance = null;


    float mTimeSinceLastSpawn = 0f;
    float mMaxX, mMaxY, mMinY, mMinX;
    List<Bounds> mWallBounds = new List<Bounds>();

    public static EnemySpawnManager getInstance()
    {
        return mInstance;
    }

    private void Awake()
    {
        mInstance = this;
    }

	// Use this for initialization
	void Start () {
        Bounds bounds = mSpawnArea.bounds;
        mMaxX = bounds.max.x;
        mMaxY = bounds.max.y;

        mMinX = bounds.min.x;
        mMinY = bounds.min.y;

        
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetKeyDown(KeyCode.R))
        {
            setInRandomPosition();
            if(!isInsideWall())
            {
                if(!mEnemySpawnRegion.isVisibleByPlayer())
                {
                    mEnemySpawnRegion.instantiateEnemy();
                    Debug.Log("enemy created");
                }
                else
                {
                    Debug.Log("Spawn aborted by player");
                }

            }
            else
            {
                Debug.Log("Spawn aborted by wall");
            }
        }
	}

    bool isInsideWall()
    {
        Bounds spawnBounds = mEnemySpawnRegion.GetComponent<Collider>().bounds;
        for(int i=0; i<mWallBounds.Count; ++i)
        {
            if (mWallBounds[i].Intersects(spawnBounds))
            {
                return true;
            }            
        }
        return false;
    }

    void setInRandomPosition()
    {
        float randomX = Random.Range(mMinX, mMaxX);
        float randomZ = Random.Range(mMinY, mMaxY);
        Vector3 pos = mEnemySpawnRegion.transform.position;
        pos.x = randomX;
        pos.z = randomZ;
        mEnemySpawnRegion.transform.position = pos;
    }

    public void addWallBounds(Bounds bounds)
    {
        mWallBounds.Add(bounds);
    }
}
