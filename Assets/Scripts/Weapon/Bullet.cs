using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Bullet : MonoBehaviour
{
    public int idCharacter;
    public float dame = 1;
    public virtual void Update()
    {
        CheckDisable();
    }
    public void CheckDisable()
    {
        if (transform.position.x < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x || transform.position.y < Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y
            || transform.position.x > Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).x || transform.position.y > Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).y)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetBullet(float _dame, int _idCharacter)
    {
        this.dame = _dame;
        this.idCharacter = _idCharacter;
    }


    //public virtual void OnTriggerEnter2D(Collision2D collision)
    //{
    //    Debug.Log("collision: " + collision.gameObject.name);
    //    switch (collision.gameObject.tag)
    //    {
    //        case "Enemy":
    //            if(collision.gameObject.GetComponent<Part>() != null)
    //               if( collision.gameObject.GetComponent<Part>().character.id != idCharacter)
    //                {
                        
    //                    Debug.Log("collision.gameObject.GetComponent<Part>(): " + collision.gameObject.GetComponent<Part>().character.name);
    //                    collision.gameObject.GetComponent<Part>().SetHitDame(dame);
    //                    gameObject.SetActive(false);
    //                }
    //            break;
    //        case "Player":
    //            if (collision.gameObject.GetComponent<Part>() != null)
    //                if (collision.gameObject.GetComponent<Part>().character.id != idCharacter)
    //                {
                        
    //                    collision.gameObject.GetComponent<Part>().SetHitDame(dame);
    //                    gameObject.SetActive(false);
    //                }
    //            break;
    //        case "GroundNotThrough":
    //            Debug.Log("GroundNotThrough");

    //            gameObject.SetActive(false);
    //            break;
    //    }
        
    //}

}
