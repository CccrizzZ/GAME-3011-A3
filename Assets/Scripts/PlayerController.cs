using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{


    public GameObject Match3Canvas;
    public GameObject Match3Panel;


    
    void Start()
    {
        Application.targetFrameRate = 60;
        // Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            print("keydown");
            // Cursor.visible = true;
            if (GameObject.FindGameObjectWithTag("Match3Panel") == null)
            {
                Instantiate(Match3Panel, Match3Canvas.transform);
                
            }

        }
    }
}
