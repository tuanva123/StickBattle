using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateGamePlay { Starting, Playing , Win, Lose , Pause }
public class GamesPlayController : MonoBehaviour
{
    [SerializeField] Map map;
    public PlayerController player;
    public List<EnemyController> listEnemyInLevel;
    public static GamesPlayController Instance;
    public List<Vector2> listPosRandomEnemy;
    public StateGamePlay stateGamePlay;
    
    void SetLevel()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
       // Debug.Log("GameObject.FindObjectsOfType<EnemyController>().Length: " + GameObject.FindObjectsOfType<EnemyController>().Length);
        for (int i=0; i< GameObject.FindObjectsOfType<EnemyController>().Length; i++)
        {
            listEnemyInLevel.Add(GameObject.FindObjectsOfType<EnemyController>()[i].GetComponent<EnemyController>());
            GameObject.FindObjectsOfType<EnemyController>()[i].GetComponent<EnemyController>().StartEnemy();
        }
    }
    IEnumerator GameStart()
    {
        if(GameObject.FindObjectOfType<Map>()== null)
        {
            GameObject objMap = Instantiate(Commons.GetMap(UnityEngine.Random.Range(1,5)));
            map = objMap.GetComponent<Map>();
        }
        else
        {
            map = GameObject.FindObjectOfType<Map>();
        }
        while (map == null)
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }
        listPosRandomEnemy = map.getListPosRandom();
        SetLevel();
        stateGamePlay = StateGamePlay.Playing;
        StartCoroutine(SetSpawnWeapon());
        while(stateGamePlay == StateGamePlay.Playing)
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield break;
    }
    void Start()
    {
        Time.timeScale = 1;
        Instance = this;
        StartCoroutine(GameStart());
    }

    public void Win() {
        if (stateGamePlay != StateGamePlay.Win && stateGamePlay != StateGamePlay.Lose)
            stateGamePlay = StateGamePlay.Win;
        GameCanvas.Instance.panelWin.SetActive(true);
    }
    public void Lose()
    {
        if (stateGamePlay != StateGamePlay.Win && stateGamePlay != StateGamePlay.Lose)
            stateGamePlay = StateGamePlay.Lose;
        GameCanvas.Instance.panelLose.SetActive(true);
    }
    public void CheckWin()
    {
        if( listEnemyInLevel.Count == 0)
        {
            Win();
        }
    }
    public void LisningEnemyDie(EnemyController e)
    {
        if (listEnemyInLevel.Contains(e))
        {
            listEnemyInLevel.Remove(e);
        }
        CheckWin();
    }

    public List<GameObject> listWeaponSpawnInMap;

    IEnumerator SetSpawnWeapon()
    {
        listWeaponSpawnInMap = new List<GameObject>();
        yield return new WaitForSeconds(1);
        while (true)
        {
            
            SpawnWeapon();
            yield return new WaitForSeconds(UnityEngine.Random.Range(3,5f));
        }
    }
    void SpawnWeapon()
    {
        if (listWeaponSpawnInMap.Count < 3 && stateGamePlay == StateGamePlay.Playing)
        {
            Vector2 posRandomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            Vector2 posRandomRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
            Vector2 pos = new Vector2(UnityEngine.Random.Range(posRandomLeft.x, posRandomRight.x), posRandomRight.y);
            GameObject weapon = GamePool.Instance.GetGameObject(GamePool.Instance.itemWeapon, pos, Quaternion.identity);
            weapon.GetComponent<ItemWeapon>().SetItem(UnityEngine.Random.Range(0, 4), UnityEngine.Random.Range(10, 30));
            listWeaponSpawnInMap.Add(weapon);
        }
    }
    void Pause()
    {

    }
    void Continue()
    {

    }
    void ResetLevel()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
