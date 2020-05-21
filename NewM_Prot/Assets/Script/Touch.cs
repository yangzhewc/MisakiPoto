using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchStateManager;

public class Touch : MonoBehaviour
{
    StateManager m_TouchManager;
    public LayerMask mask;          // 特定レイヤーのみ判定衝突を行うようにするためのマスク、Unity上で設定（TouchManagerインスペクタ内）
    private GameObject startObj;            // タッチ始点にあるオブジェクトを格納
     private GameObject endObj;              // タッチ終点にあるオブジェクトを格納
    List<GameObject> removableSlimeList = new List<GameObject>();    // 削除するスライムのリスト
    public string currentName;       // タグ判定用のstring変数

    //マネージャー読み込み======
    public GameObject managerObj;
    manager managerScript;
    //=======================亀山

    //=========================
    // 初期化処理
    //=========================
    void Start()
    {
        // タッチ管理マネージャ生成
        this.m_TouchManager = new StateManager();
        managerObj = GameObject.Find("StageManager");
        managerScript = managerObj.GetComponent<manager>();

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
            Debug.Log("タッチ開始");
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
                        Debug.Log("爆発");

                        // 処理内容はslimeControl.csのBigSlimeClickAct()の中
                        hit.collider.gameObject.GetComponent<slimeControl>().SlimeDestroy();
                    }
                    //　小、中スライムにRayがぶつかった時
                    else if (hit.collider.gameObject.CompareTag("MiddleSlime") ||
                             hit.collider.gameObject.CompareTag("SmallSlime"))
                    {
                        currentName = hit.collider.gameObject.tag;

                        // スライムオブジェクトを格納
                        startObj = hit.collider.transform.parent.gameObject;
                        endObj = hit.collider.transform.parent.gameObject;

                        // 削除対象オブジェクトリストの初期化
                        removableSlimeList = new List<GameObject>();

                        // 削除対象のオブジェクトを格納
                        PushToList(hit.collider.gameObject);

                        Debug.Log("削除対象追加");
                    }
                }
            }
            //タッチ終了時
            else if(TouchState.Phase == TouchPhase.Ended)
            {
                int remove_cnt = removableSlimeList.Count;

                if (remove_cnt == 2)
                {
                    //中スライムが消された場合
                    if (startObj.CompareTag("MiddleSlime"))
                    {
                        GameObject obj = (GameObject)Resources.Load("Prefab/Fields/FieldInBig");
                        //プレハブを元に、インスタンスを生成
                        GameObject tmp=Instantiate(obj, 
                                    new Vector3
                                    (
                                        (startObj.transform.position.x + endObj.transform.position.x) / 2f,
                                        (startObj.transform.position.y + endObj.transform.position.y) / 2f,
                                        (startObj.transform.position.z + endObj.transform.position.z) / 2f
                                     ),
                                      Quaternion.Euler(CreateSlimeQuarternion()));
                        //生成したプレハブをFieldCenterに登録する。
                        tmp.transform.parent = GameObject.Find("FieldCenter").transform;
                        Debug.Log("終点側に大スライムを生成");
                    }
                    //小スライムが消された場合
                    else if (startObj.CompareTag("SmallSlime"))
                    {
                        GameObject obj = (GameObject)Resources.Load("Prefab/Fields/FieldInMid");
                        //プレハブを元に、インスタンスを生成
                       GameObject tmp= Instantiate(obj,
                                    new Vector3
                                    (
                                         (startObj.transform.position.x + endObj.transform.position.x) / 2f,
                                        (startObj.transform.position.y + endObj.transform.position.y) / 2f,
                                        (startObj.transform.position.z + endObj.transform.position.z) / 2f
                                     ),
                                      Quaternion.Euler(CreateSlimeQuarternion()));
                        //生成したプレハブをFieldCenterに登録する。
                        tmp.transform.parent = GameObject.Find("FieldCenter").transform;
                        Debug.Log("終点側に中スライムを生成");
                    }

                    GameObject.Destroy(startObj);
                    GameObject.Destroy(endObj);
                    // スコアと消えるときの音はここ↓※これは昔つくったやーつ

                    //scoreGUI.SendMessage("AddPoint", point * remove_cnt);
                    //efxSource.Play();
                }
                // 消す対象外の時
                else
                {
                    for (int i = 0; i < remove_cnt; i++)
                    {
                        removableSlimeList[i] = null;
                    }
                }

                // リスト内のスライムを消す
                currentName = null;
                startObj = null;
                endObj = null;
            }
            // タッチ中
            else if(TouchState.Phase == TouchPhase.Moved && startObj != null)
            {
                // Rayが特定レイヤの物体（スライム）に衝突している場合
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {
                    if (hit.collider != null)
                    {
                        GameObject hitObj = hit.collider.transform.parent.gameObject;

                        // 同じタグのブロックをクリック＆endObjとは別オブジェクトである時
                        if (hitObj.tag == currentName && endObj != hitObj)
                        {
                            Debug.Log("同タグの別オブジェクトが選択された");
                            // ２つのオブジェクトの距離を取得
                            float distance = Vector2.Distance(hitObj.transform.position, endObj.transform.position);

                            if (distance <= 5.0f)
                            {
                                Debug.Log("z値を取得し比較");
                                // zが同じであれば
                                if (hitObj.transform.position.z == endObj.transform.position.z)
                                {
                                    Debug.Log("削除します");

                                    // 削除対象のオブジェクトを格納
                                    endObj = hitObj;
                                    // 削除対象のオブジェクトを格納
                                    PushToList(hitObj);
                                }
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
        // SpriteRendererコンポーネントを取得
        //SpriteRenderer SlimeTexture = obj.GetComponent<SpriteRenderer>();

        // Colorプロパティのうち、透明度のみ変更(色の透明度をAlpha%に変更)
        //SlimeTexture.color = new Color(SlimeTexture.color.r, SlimeTexture.color.g, SlimeTexture.color.b, Alpha);
    }

    //==============================================================
    //　選択されているスライムを除去リストに格納し、色を半透明にする
    //==============================================================
    void PushToList(GameObject obj)
    {
        // 除去リストに選択しているオブジェクトを追加
        removableSlimeList.Add(obj);
        obj.name = "_" + obj.name;
        // 色の透明度を50%に変更
        //ChangeColor(obj, 0.5f);
    }

    /*==================================================
     生成されるスライムの角度を
     managerに保存されているカメラ位置に対応したRotateで生成する
     ===================================================    */

    Vector3 CreateSlimeQuarternion()
    {
        //角度別スライム生成
        Vector3 compared = startObj.transform.position;
        Vector3 compare = endObj.transform.position;
        Vector3 prefRotate = new Vector3(0, 0, 0);

        if (managerScript.cameraRotate % 2 == 0)
            prefRotate.y = 0f;
        else
            prefRotate.y = 90f;

        //位置取得。
        if (compared.x == compare.x) {
            //縦長スライム生成
            prefRotate.z = 90f;
        } else if (compared.y == compare.y) {
            //横長スライム生成
            prefRotate.z = 0f;
        }

        return prefRotate;
    }

    public Vector3 GetStartObj()
    {
        return startObj.transform.position;
    }
}
