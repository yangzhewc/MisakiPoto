using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeControl : MonoBehaviour
{
    GameObject Manager;
    manager script;
    GameObject Field;
    field F_script;
    bool isMove;

    // Start is called before the first frame update
    void Awake()
    {
        Manager = GameObject.Find("StageManager");
        script = Manager.GetComponent<manager>();

    //    Field = transform.parent.gameObject;//GameObject.Find("Field");
     //   F_script = Field.GetComponent<field>();
        Physics.gravity = new Vector3(0, 0, 0);
        isMove = false;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (!isMove && (Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.RightArrow)))
        {
            isMove = true;
            Physics.gravity = new Vector3(0, -10, 0);
        }
        switch (script.nowTop)
        {
            case (int)manager.Wall.Top:
                //処理
                break;
            case (int)manager.Wall.Bottom:
                //処理
                break;
            case (int)manager.Wall.Left:
                //処理
                break;
            case (int)manager.Wall.Right:
                //処理
                break;

            default:break;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == this.gameObject.tag) {
            switch (this.gameObject.tag) {
                //大スライム同士が接触した場合
                case "BigSlime":
                    Destroy(this.gameObject);
					FindObjectOfType<Score>().AddPoint(10);
					break;
                case "MiddleSlime":
                    script.CreateSlime((int)manager.SlimeSize.middle, this.gameObject);
					FindObjectOfType<Score>().AddPoint(10);
					break;
                case "SmallSlime:":
                    script.CreateSlime((int)manager.SlimeSize.small, this.gameObject);
					FindObjectOfType<Score>().AddPoint(10);
					break;
                default:
                    break;
            }
        }
    }
        // private void OnCollisionEnter(Collision collision)
        // {
        //     if (collision.gameObject.tag == this.gameObject.tag)
        //     {
        //         switch (this.gameObject.tag)
        //         {
        //             //大スライム同士が接触した場合
        //             case "BigSlime":
        //                 //大スライムを消す
        //                 Destroy(this.gameObject);
        ////		FindObjectOfType<Score>().AddPoint(10);

        //		//デバッグ表記
        //		Debug.Log("BiggestSlimes Disappear!");

        //                 break;

        //             //中スライム同士が接触した場合
        //             case "MiddleSlime":

        //                 //=========================
        //                 // 大スライムを作成する
        //                 //=========================
        //                 Vector3 tmp = this.gameObject.transform.position;   //生成位置（＝変更前の位置)取得
        //                 GameObject OYA = transform.parent.gameObject;       //親クラス取得
        //                 Destroy(this.gameObject);                           //中スライムを消す
        //	//	FindObjectOfType<Score>().AddPoint(10);

        //		//プレハブを取得


        //		script.CreatePrefabAsChild
        //                 (
        //                     OYA,                                            //親クラス
        //                     (GameObject)Resources.Load("Prefab/BigSlime"),  //プレハブ選択
        //                     tmp,                                            //生成位置
        //                     "BigSlime"                                      //タグ名
        //                 );    

        //                 break;

        //             //小ライム同士が接触した場合
        //             case "SmallSlime":
        //                 //=========================
        //                 // 中スライムを作成する
        //                 //=========================   
        //                 Vector3 tmp2 = this.gameObject.transform.position;  //生成位置（＝変更前の位置)取得               
        //                 GameObject OYA2 = transform.parent.gameObject;      //親クラス取得  
        //                 Destroy(this.gameObject);                           //小スライムを消す             
        //	//	FindObjectOfType<Score>().AddPoint(10);

        //		script.CreatePrefabAsChild
        //                 (
        //                     OYA2,                                           //親クラス
        //                     (GameObject)Resources.Load("Prefab/MiddleSlime"),      //プレハブ選択
        //                     tmp2,                                           //生成位置
        //                     "MiddleSlime"                                   //タグ名
        //                 );

        //                 break;

        //             default:
        //                 break;
        //         }

        //     }
        // }
    }
