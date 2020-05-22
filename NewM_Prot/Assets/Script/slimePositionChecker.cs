using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimePositionChecker : MonoBehaviour
{
    GameObject managerObject;
    manager managerScript;
    public bool PosChecker;
    public bool isChange;
    public Vector3 pos;
    bool changer;
    // Start is called before the first frame update
    void Start()
    {
        managerObject = GameObject.Find("StageManager");
        managerScript = managerObject.GetComponent<manager>();
        PosChecker = false;
        isChange = managerScript.isRotate;
        pos = gameObject.transform.position;
        changer=false;
    }

    // Update is called once per frame
   
    void Update()
    {
        if (managerScript.isRotate == true) {
            isChange = managerScript.isRotate;
        }
        if (isChange == true && managerScript.isRotate == false) {
            changer = true;
           
            isChange = managerScript.isRotate;
        
        }
        if (changer == true&&isChange == false && managerScript.isRotate == false ) {
      
            //親から見たこの方向
            //Vector3 direction = GameObject.Find("FieldCenter").transform.position-this.gameObject.transform.position;

            ////ローカル位置取得
            ////位置を親から見た位置でだす
            //this.gameObject.transform.position=transform.InverseTransformDirection(direction);
            Debug.Log("スライム位置確認");
            changer = false;
        }

    }
}
