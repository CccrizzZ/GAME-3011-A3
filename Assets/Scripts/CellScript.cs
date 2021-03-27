using System;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;


public class CellScript : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{


    // references
    public Canvas canvasRef;
    RectTransform rectTransform;
    GameObject IconRef;
    Match3Canvas M3CRef;
    
    public bool activated;


    // variables
    Vector3 BeginDragPosition;
    public int CellID;




    // canvas raycast system
    EventSystem Esystem;
    GraphicRaycaster GRaycaster;

    void Start()
    {
        activated = false;
        


        // get references
        IconRef = transform.Find("Icon").gameObject;
        rectTransform = GetComponent<RectTransform>();
        canvasRef = GameObject.FindGameObjectWithTag("Match3Canvas").GetComponent<Canvas>();
        Esystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        GRaycaster = GetComponent<GraphicRaycaster>();
        M3CRef = transform.parent.GetComponent<Match3Canvas>();


    }

    void Update()
    {
  
    }




    public void OnPointerDown(PointerEventData eventData)
    {
        // RunCanvasRaycast();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (activated)
        {
            SetIconDisable();
            BeginDragPosition = rectTransform.anchoredPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (activated)
        {
            // rectTransform.anchoredPosition += eventData.delta / canvasRef.scaleFactor;
            
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (activated)
        {
            SetIconAvaliable();
            RunCanvasRaycast();
            
            
        }
        

    }


    // raycast method for pointer
    void RunCanvasRaycast()
    {
        // create pointer event data
        PointerEventData pdata = new PointerEventData(Esystem);
        pdata.position = Input.mousePosition;

        // create list for all hit results
        List<RaycastResult> results = new List<RaycastResult>();
        
        // GRaycaster.Raycast(pdata, results);
        Esystem.RaycastAll(pdata, results);

        if (results.Count > 0)
        {
            foreach (RaycastResult result in results)
            {
                // if hit result have cell tag and is not self
                if (result.gameObject.CompareTag("cell") && result.gameObject.transform.position != gameObject.transform.position)
                {
                    
                    // print(result.gameObject.GetComponent<Image>().sprite);
                    
                    
                    // get hit cell container
                    var targetcellparent = result.gameObject.transform.parent.gameObject;
                    
                    // if the icons are different, swap it
                    if (targetcellparent.GetComponent<CellScript>().CellID != CellID)
                    {
                        
                        // get distance between this cell and target cell
                        var offset = targetcellparent.GetComponent<RectTransform>().anchoredPosition - rectTransform.anchoredPosition;

                        // print(Math.Abs(offset.x) + ", " + Math.Abs(offset.y));

                        // if the tiles are neighbors and arent diagnal
                        if ((Math.Abs(offset.x) == 100 && Math.Abs(offset.y) == 0) || (Math.Abs(offset.x) == 0 && Math.Abs(offset.y) == 100))
                        {
                            
                            // swap position of this cell and hit cell
                            SwapPosition(gameObject, targetcellparent);
                            
                            // play sfx
                            M3CRef.PlayMoveSound();
                        }


                    }
                }
            }
        }
        else
        {
            print("hitnone");
        }


    }



    // swap 2 game object position
    void SwapPosition(GameObject A, GameObject B)
    {
        var tempPos = A.GetComponent<RectTransform>().anchoredPosition;
        A.GetComponent<RectTransform>().anchoredPosition = B.GetComponent<RectTransform>().anchoredPosition;
        B.GetComponent<RectTransform>().anchoredPosition = tempPos;

    }


    // find match up,down,left,right
    public void FindMatchAllDirection()
    {
        // FindMatchOneWay(new Vector2(0,-50));
        FindMatchOneWay(new Vector2(0,50));
        FindMatchOneWay(new Vector2(50,0));
        // FindMatchOneWay(new Vector2(-50,0));


    }




    bool FindMatchOneWay(Vector2 dir)
    {

        // create hit list
        List<GameObject> matchList1 = new List<GameObject>();


        // create raycast towards direction in parameter
        RaycastHit2D[] hit1 = Physics2D.RaycastAll(transform.position, dir);


        // draw ray
        Debug.DrawRay(transform.position, dir, Color.red, 1.0f);

        
        // string temp = "";
        // get all hit items positive direction
        foreach (var item in hit1)
        {
            // get raycast item id
            var hitID = item.collider.GetComponent<CellScript>().CellID;
            

            // if raycast id matchs this cell's id
            if (hitID == CellID)
            {
                matchList1.Add(item.collider.gameObject);
            }
            else
            {
                break;
            }
            
        }

        



        // if more than 2 matches
        if (matchList1.Count > 2)
        {
            // print(matchList1.Count);
            


            // detemine if all hit tiles are neighbors
            Vector2 temp = rectTransform.anchoredPosition;
            bool isNeighbor = false;
            foreach (var item in matchList1)
            {
                
                var offset = item.GetComponent<RectTransform>().anchoredPosition - temp;

                // if previous tile is near
                var isnear = (Math.Abs(offset.x) == 100 && Math.Abs(offset.y) == 0) || (Math.Abs(offset.x) == 0 && Math.Abs(offset.y) == 100);
                
                if (isnear)
                {
                    isNeighbor = true;
                    temp = item.GetComponent<RectTransform>().anchoredPosition;
                }
                else
                {
                    isNeighbor = false;
                }
            }


            // deletion of tiles
            foreach (var item in matchList1)
            {
                // if the hit tiles are all neighbors
                if (isNeighbor)
                {
                    
                    // destroy tiles
                    Destroy(item);

                    // play sound effect
                    M3CRef.PlayMatchSound();
                }
            }
        }

        return true;
    }


    // Set icon alpha
    void SetIconAvaliable()
    {
        GetComponent<Image>().color = new Color(
            GetComponent<Image>().color.r,
            GetComponent<Image>().color.g,
            GetComponent<Image>().color.b,
            1.0f
        );
    }

    void SetIconDisable()
    {
        GetComponent<Image>().color = new Color(
            GetComponent<Image>().color.r,
            GetComponent<Image>().color.g,
            GetComponent<Image>().color.b,
            0.5f
        );
    }



}
