using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    private static float MaxTime;       // 時間（カウントダウンしていく）
    public int Seconds;                 // floatからintに変換するのに必要（５～０のカウントダウンであるため）
    public Text text_CountDown;         // 表示用テキスト

    // Start is called before the first frame update
    void Start()
    {
        // カウントダウン開始秒数のセット
        MaxTime = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (MaxTime > 0.0f)
        {
            MaxTime -= Time.deltaTime;
            Seconds = (int)MaxTime;
            text_CountDown.text = Seconds.ToString();
        }
    }
}
