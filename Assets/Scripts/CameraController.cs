using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    GameObject Player;


	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Cube");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(20, Player.transform.position.y, Player.transform.position.z);
	}
}
