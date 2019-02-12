using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Totu : MonoBehaviour {

    GameObject Player;
    private const string MainCamera = "MainCamera";

    //カメラに表示されているか
    private bool _isRendered = false;
    int i = 0;
    float x = 0.0f, y = 0.0f, span = 0.0f, idou = 0.0f;
    public static int HP = 5;
    
    // Use this for initialization
	void Start () {
        Player = GameObject.FindWithTag("Player");
	}

    // Update is called once per frame
    private void Update()
    {
        // 座標を取得
        Vector3 pos = transform.position;
        //敵が表示されているか判断
        if (_isRendered)
        {
            //写っているので動かす
            Debug.Log("カメラに映ってるよ！");

            // 自分の位置とプレイヤーの位置から向きベクトルを作成しRayに渡す
            Vector3 direction = (Player.transform.position - transform.position).normalized;
            Ray ray = new Ray(transform.position, direction);

            //Rayが当たったオブジェクトの情報を入れる
            RaycastHit hit;

            //Rayの飛ばせる距離  数字を最終的に画面のサイズ等で微調整が必要の可能性あり
            int distance = 10;

            // transformを取得
            Transform myTransform = this.transform;

            int layerMask = 1;
            Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
            //敵キャラを歩かせる 制限を描ける
            switch (i)
            {
                case 0:
                    if (transform.position.x - 5.0f > pos.x)
                    {
                        myTransform.Translate(-0.05f, 0.0f, 0.0f);
                    }
                    else
                        i = 1;
                    break;
                case 1:
                    if (transform.position.x + 5.0f < pos.x)
                    {
                        myTransform.Translate(0.05f, 0.0f, 0.0f);
                    }
                    else i = 0;
                    break;
                default:
                    break;
            }
            
            if (Physics.Raycast(ray, out hit, distance))
            { 
                //Rayが当たったオブジェクトのtagがPlayerだったら
                if (hit.collider.tag == "Player")
                {
                    Debug.Log("RayがPlayerに当たった");
                    //各手機能攻撃を入れる
                    span += Time.deltaTime;
                    if (span > 2.0f)
                    {
                        //プレイヤーと敵の位置を取得し差分移動
                        x = Player.transform.position.x - transform.position.x;
                        y = Player.transform.position.y - transform.position.y;
                        idou += Time.deltaTime;
                        if (idou > 1.0f)
                        {
                            transform.Translate(x, y, 0.0f);
                            
                        }
                        idou = 0.0f;
                        span = 0.0f;
                    }
                    //x = 0.0f;
                    //y = 0.0f;
                }
            }
        }
        else
        {
            //写ってないので動かさない
            Debug.Log("カメラに映ってないよ！");
            //カメラのフレームから外れたので元の位置に戻す
            transform.position = pos;
            //←最初に取った座標代入
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
