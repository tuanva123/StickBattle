using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("collision: " + collision.gameObject.name);
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                if (collision.gameObject.GetComponent<Part>() != null)
                    if (collision.gameObject.GetComponent<Part>().character.id != idCharacter)
                    {
                        if (idCharacter != 0) // not player
                            dame = dame / 2;
                        //Debug.Log("collision.gameObject.GetComponent<Part>(): " + collision.gameObject.GetComponent<Part>().character.name);
                        collision.gameObject.GetComponent<Part>().SetHitDame(dame);
                        GameObject effectBlood = GamePool.Instance.GetGameObject(GamePool.Instance.arrBloodCharacter[collision.gameObject.GetComponent<Part>().character.id], transform.position, Quaternion.identity);
                        gameObject.SetActive(false);
                    }
                break;
            case "Player":
                if (collision.gameObject.GetComponent<Part>() != null)
                    if (collision.gameObject.GetComponent<Part>().character.id != idCharacter)
                    {
                        collision.gameObject.GetComponent<Part>().SetHitDame(dame);
                        GameObject effectBlood = GamePool.Instance.GetGameObject(GamePool.Instance.arrBloodCharacter[collision.gameObject.GetComponent<Part>().character.id], transform.position, Quaternion.identity);
                        gameObject.SetActive(false);
                    }
                break;
            case "GroundNotThrough":
                //Debug.Log("GroundNotThrough");

                gameObject.SetActive(false);
                break;
            case "TNT":
                //Debug.Log("GroundNotThrough");
                GameObject effectBoom= GamePool.Instance.GetGameObject(GamePool.Instance.effectBoom, collision.transform.position, Quaternion.identity);
                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);
                SoundController.Instance.PlaySfx(SoundController.Instance.sfxBoom, 0.5f);
                break;
        }
    }
    void CheckDisableBullet()
    {

    }

    private void ResetBullet()
    {
        
    }
}
