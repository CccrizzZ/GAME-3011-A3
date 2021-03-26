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

    public List<GameObject> XNeighbors;
    public List<GameObject> YNeighbors;




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
                    
                    var offset = targetcellparent.GetComponent<RectTransform>().anchoredPosition - rectTransform.anchoredPosition;

                    // if the tiles are neighbors and arent diagnal
                    if (Math.Abs(offset.x) <= 50 || Math.Abs(offset.y) <= 50)
                    {
                        // swap position of this cell and hit cell
                        SwapPosition(gameObject, targetcellparent);
                        
                        M3CRef.UpdateBoardArray();

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



    public void GetNeighbors()
    {
        foreach (var item in M3CRef.BoardArray)
        {
            var offset = item.GetComponent<RectTransform>().anchoredPosition - rectTransform.anchoredPosition;

            // get x neighbor
            if (Math.Abs(offset.x) <= 50 || Math.Abs(offset.y) <= 50)
            {
                XNeighbors.Add(item);
            }

            // get y neighbor
            if (Math.Abs(offset.y) <= 50)
            {
                // YNeighbors.Add(item);
            }


        }
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
