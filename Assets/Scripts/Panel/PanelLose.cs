using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelLose : MonoBehaviour
{
    [SerializeField] Transform popup;
    private void OnEnable()
    {
        Commons.ShowPouup(popup);
    }
    public void SetPanel()
    {

    }
    public void btnRestart_Onclick()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void btnHome_Onclick()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void btnLevelMode_Onclick()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void btnClose_Onclick()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
