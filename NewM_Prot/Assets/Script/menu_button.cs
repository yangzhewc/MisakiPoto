using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

  

    }

    public void OnClick_easy()
    {
        SceneManager.LoadScene("EASY");
    }

    public void OnClick_normal()
    {
        SceneManager.LoadScene("EASY+");
    }

    public void OnClick_hard()
    {
        SceneManager.LoadScene("HARD");
    }

}
