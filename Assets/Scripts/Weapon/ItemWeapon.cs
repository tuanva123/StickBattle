using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemWeapon : MonoBehaviour
{
    public int idWeapon;
    public int amountBullet;
    [SerializeField] Sprite[] spriteItems;

    private void Update()
    {
        Vector2 posLeftBelow = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        if (transform.position.y <= posLeftBelow.y - 3)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetItem(int _id, int _amountBullet)
    {
        idWeapon = _id;
        amountBullet = _amountBullet;
        GetComponent<SpriteRenderer>().sprite = spriteItems[idWeapon];
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = S;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "GroundNotThrough")
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        if(collision.tag == "Player" || collision.tag == "Enemy")
        {
            if (GamesPlayController.Instance.listWeaponSpawnInMap.Contains(this.gameObject))
                GamesPlayController.Instance.listWeaponSpawnInMap.Remove(this.gameObject);
            gameObject.SetActive(false);
        }
        
    }
}
