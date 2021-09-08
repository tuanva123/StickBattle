using System.Collections;
using System.Collections.Generic;
using RagdollCreatures;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    enum ButtonType { MoveLeft,MoveRight}
    [SerializeField] ButtonType buttonType;
    // Start is called before the first frame update
    
    private void OnMouseDown()
    {
        switch (buttonType)
        {
            case ButtonType.MoveLeft:
                Debug.Log("MoveLeft_Btn");
                GamesPlayController.Instance.player.GetComponent<RagdollCreatureController>().moveVector = new Vector3(-1, 0, 0);
                break;
            case ButtonType.MoveRight:
                Debug.Log("MoveRight_Btn");
                GamesPlayController.Instance.player.GetComponent<RagdollCreatureController>().moveVector = new Vector3(1, 0, 0);
                break;
        }
    }
    private void OnMouseUp()
    {
        Debug.Log("Close_Btn");

        switch (buttonType)
        {
            case ButtonType.MoveLeft:
                GamesPlayController.Instance.player.GetComponent<RagdollCreatureController>().moveVector = new Vector3(0, 0, 0);
                break;
            case ButtonType.MoveRight:
                GamesPlayController.Instance.player.GetComponent<RagdollCreatureController>().moveVector = new Vector3(0, 0, 0);
                break;
        }
    }
    private void OnMouseExit()
    {
    }
    private void Update()
    {
        //if(IsInside(gameObject.GetComponent<CircleCollider2D>(),Camera.main.ScreenToWorldPoint(Input.mousePosition))){
        //    switch (buttonType)
        //    {
        //        case ButtonType.MoveLeft:
        //            Debug.Log("MoveLeft_Btn");
        //            GamesPlayController.Instance.player.GetComponent<RagdollCreatureController>().moveVector = new Vector3(-1, 0, 0);
        //            break;
        //        case ButtonType.MoveRight:
        //            Debug.Log("MoveRight_Btn");
        //            GamesPlayController.Instance.player.GetComponent<RagdollCreatureController>().moveVector = new Vector3(1, 0, 0);
        //            break;
        //    }
        //}
    }
    bool IsInside(Collider2D c, Vector3 point)  // Check xem điểm point có nằm trong collider hay không
    {
        bool closest = c.OverlapPoint(point);
        // Because closest=point if point is inside - not clear from docs I feel
        return closest;
    }
}
