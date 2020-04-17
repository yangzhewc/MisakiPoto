using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchStateManager;

public class Touch : MonoBehaviour
{
    GameObject Manager;
    manager script;
    StateManager m_TouchManager;
    public LayerMask mask;          //特定レイヤーのみ判定衝突を行うようにするためのマスク、Unity上で設定（TouchManagerインスペクタ内）
    GameObject startObj;            //タッチ始点にあるオブジェクトを格納
    GameObject endObj;              //タッチ終点にあるオブジェクトを格納
    List<GameObject> removableSlimeList = new List<GameObject>();    //削除するスライムのリスト
    public string currentName;       //タグ判定用のstring変数
    //=========================
    // 初期化処理
    //=========================
    void Start()
    {
        // タッチ管理マネージャ生成
        this.m_TouchManager = new StateManager();
    }

    //=========================
    // 更新処理
    //=========================
    void Update()
    {
        // タッチ状態更新
        this.m_TouchManager.update();

        // タッチ状態の取得
        StateManager TouchState = this.m_TouchManager.GetTouch();

        // タッチされている時
        if (TouchState.IsTouch)
        {
            Debug.Log("タッチ開始、れいびゅ～ん");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (startObj == null)
            {
                // Rayが特定レイヤの物体（スライム）に衝突している場合
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {
                    //　大スライムにRayが衝突している時
                    if (hit.collider.gameObject.CompareTag("BigSlime"))
                    {
                        //タッチしたらあぼ～～ん（今は消えるだけ)
                        //処理内容はslimeControl.csのBigSlimeClickAct()の中
                        hit.collider.gameObject.GetComponent<slimeControl>().SlimeDestroy();
                    }
                    //　小、中スライムにRayがぶつかった時
                    else if (hit.collider.gameObject.CompareTag("MiddleSlime") ||
                                hit.collider.gameObject.CompareTag("SmallSlime"))
                    {
                        GameObject hitObj = hit.collider.gameObject;

                        currentName = hitObj.tag;

                        //スライムオブジェクトを格納
                        startObj = hitObj;
                        endObj = hitObj;

                        //削除対象オブジェクトリストの初期化
                        removableSlimeList = new List<GameObject>();

                        //削除対象のオブジェクトを格納
                        PushToList(hitObj);
                    }
                }
                else
                {
                    // タッチした場所がスライム以外の場合の処理はここ

                }
            }
            //タッチ終了時
            else if(TouchState.Phase == TouchPhase.Ended)
            {
                int remove_cnt = removableSlimeList.Count;

                if (remove_cnt == 2)
                {
                    Debug.Log("スライムサイズでかくなりますはい");

                    //リスト内のスライムを消す
                    GameObject.Destroy(startObj);
                    GameObject.Destroy(endObj);

                    Manager = GameObject.Find("StageManager");
                    script = Manager.GetComponent<manager>();
                   
                    /*
                    if (startObj.CompareTag("MiddleSlime"))
                    {
                        script.CreateSlime((int)manager.SlimeSize.middle, endObj);
                    }
                    else if (startObj.CompareTag("SmallSlime"))
                    {
                        script.CreateSlime((int)manager.SlimeSize.small,endObj);
                    }
                    */
                    //スコアと消えるときの音はここ↓※これは昔つくったやーつ

                    //scoreGUI.SendMessage("AddPoint", point * remove_cnt);
                    //efxSource.Play();
                }
                //消す対象外の時
                else
                {
                    for (int i = 0; i < remove_cnt; i++)
                    {
                        //色をもとに戻す
                        ChangeColor(removableSlimeList[i], 1.0f);
                    }
                }

                startObj = null;
                endObj = null;
            }
            else if(TouchState.Phase == TouchPhase.Moved && startObj != null)
            {
                // Rayが特定レイヤの物体（スライム）に衝突している場合
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {
                    if (hit.collider != null)
                    {
                        GameObject hitObj = hit.collider.gameObject;

                        //同じ名前のブロックをクリック＆lastBallとは別オブジェクトである時
                        if (hitObj.tag == currentName && endObj != hitObj)
                        {
                            //２つのオブジェクトの距離を取得

                            float distance = Vector2.Distance(hitObj.transform.position, endObj.transform.position);

                            if (distance < 5.0f)
                            {
                                Debug.Log("そのオブジェクト消えるよ");
                                //削除対象のオブジェクトを格納
                                endObj = hitObj;
                                //削除対象のオブジェクトを格納
                                PushToList(hitObj);
                            }
                        }
                    }
                }
            }
            
        }
    }
    //=============================================
    //　オブジェクトの色を変える
    //=============================================
    void ChangeColor(GameObject obj, float Alpha)
    {
        //SpriteRendererコンポーネントを取得
        //SpriteRenderer SlimeTexture = obj.GetComponent<SpriteRenderer>();

        //Colorプロパティのうち、透明度のみ変更(色の透明度をAlpha%に変更)
       // SlimeTexture.color = new Color(SlimeTexture.color.r, SlimeTexture.color.g, SlimeTexture.color.b, Alpha);
    }

    //==============================================================
    //　選択されているスライムを除去リストに格納し、色を半透明にする
    //==============================================================
    void PushToList(GameObject obj)
    {
        //除去リストに選択しているオブジェクトを追加
        removableSlimeList.Add(obj);

        //色の透明度を50%に変更)
       // ChangeColor(obj, 0.5f);
    }
}
