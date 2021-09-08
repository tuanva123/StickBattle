using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float timeStartBeforeDistroy = 4;
    private void OnEnable()
    {
        timeStartBeforeDistroy = 4;
    }


    // Update is called once per frame
    void Update()
    {
        timeStartBeforeDistroy -= Time.deltaTime;
        if (timeStartBeforeDistroy <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
