using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAlphaChange : MonoBehaviour
{
    public Text text;
    float Alpha;
    bool Flag;  // trueの時アルファ値を下げ、falseの時にアルファ値を上げる

    // Use this for initialization
    void Start()
    {
        Alpha = 0;
    }
    // Update is called once per frame
    void Update()
    {
        //テキストの透明度を変更する
        text.color = new Color(0.5f, 0.78f, 0.78f, Alpha);

        if (Flag)
        {
            Alpha -= Time.deltaTime;
        }
        else
        {
            Alpha += Time.deltaTime;
        }
        if (Alpha < 0)
        {
            Alpha = 0;
            Flag = false;
        }
        else if (Alpha > 1)
        {
            Alpha = 1;
            Flag = true;
        }
    }
}
