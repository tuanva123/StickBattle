using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelPause : MonoBehaviour
{
    [SerializeField] Transform popup;

    public void SetPanel()
    {

    }
    private void OnEnable()
    {
        GamesPlayController.Instance.stateGamePlay = StateGamePlay.Pause;
        Commons.ShowPouup(popup, ()=> {
            Time.timeScale = 0;
        });
        
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
        Time.timeScale = 1;
        Commons.HidePopup(popup, gameObject, () =>
        {
            
            gameObject.SetActive(false);
            GamesPlayController.Instance.stateGamePlay = StateGamePlay.Playing;
        });
        

    }
}
