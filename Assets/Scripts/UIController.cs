using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RagdollCreatures;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour {
    bool isMoveLeft, isMoveRight, isJump;
    private void Awake()
    {
        if (UserData.IsNewGame)
        {
            UserData.IsNewGame = false;
            UserData.UpdateBooster(BoosterType.Booster01, 20);
            UserData.UpdateBooster(BoosterType.Booster02, 20);
            UserData.UpdateBooster(BoosterType.Booster03, 15);
        }
    }
    void Update()
    {
        Vector3 moveVector = GamesPlayController.Instance.player.GetComponent<RagdollCreatureController>().moveVector;
        if (isJump)
        {
            //Debug.Log("=============== isJump : " + isJump);
            GamesPlayController.Instance.player.GetComponent<RagdollCreatureController>().moveVector = new Vector3(moveVector.x, 1, 0);
            moveVector = GamesPlayController.Instance.player.GetComponent<RagdollCreatureController>().moveVector;
        }
        else
        {
            GamesPlayController.Instance.player.GetComponent<RagdollCreatureController>().moveVector = new Vector3(moveVector.x, 0, 0);
            moveVector = GamesPlayController.Instance.player.GetComponent<RagdollCreatureController>().moveVector;

        }
        if (isMoveLeft)
        {
            GamesPlayController.Instance.player.GetComponent<RagdollCreatureController>().moveVector = new Vector3(-1, moveVector.y, 0);
            moveVector = GamesPlayController.Instance.player.GetComponent<RagdollCreatureController>().moveVector;
        }
        else 
        if (isMoveRight)
        {
            GamesPlayController.Instance.player.GetComponent<RagdollCreatureController>().moveVector = new Vector3(1, moveVector.y, 0);
            moveVector = GamesPlayController.Instance.player.GetComponent<RagdollCreatureController>().moveVector;
        }
        else
            GamesPlayController.Instance.player.GetComponent<RagdollCreatureController>().moveVector = new Vector3(0, moveVector.y, 0);



    }
    public void btnMoveLeft_Onclick()
    {
        //Debug.Log("Move Left");
        isMoveLeft = true;
    }
    public void btnMoveRight_Onclick()
    {
        //Debug.Log("Move Right");
        isMoveRight = true;

    }
    public void btnJump_Onclick()
    {
        isJump = true;
    }
    public void btnUnMove_OnClick()
    {
        //Debug.Log("Btn up");
        isMoveLeft = isMoveRight = false;
        GamesPlayController.Instance.player.GetComponent<RagdollCreatureController>().moveVector = new Vector3(0, 0, 0);
    }
    public void btnJump_Up()
    {
        isJump = false;
    }
    public void btnShoot_Onclick()
    {
        if(GamesPlayController.Instance.player != null && GamesPlayController.Instance.player.hp > 0)
        {
            GamesPlayController.Instance.player.Shoot();
        }
    }
    public void btnGuns_Onclick()
    {
        
    }
    public void btnSkill01_Onclick()
    {

    }
    public void btnSkill02_Onclick()
    {

    }
    public void btnSkill03_Onclick()
    {

    }
    public void btnPause_Onclick()
    {
        GameCanvas.Instance.panelPause.SetActive(true);
    }

}

