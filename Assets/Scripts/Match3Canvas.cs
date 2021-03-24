using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Difficulties
{
    EASY,
    MEDIUM,
    HARD
}



public class Match3Canvas : MonoBehaviour
{


    public Difficulties diff;

    // array for storing cell images
    public Sprite[] imageArray;

    // col and row for board
    public int col;
    public int row;



    // references
    public GameObject Cellref;
    RectTransform rectRef;


    // variables
    float CellSize = 100;
    
    // activate board if it stopped moving
    bool activated;
    
    // random id for cell
    int randID;



    void Start()
    {
        // board will be activated after stop moving 
        activated = false;

        rectRef = GetComponent<RectTransform>();
        
        // temp position for cell
        Vector3 tempPos = transform.position;



        // generate & spawn cell
        for (var i = 0; i < col; i++)
        {
            for (var j = 0; j < row; j++)
            {
                // define new cell position
                // tempPos.y = i * 100;
                // tempPos.x = j * 100;
                tempPos.z = 0;

                // spawn cell
                GameObject newCell = Instantiate(Cellref, tempPos, transform.rotation);
                
                // attach cell to the panel
                newCell.transform.SetParent(transform, false);
                

                switch (diff)
                {
                    case Difficulties.EASY:
                        randID = Random.Range(0, 3);
                        break;
                    case Difficulties.MEDIUM:
                        randID = Random.Range(0, 4);
                        break;
                    case Difficulties.HARD:
                        randID = Random.Range(0, 6);
                        break;
                    default:
                        break;
                }
                




                // set cell image and cell id
                newCell.transform.Find("Icon").GetComponent<Image>().sprite = imageArray[randID];
                newCell.GetComponent<CellScript>().CellID = randID;


            }
        }

        // push the panel to the lerp start position
        rectRef.localPosition = new Vector3(0,800,0);
    }

    void Update()
    {  
        if(rectRef.localPosition.y >= 50)
        {

            rectRef.localPosition = Vector3.Lerp(rectRef.localPosition, transform.position - transform.position, 1.0f * Time.deltaTime);
        }
    }
}
