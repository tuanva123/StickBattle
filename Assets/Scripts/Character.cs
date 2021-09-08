using RagdollCreatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeCharacter { Player, Enemy, Boss}
public enum StateCharacter { Living , Dead }
public class Character: MonoBehaviour
{
    public TypeCharacter typeCharacter;
    public StateCharacter state;
    public int id;
    public float hp;
    public float hpMax;
    public float dame;
    public float speed;
    public float levelMax;
    public float dameBuff;
    public float timeDameBuff;

    public GameObject head;
    public GameObject body;
    
    public RagdollLimbIK ragdollLimbIK;
    public GameObject[] Weapons;
    public Gun curGun;
    public Vector2 targetRetime;
    public GameObject hpBar;
    [SerializeField] SpriteRenderer valueBarHp;
    public bool isDie;
    Vector2 defaultScaleHead;
    public virtual void Update()
    {
        hpBar.transform.eulerAngles = new Vector3(0, 0, 0);
        hpBar.transform.position = head.transform.position + deltaPosHpBar;
        CheckPosY();
    }
    void CheckPosY()
    {
        if(state == StateCharacter.Living)
        {
            Vector2 posLeftBelow = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            if (body.transform.position.y <= posLeftBelow.y - 3)
            {
                isDie = true;
                StartCoroutine(Die());
            }
        }
    }
    Vector3 deltaPosHpBar;
    public  virtual void Awake()
    {
        isDie = false;
        deltaPosHpBar = hpBar.transform.position - head.transform.position;
        defaultScaleHead = head.transform.localScale;
    }
    public void SetDir(float dirX)
    {
        if (dirX > 0)
        { 
            head.transform.localScale = new Vector3(defaultScaleHead.x, defaultScaleHead.y, 1f);
            body.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (dirX < 0)
        {
            head.transform.localScale = new Vector3(-defaultScaleHead.x, defaultScaleHead.y, 1f);
            body.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
    public void UpdateHp(float _dame)
    {
        hp -= _dame;
        if (hp < 0) hp = 0;
        UpdateHpBar();
        switch (typeCharacter)
        {
            case TypeCharacter.Player:
                SoundController.Instance.PlaySfx(SoundController.Instance.playerHitDame, 0.4f);
                break;
            case TypeCharacter.Enemy:
                SoundController.Instance.PlaySfx(SoundController.Instance.enemyHitDame, 0.4f);
                break;
            case TypeCharacter.Boss:
                SoundController.Instance.PlaySfx(SoundController.Instance.enemyHitDame, 0.4f);
                break;
        }
        if (hp <= 0 && state == StateCharacter.Living)
            StartCoroutine(Die());
    }
    public void UpdateHpBar()
    {
        valueBarHp.GetComponent<SpriteRenderer>().size = new Vector2((2.4778f * (float)hp) / (float)hpMax, 0.4f);
    }

    IEnumerator Die()
    {
        state = StateCharacter.Dead;
        switch (typeCharacter)
        {
            case TypeCharacter.Player:
                GamesPlayController.Instance.Lose();
                SoundController.Instance.PlaySfx(SoundController.Instance.playerDie);
                break;
            case TypeCharacter.Enemy:
                GamesPlayController.Instance.LisningEnemyDie(this.GetComponent<EnemyController>());
                SoundController.Instance.PlaySfx(SoundController.Instance.enemyDie);
                Destroy(this.gameObject);
                break;
            case TypeCharacter.Boss:
                SoundController.Instance.PlaySfx(SoundController.Instance.enemyDie);
                break;
        }
        yield break;
    }
    public void AddForce(Vector2 dir)
    {
    }
    public void Jump(float _jumpForce = 200)
    {
        GetComponent<RagdollCreatureController>().JumpAddForce(_jumpForce);
    }
    public void CheckDie()
    {
        if (hp <= 0)
            isDie = true;
    }
    void AnimArmRandom(Vector2 _posArm)
    {     
        
    }

    public virtual void OnDisable()
    {
        this.StopAllCoroutines();
    }
    public virtual void OnDestroy()
    {
        this.StopAllCoroutines();
    }
}
