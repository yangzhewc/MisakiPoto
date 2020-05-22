using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    [SerializeField] GameObject menuPanel = default;
    [SerializeField] GameObject StageChoicePanel_1 = default;
    [SerializeField] GameObject StageChoicePanel_2 = default;
    [SerializeField] GameObject StageChoicePanel_3 = default;
    [SerializeField] GameObject HAZIMARINOUMI = default;
    [SerializeField] GameObject SEIREINOMORI = default;
    [SerializeField] GameObject KARAPPONOKAZAN = default;
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
    public void StageChoice_SEIREINOMORI()
    {
        Debug.Log("せいれいのもりステージ");
        // menuPanelはActive(true)のままにする
        SEIREINOMORI.SetActive(true);
        StageChoicePanel_2.SetActive(false);
    }
    public void StageChoice_KARAPPONOKAZAN()
    {
        Debug.Log("からっぽのかざんステージ");
        // menuPanelはActive(true)のままにする
        KARAPPONOKAZAN.SetActive(true);
        StageChoicePanel_3.SetActive(false);
    }

    //ステージセレクト画面１に戻るとき
    public void InitStageChoice()
    {
        Debug.Log("ステージセレクト画面");
        menuPanel.SetActive(true);
        StageChoicePanel_1.SetActive(true);
        StageChoicePanel_2.SetActive(true);
        StageChoicePanel_3.SetActive(true);
        HAZIMARINOUMI.SetActive(false);
        SEIREINOMORI.SetActive(false);
        KARAPPONOKAZAN.SetActive(false);
    }

    // ここから各ステージの遷移
    public void Stage1_1()
    {
        Debug.Log("はじまりのうみ１－１に遷移");
        SceneManager.LoadScene("Scenes/stage1/1");
    }

    public void Stage2_1()
    {
        Debug.Log("森1-1に移動");
        SceneManager.LoadScene("Scenes/stage2/1");
    }
    public void Stage3_1()
    {
        Debug.Log("からっぽ火山1-1に移動");
        SceneManager.LoadScene("Scenes/stage3/1");
    }
}
