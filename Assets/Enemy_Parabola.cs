using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Parabola : MonoBehaviour {

    GameObject Player;
    private const string MainCamera = "MainCamera";

    //カメラに表示されているか
    private bool _isRendered = false;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        //敵が表示されているか判断
        if (_isRendered)
        {
            Debug.Log("カメラに映ってるよ！");

            // 自分の位置とプレイヤーの位置から向きベクトルを作成しRayに渡す
            Vector3 direction = (Player.transform.position - transform.position).normalized;
            Ray ray = new Ray(transform.position, direction);

            //Rayが当たったオブジェクトの情報を入れる箱
            RaycastHit hit;
            //Rayの飛ばせる距離  最終的に画面のサイズ等で微調整が必要
            int distance = 10;

            if (Physics.Raycast(ray, out hit, distance))
            {
                //Rayが当たったオブジェクトのtagがPlayerだったら
                if (hit.collider.tag == "Player")
                {
                    Debug.Log("RayがPlayerに当たった");
                    //各手機能攻撃を入れる
                }
            }
        }
        else
        {
            Debug.Log("カメラに映ってないよ！");
        }
        _isRendered = false;
    }

    //カメラに映ってる間に呼ばれる
    private void OnWillRenderObject()
    {
        //メインカメラに映った時だけ_isRenderedを有効に
        if (Camera.current.tag == MainCamera)
        {
            _isRendered = true;
        }
    }
}
