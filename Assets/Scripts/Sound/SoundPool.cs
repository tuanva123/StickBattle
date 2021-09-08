using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPool : MonoBehaviour
{
    public static SoundPool Instance;
    //private readonly Stack<GameObject> instances = new Stack<GameObject>();
    List<int> _PooledKeyList = new List<int>();
    Dictionary<int, List<GameObject>> _PooledGoDic = new Dictionary<int, List<GameObject>>();

    public GameObject itemSound;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        CreateObj(10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateObj(int num = 5)
    {
        for(int i=0; i< num; i++)
        {
            GameObject obj = GetGameObject(itemSound, true);
            obj.SetActive(false);
        }
    }



    public void GetSfx(AudioClip clip, float volume)
    {
        GameObject sfxItem = GetGameObject(itemSound, false);
        sfxItem.GetComponent<ItemSound>().StartSoundItem(clip, volume);
    }

    GameObject GetGameObject(GameObject prefab,bool forceInstantiate = false)
    {
        if (prefab == null)
        {
            return null; 
        }
        int key = prefab.GetInstanceID();

        if (_PooledKeyList.Contains(key) == false && _PooledGoDic.ContainsKey(key) == false)
        {
            _PooledKeyList.Add(key);
            _PooledGoDic.Add(key, new List<GameObject>());
        }

        List<GameObject> goList = _PooledGoDic[key];
        //List<GameObject> goList = new List<GameObject>();
        GameObject go = null;

        if (forceInstantiate == false)
        {
            for (int i = goList.Count - 1; i >= 0; i--)
            {
                go = goList[i];
                if (go == null)
                {
                    goList.Remove(go);
                    continue;
                }
                if (go.activeSelf == false)
                {
                    // Found free GameObject in object pool.
                    Transform goTransform = go.transform;
                    go.SetActive(true);
                    return go;
                }
            }
        }

        // Instantiate because there is no free GameObject in object pool.
        go = (GameObject)Instantiate(prefab);
        go.transform.parent = transform;
        goList.Add(go);

        return go;
    }
}
