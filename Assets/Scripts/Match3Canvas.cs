using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match3Canvas : MonoBehaviour
{
    public GameObject Cellref;
    void Start()
    {

        Vector3 tempPos = transform.position;

        for (var i = 0; i < 5; i++)
        {
            for (var j = 0; j < 5; j++)
            {
                tempPos.y = i * 120 - transform.localScale.y * 160;
                tempPos.x = j * 120 - transform.localScale.x * 160;
                GameObject newCell = Instantiate(Cellref, tempPos, transform.rotation);
                newCell.transform.SetParent(transform, false);
                
            }
        }
    }

    void Update()
    {
        
    }
}
