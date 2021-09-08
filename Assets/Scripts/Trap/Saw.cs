using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] GameObject sawBlade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sawBlade.transform.Rotate(-Vector3.forward *Time.deltaTime * 150);
    }
}
