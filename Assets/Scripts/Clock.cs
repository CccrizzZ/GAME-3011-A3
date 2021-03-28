using UnityEngine;
using UnityEngine.UI;


public class Clock : MonoBehaviour
{
    public Text ClockSecondsUI;
    
    public GameObject GameOverPanel;
    GameObject CanvasRef;

    public int TimeLimit;

    
    public bool isTicking;

    public int timer;

    void Start()
    {
        
        CanvasRef = GameObject.FindGameObjectWithTag("Match3Canvas");


        // ClockSecondsUI = GameObject.FindGameObjectWithTag("ClockSeconds").GetComponent<Text>();
        timer = TimeLimit;
        ClockSecondsUI.text = timer.ToString();
    }

    void Update()
    {
        if (isTicking)
        {
            CountDown();
            
        }

    }

    void CountDown()
    {
        if (Time.frameCount % 60 == 0)
        {
            // if timeup, show the game over panel
            if (timer <= 0)
            {
                // print("Failed");
                isTicking = false;

                // instantiate gameover panel
                var newpanel = Instantiate(GameOverPanel, CanvasRef.transform);
                GetComponent<Match3Canvas>().DisableAllCells();

            }
            else
            {
                // set timer
                timer--;
            }

            // update timer ui
            ClockSecondsUI.text = timer.ToString();
            
        }

    }

    public void ResetClock()
    {
        timer = TimeLimit;
    }

}
