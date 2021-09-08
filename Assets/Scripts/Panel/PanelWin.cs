using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class PanelWin : MonoBehaviour
{
    [SerializeField] Transform popup;
    [SerializeField] Image imageHead;
    [SerializeField] TextMeshProUGUI textMoney;
    private void OnEnable()
    {
        GetComponent<Animation>().Play("WinShow");
        //Commons.ShowPouup(popup);
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
