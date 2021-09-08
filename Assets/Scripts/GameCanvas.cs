using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    public GameObject panelWin;
    public GameObject panelLose;
    public GameObject panelPause;
    public static GameCanvas Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
