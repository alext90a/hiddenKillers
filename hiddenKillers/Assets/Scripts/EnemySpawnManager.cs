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
    Collider mEnemySpawnCollider = null;
    HashSet<Enemy> mEnemiesOnMap = new HashSet<Enemy>();

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
        mEnemySpawnCollider = mEnemySpawnRegion.GetComponent<Collider>();
        if(mEnemySpawnCollider == null)
        {
            Debug.LogError("Enemy spawn collider not found!");
        }
        
	}
	
	// Update is called once per frame
	void Update () {

        mTimeSinceLastSpawn += Time.deltaTime;
        if(mTimeSinceLastSpawn >= GameConstants.kEnemySpawnTime)
        {
            while(mTimeSinceLastSpawn>= GameConstants.kEnemySpawnTime)
            {
                setInRandomPosition();
                if (!isInsideWall())
                {
                    if (!Player.getInstance().isObjectVisibleByPlayer(mEnemySpawnCollider))
                    {
                        mEnemiesOnMap.Add(mEnemySpawnRegion.instantiateEnemy());
                        Debug.Log("enemy created");
                        mTimeSinceLastSpawn = 0f;
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
	
        
	}

    bool isInsideWall()
    {
        Bounds spawnBounds = mEnemySpawnCollider.bounds;
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

    public void killAllInvisibleEnemies()
    {
        mEnemiesOnMap.RemoveWhere(delegate (Enemy enemy) { return enemy.destroyIfNotVisible(); });
        /*
        foreach(var curEnemy in mEnemiesOnMap)
        {
            
            curEnemy.destroyIfNotVisible();
            //mEnemiesOnMap.Remove(curEnemy);
        }
        */
        int i = 0;
    }

    public void removeEnemyFromMap(Enemy enemy)
    {
        mEnemiesOnMap.Remove(enemy);
    }
}
