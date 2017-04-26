using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    float mTimeSinceStart = 0f;

    Collider mCollider = null;
	// Use this for initialization
	void Start () {

        mCollider = GetComponent<Collider>();
        if(mCollider == null)
        {
            Debug.LogError("Collider component not found in Enemy!");
        }
	
	}
	
	// Update is called once per frame
	void Update () {

        mTimeSinceStart += Time.deltaTime;
        if(mTimeSinceStart >= GameConstants.kEnemyLifeTime)
        {
            destroyIfNotVisible();
        }
	
	}

    public bool destroyIfNotVisible()
    {
        if (!Player.getInstance().isObjectVisibleByPlayer(mCollider))
        {
            EnemySpawnManager.getInstance().removeEnemyFromMap(this);
            GameObject.Destroy(gameObject);
            return true;
        }
        return false;
    }
}
