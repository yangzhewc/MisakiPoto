using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottun : MonoBehaviour
{
    public bool ClickSide;//左右どちらかを判定する。L=false,R=True
    public bool isClicked;
    manager script;

    // Start is called before the first frame update
    void Start()
    {
        isClicked = false;
        script = GameObject.Find("StageManager").GetComponent<manager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()
    {
        isClicked = true;
        Debug.Log("押された");
    }
    public void SetisClicked(bool check)
    {
        isClicked = check;
    }
}
