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

    // difficulty
    public Difficulties diff;


    // array for storing cell images
    public Sprite[] imageArray;

    // array for all the cell object currently on board
    public List<GameObject> BoardArray;


    // array for sounds
    public AudioSource[] SoundArray;



    // col and row for board
    public int col;
    public int row;



    // references
    public GameObject Cellref;
    RectTransform rectRef;


    // cell rect size
    float CellSize = 100;
    
    // activate board if it stopped moving
    public bool activated;
    
    // random id for cell
    int randID;

    // prevent repeating tiles
    int prevID;
    int randomRange = 0;
    int randomRangeEasy = 4;
    int randomRangeMedium = 5;
    int randomRangeHard = 6;


    void Start()
    {
        // board will be activated after stop moving 
        activated = false;
        // get rect transform of panel
        rectRef = GetComponent<RectTransform>();
        // temp position for cell
        Vector3 tempPos = transform.position;

        // generate & spawn cell
        for (var i = 0; i < col; i++)
        {
            for (var j = 0; j < row; j++)
            {

                // set new cell position
                tempPos.x = j * CellSize - CellSize * row / 2.5f;
                tempPos.y = i * CellSize - CellSize * col / 2.5f;
                tempPos.z = 0;

                // spawn cell
                GameObject newCell = Instantiate(Cellref, tempPos, transform.rotation);
                
                // attach cell to the panel
                newCell.transform.SetParent(transform, false);
                


                // set how many different sprites according to difficulty
                switch (diff)
                {
                    case Difficulties.EASY:
                        randomRange = randomRangeEasy;
                        break;
                    case Difficulties.MEDIUM:
                        randomRange = randomRangeMedium;
                        break;
                    case Difficulties.HARD:
                        randomRange = randomRangeHard;
                        break;
                    default:
                        break;
                }
                

                // set id to random range
                randID = Random.Range(0, randomRange);

                // if random range is set
                if (randomRange != 0)
                {
                    // while same id generated, regenerate it
                    while (prevID == randID)
                    {
                        randID = Random.Range(0, randomRange);
                    }
                }
                
                // set previous id to generated id
                prevID = randID;




                // set cell image
                newCell.transform.Find("Icon").GetComponent<Image>().sprite = imageArray[randID];
                
                // set cell ID
                newCell.GetComponent<CellScript>().CellID = randID + 1;
            

                // add cell to board array
                BoardArray.Add(newCell);
            

            }
        }


        // push the panel to the lerp start position
        rectRef.localPosition = new Vector3(0,800,0);
    }

    void Update()
    {  

        
        if (activated)
        {
            if (Time.frameCount % 60 == 0)
            {
                FindMatchForAllCells();
            }
            return;
        }
        else if(rectRef.localPosition.y >= 50)
        {
            rectRef.localPosition = Vector3.Lerp(rectRef.localPosition, transform.position - transform.position, 1.0f * Time.deltaTime);
        }
        else
        {
            activated = true;

            // set all cell to active
            foreach (var item in BoardArray)
            {
                if (item != null)
                {
                    item.GetComponent<CellScript>().activated = true;

                }
                
            }
        }



    }


    public void FindMatchForAllCells()
    {


        foreach (var item in BoardArray)
        {
            if (item != null)
            {
                item.GetComponent<CellScript>().FindMatchAllDirection();
            }
        }
        

        BoardArray.Clear();

        var temp = GameObject.FindGameObjectsWithTag("cellparent");
        foreach (var item in temp)
        {
            BoardArray.Add(item);
        }
    }  



    public void PlayMoveSound()
    {
        SoundArray[0].Play();
        
    }

    public void PlayMatchSound()
    {
        SoundArray[1].Play();

    }
}

