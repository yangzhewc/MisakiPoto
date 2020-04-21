using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneChanger : MonoBehaviour
{
    Mosaic script;
    // Start is called before the first frame update
    void Start()
    {
        script = GetComponent<Mosaic>();
        script.SetSize(100.0f);
        do {
            script.SetSize(script.GetSize()-1.0f);
        } while (script.GetSize() <= 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (script.GetSize() <= 100.0f) {
                script.AddSize(1.0f);
            }else if (script.GetSize() >= 0.0f) {
                script.AddSize(-1.0f);
            }
                

        }
    }
}
