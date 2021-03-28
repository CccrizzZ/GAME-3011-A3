using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{

    GameObject canvasRef;
    GameObject M3Panel;
    GameObject GameoverPanel;

    public GameObject M3PanelPrefab;


    void Start() {
        M3Panel = GameObject.FindGameObjectWithTag("Match3Panel");
        canvasRef = GameObject.FindGameObjectWithTag("Match3Canvas");
        GameoverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");

    }

    public void RestartButtonBehavior()
    {
        Destroy(M3Panel);
        Destroy(GameoverPanel);
        Instantiate(M3PanelPrefab, canvasRef.transform);

    }

    public void QuitButtonBehavior()
    {
        // canvasRef.SetActive(false);
        Destroy(M3Panel);
        Destroy(GameoverPanel);
        
    }
}
