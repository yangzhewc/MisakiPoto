using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class field : MonoBehaviour
{
    GameObject Center;          // カメラやフィールドの回転の中心軸（の位置）
    GameObject StageManager;    // 各値が入ってるマネージャーを呼び出す
    manager script;             // マネージャーのスクリプト

    [SerializeField, PersistentAmongPlayMode] public int AppearSlimeCount;  //生成するスライム数

    [SerializeField, PersistentAmongPlayMode] public Vector3 SpawnSlimePos; //スライムごとの位置

    // Start is called before the first frame update
    void Start()
    {
        GameObject SmallSlime = (GameObject)Resources.Load("Prefab/SmallSlime");

        //フィールドの中心軸（の位置）を取得
        Center = GameObject.Find("FieldCenter");

        //ステージマネージャーの取得
        StageManager = GameObject.Find("StageManager");
        //マネージャーが持っているmanagerスクリプト
        script = StageManager.GetComponent<manager>();

        //===================
        //　小スライムの生成
        //===================
        for (int i = 0; i < AppearSlimeCount; i++)
        {
            script.CreatePrefabAsChild
            (
                this.gameObject,
                SmallSlime, 
                SpawnSlimePos,
                SmallSlime.tag
            );
        }
    }

    // Update is called once per frame
    void Update()
    {
        // キーを押している間
        if (Input.anyKey)
        {
            // 移動量
            float ToRotate = 0.0f;//Input.GetAxis("Mouse X");

            //==========================
            //　左にステージが90度傾く
            //==========================
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //trueで左回転
                script.SetTop(script.nowTop, true);
                ToRotate = -90.0f;
				script.operations(-1);

			}

            //==========================
            //　右にステージが90度傾く
            //==========================
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //falseで右回転
                script.SetTop(script.nowTop, false);
                ToRotate = 90.0f;
				script.operations(-1);
			}
            // float mouseInputY = Input.GetAxis("Mouse Y");
            // targetの位置のZ軸を中心に、回転（公転）する
            switch (script.cameraRotate)
            {
                //カメラがＸ軸上にある場合
                case true:
                    transform.RotateAround
                    (
                        Center.transform.position, 
                        Vector3.right, 
                        ToRotate
                    );
                    break;

                //カメラがＺ軸上にある場合
                case false:
                    transform.RotateAround
                    (
                        Center.transform.position, 
                        Vector3.forward, 
                        ToRotate
                    );
                    break;
            }
        }
    }
}
