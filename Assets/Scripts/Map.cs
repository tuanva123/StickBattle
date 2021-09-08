using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform parentPosRandom;
    
    public List<Vector2> getListPosRandom()
    {
        List<Vector2> listPos = new List<Vector2>();
        foreach(Transform t in parentPosRandom)
        {
            listPos.Add(t.position);
           // Debug.Log(t.name);
        }
        return listPos;
    }

}


