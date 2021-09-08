using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypePartCharacter { Default, Body, Head, Arm}
public class Part : MonoBehaviour
{
    public Character character;
    [SerializeField] TypePartCharacter typePart;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCollisionBody();
    }
    void UpdateCollisionBody()
    {
        if (typePart != TypePartCharacter.Body || character.typeCharacter == TypeCharacter.Player) return;

        if (timeCollisionGroundBox > 0)
        {
            timeCollisionGroundBox -= Time.deltaTime;
        }

        if (timeCollisionGroundBox <= 0 && isCollisionGroundBox)
        {

            countJumpCollisionGroundBox += 1;
            if (countJumpCollisionGroundBox >= 2)
            {
                character.Jump(300);
            }
            else
                character.Jump();
            timeCollisionGroundBox = 0.5f;

        }
    }

    public void SetHitDame(float _dame)
    {
        character.UpdateHp(_dame);
    }
    float timeCollisionGroundBox;
    bool isCollisionGroundBox;
    int countJumpCollisionGroundBox;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( typePart == TypePartCharacter.Body && collision.gameObject.name.Contains("groundBox") )
        {
            isCollisionGroundBox = true;
            timeCollisionGroundBox = 0.5f;
            countJumpCollisionGroundBox = 0;
           // Debug.Log("groundBox");
        }
    }

  
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (typePart == TypePartCharacter.Body && collision.gameObject.name.Contains("groundBox") )
        {
            isCollisionGroundBox = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (typePart == TypePartCharacter.Body && collision.gameObject.name.Contains("groundBox"))
        {
            isCollisionGroundBox = false;
            countJumpCollisionGroundBox = 0;
        }
    }
}
