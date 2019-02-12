using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour {

    Rigidbody rigid; //Rigidbodyを入れる変数
    float speedx; //移動スピードを入れる変数
    float MaxWalkSpeed=2; //移動スピードの上限値
    float JumpForce = 880.0f; //ジャンプ時にかかる力
    int JumpMax; //一度にジャンプできる回数上限
    int JumpCount; //ジャンプした回数の変数

    //赤が0、青が1、黄が2、緑が3、紫が4
    //falseが未所持、trueが所持
    public bool[] StarFrag;

    public Vector3 localGravity; //重力を入れる変数

    GameObject CanePivot; //杖のpivotを入れる変数
    Animator animator; //Animatorの情報を入れる変数

    // Use this for initialization
    void Start () {
        //アタッチしたオブジェクトのRigidbodyを取得する
        this.rigid = GetComponent<Rigidbody>();

        JumpMax = 1; //ジャンプできる回数の初期化

        CanePivot = GameObject.Find("CanePivot");
        animator = CanePivot.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        Move(); //移動
        Attack(); //攻撃
        setLocalGravity(); //重力

    }
    void Move()
    {
        //星のフラグが立っているとき
        if (StarFrag[0] == true)
        {
            JumpMax = 2;
        }
        //ジャンプする
        if (Input.GetKeyDown(KeyCode.LeftShift) && JumpCount <= JumpMax)  
        {
            this.rigid.velocity = new Vector3(this.rigid.velocity.x, 0.01f, this.rigid.velocity.z);
            this.rigid.AddForce(transform.up * this.JumpForce);
            JumpCount++;
        }
        //上下に動いていないとき
        if(this.rigid.velocity.y == 0)
        {
            JumpCount = 1;
        }

        //Wを押してスピードが0.5以下の時前進
        if (Input.GetKey(KeyCode.RightArrow) && speedx < 0.1f)
        {
            speedx += 0.01f;
        }
        //Sを押してスピードが-0.5以上の時後退
        else if (Input.GetKey(KeyCode.LeftArrow) && speedx > -0.1f)
        {
            speedx -= 0.01f;
        }

        //ボタンを離したとき、またはSとWを同時に押すと停止
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow)) 
        {
            speedx = 0;
        }

        //移動処理
        transform.Translate(speedx, 0, 0);
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !animator.GetCurrentAnimatorStateInfo(0).IsName("cane 3") && !animator.IsInTransition(0)) 
        {
            animator.SetTrigger("CaneTrigger");
        }

    }
    void setLocalGravity()
    {
        //重力を変える
        rigid.AddForce(localGravity, ForceMode.Acceleration);
    }
}
