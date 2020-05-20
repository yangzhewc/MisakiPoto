using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    GameObject EventSystem;
    manager script; //manager.csにフェードインに必要な機能が入ってるから仕方なく

    // Start is called before the first frame update
    void Start()
    {
        EventSystem = GameObject.Find("EventSystem");
        script = EventSystem.GetComponent<manager>();
    }

    // Update is called once per frame
    void Update()
    {
 
        // タッチしたら遷移
        if (Input.GetMouseButtonDown(0))
        {
            /*
             * 遷移できるシーンはFile→Build Settings→Scene In Build
             * Scene In Build内に追加されたシーンであれば下記コードで遷移可能
             * 名前のミスがないようにだけ注意
             */
            script.fadeStart();
        }

    }
}
