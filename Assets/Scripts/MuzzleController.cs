using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleController : MonoBehaviour {

    public GameObject Magic1; //魔法1を入れる変数
    float MagicSpeed = 10; //魔法の弾の速さ 
    GameObject magic; //生成した魔法を一時的に保存する変数
    float MagicScale; //魔法の大きさ

    // Use this for initialization
    void Start () {
        MagicScale = 1f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //魔法を生成
    public void Magic()
    {
        magic = Instantiate(Magic1) as GameObject;
        magic.transform.position = transform.position;
    }

    //魔法のチャージ
    public void MagicCharge()
    {
        magic.transform.position = transform.position;
        MagicScale += 0.02f;
        magic.transform.localScale = new Vector3(MagicScale, MagicScale, MagicScale);
    }

    //魔法の発射
    public void MagicFire()
    {
        Vector3 force;
        magic.transform.forward = transform.forward;
        force = transform.forward * MagicSpeed * 100;
        magic.GetComponent<Rigidbody>().AddForce(force);
        MagicScale = 1f;
    }


}
