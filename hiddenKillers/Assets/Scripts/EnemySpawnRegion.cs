using UnityEngine;
using System.Collections;

public class EnemySpawnRegion : MonoBehaviour {


    [SerializeField]
    GameObject mEnemyPrefab = null;
    




    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}




    public Enemy instantiateEnemy()
    {
        GameObject instantiatedObj = GameObject.Instantiate(mEnemyPrefab, transform.position, transform.rotation) as GameObject;
        instantiatedObj.SetActive(true);
        return instantiatedObj.GetComponent<Enemy>();
    }
}
