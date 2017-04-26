using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EnemySpawnManager.getInstance().addWallBounds(GetComponent<Collider>().bounds);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
