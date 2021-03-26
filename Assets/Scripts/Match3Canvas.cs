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
                

                // set cell image
                newCell.transform.Find("Icon").GetComponent<Image>().sprite = imageArray[randID];
                
                // set cell ID
                newCell.GetComponent<CellScript>().CellID = randID;
            


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
            return;
        }
        else if(rectRef.localPosition.y >= 50)
        {
            rectRef.localPosition = Vector3.Lerp(rectRef.localPosition, transform.position - transform.position, 1.0f * Time.deltaTime);
        }
        else
        {
            activated = true;
            foreach (var item in BoardArray)
            {
                item.GetComponent<CellScript>().activated = true;
                item.GetComponent<CellScript>().GetNeighbors();
            }
        }



    }

    void AddNewCell()
    {

    }
    
    public void UpdateBoardArray()
    {
        BoardArray.Clear();

        var temp = GameObject.FindGameObjectsWithTag("cellparent");
        
        int i = 1;
        string tempStr = "";
        foreach (var item in temp)
        {
            BoardArray.Add(item);
            // print(item.transform.Find("Icon").GetComponent<Image>().sprite.name);

            tempStr += item.transform.Find("Icon").GetComponent<Image>().sprite.name + ", ";

            if (i % 5 == 0)
            {
                print(tempStr);
                tempStr = "";
            }

            i++;
        }
        

        
        

    }
}
