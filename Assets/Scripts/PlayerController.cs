using RagdollCreatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : Character
{
    public float angleGun;
    [SerializeField] Sprite[] arraySpriteHead;

    int curWeapon;
    [SerializeField] ParticleSystem particleSkill01, particleSkill02, particleSkill03;

    private void Start()
    {
        SetPlayer();
        curWeapon = 1;
        SetWeapon();
        //StartCoroutine(LoopShoot());
    }
    void SetPlayer()
    {
        hp = hpMax = 100;
        dame = 3;
        head.GetComponent<SpriteRenderer>().sprite = arraySpriteHead[2];
    }
    //public void SetSkill(SkillType _skill)
    //{
    //    switch (_skill)
    //    {
    //        case SkillType.Skill01:
    //            break;
    //        case SkillType.Skill02:
    //            break;
    //        case SkillType.Skill03:
    //            break;
    //    }
    //}

    public override void Update()
    {
        base.Update();
        ragdollLimbIK.transform.position = getIKTarget();
        if (timeDameBuff > 0)
        {
            timeDameBuff -= Time.deltaTime;
        }
        particleSkill01.gameObject.SetActive(timeDameBuff > 0);
    }

    void SetWeapon()
    { 
        foreach (GameObject w in Weapons)
        {
            w.SetActive(false);
        }
        Weapons[curWeapon].SetActive(true);
        switch (curWeapon)
        {
            case 0:
                curGun = null;
                break;
            case 1:
            case 2:
            case 3:
                curGun = Weapons[curWeapon].GetComponent<Gun>();
                break;
        }
    }

    public void Shoot()
    {
        if(curGun != null)
        {
            curGun.Shoot(ragdollLimbIK.curAngle, dame, id);
        }
    }
    //IEnumerator LoopShoot()
    //{
    //    yield return new WaitForSeconds(2f);
    //    while (true)
    //    {
    //        //yield return new WaitForSeconds(0.5f);
    //        curGun.Shoot(ragdollLimbIK.curAngle, dame, id);
    //        yield return new WaitForSeconds(2f);
    //    }
    //}

    Vector2 getIKTarget() 
    {
        Vector2 _target = Vector2.zero;
        EnemyController e = getEnemyNear();
        if (e != null)
            _target = e.body.transform.position;
        return _target;
    }
    EnemyController getEnemyNear() // enemy gần nhất 
    {
        EnemyController eNear = null;
        float dis = 0 ;
        foreach(EnemyController e in GamesPlayController.Instance.listEnemyInLevel)
        {
            float disE = Vector2.Distance(body.transform.position, e.body.transform.position);
            if(disE < dis || dis ==0)
            {
                dis = disE;
                eNear = e;
            }
        }
        return eNear;
    }
    public void BuffDame(float _dameBuff, float _timeBuff )
    {
        this.dameBuff = _dameBuff;
        this.timeDameBuff = _timeBuff;
    }
    public void BuffHp(float _hpBuff)
    {
        hp += _hpBuff;
        if (hp > hpMax) hp = hpMax;
        UpdateHpBar();
        GameObject effectBuffHp = GamePool.Instance.GetGameObject(GamePool.Instance.effectBuffHp, body.transform.position , Quaternion.identity);
        effectBuffHp.transform.eulerAngles = new Vector3(-90, 0, 0);
        effectBuffHp.GetComponent<ParticleSystem>().Play();
        //particleSkill02.gameObject.SetActive(true);
       // particleSkill02.Play();
    }
}
