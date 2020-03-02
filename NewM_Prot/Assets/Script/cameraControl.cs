using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    GameObject targetObj;
    GameObject Manager;
    Vector3 targetPos;
    manager script;

    // Start is called before the first frame update
    void Start()
    {
        targetObj = GameObject.Find("FieldCenter");
        targetPos = targetObj.transform.position;

        Manager = GameObject.Find("StageManager");
        script = Manager.GetComponent<manager>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraRoll();
    }

    void CameraRoll()
    {
        // targetの移動量分、自分（カメラ）も移動する
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;

        // キーを押している間
        if (Input.anyKey) {
            // カメラ移動量
            float InputX = 0f;//Input.GetAxis("Mouse X");
            if (Input.GetKeyDown(KeyCode.A)) {
                InputX = -90.0f;
                script.changeCameraRotate();
            }

            if (Input.GetKeyDown(KeyCode.D)) {
                InputX = 90.0f;
                script.changeCameraRotate();

            }
                
            // float mouseInputY = Input.GetAxis("Mouse Y");
            // targetの位置のY軸を中心に、回転（公転）する
            transform.RotateAround(targetPos, Vector3.up, InputX );
            // カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
            // transform.RotateAround(targetPos, transform.right, mouseInputY * Time.deltaTime * 200f);
        }
    }
}
