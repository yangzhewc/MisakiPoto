using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject StageChoicePanel_1;
    [SerializeField] GameObject HAZIMARINOUMI;

    // Start is called before the first frame update
    void Start()
    {
        //BackToMenuメソッドを呼び出す
        InitStageChoice();
    }


    // ステージセレクト画面１ではじまりのうみステージを選択した場合
    public void StageChoice_HAZIMARINOUMI()
    {
        Debug.Log("はじまりのうみステージ");
        // menuPanelはActive(true)のままにする
        HAZIMARINOUMI.SetActive(true);
        StageChoicePanel_1.SetActive(false);
    }

    //ステージセレクト画面１に戻るとき
    public void InitStageChoice()
    {
        Debug.Log("ステージセレクト画面");
        menuPanel.SetActive(true);
        StageChoicePanel_1.SetActive(true);
        HAZIMARINOUMI.SetActive(false);
    }

    // ここから各ステージの遷移
    public void Stage1_1()
    {
        Debug.Log("はじまりのうみ１－１に遷移");
        SceneManager.LoadScene("Scenes/kameyama/1");
    }

}
