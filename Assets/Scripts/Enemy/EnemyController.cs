using RagdollCreatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Character
{
    public int idEnemy;
   // [SerializeField] RagdollLimbIK ragdollLimbIK;
    public float angleGun;
    int curWeapon;
    Vector2 targetMove;
   // [SerializeField] GameObject body;

    private void Start()
    {
       
        
    }
    public override void Update()
    {
        base.Update();
        ragdollLimbIK.transform.position = getIKTarget();
    }
    Vector2 getIKTarget()
    {
        Vector2 _target = Vector2.zero;
        Character e = getCharacterNear();
        if (e != null)
            _target = e.body.transform.position;
        return _target;
    }
    Character getCharacterNear() // character gần nhất 
    {
        Character eNear = null;
        float dis = 0;
        if (GamesPlayController.Instance.player != null && GamesPlayController.Instance.player.hp > 0)
        {
            dis = Vector2.Distance(body.transform.position, GamesPlayController.Instance.player.body.transform.position);
            eNear = GamesPlayController.Instance.player;
        }
        foreach (Character e in GamesPlayController.Instance.listEnemyInLevel)
        {
            float disE = Vector2.Distance(body.transform.position, e.body.transform.position);
            if ((disE < dis || dis == 0 )&& e != this)
            {
                dis = disE;
                eNear = e;
            }
        }
        return eNear;
    }

    public void StartEnemy()
    {
        hp = hpMax = 100;
        dame = 3;
        //Debug.Log("Enemy_ name: " + gameObject.name);
        curWeapon = 3;
        StartCoroutine(AI_Enemy());
        MoveTarget();
        SetWeapon();
    }

    Vector2 getTargetMove()
    {
        Vector2 _target = GamesPlayController.Instance.listPosRandomEnemy[UnityEngine.Random.Range(0, GamesPlayController.Instance.listPosRandomEnemy.Count)];
        _target = new Vector2(Random.Range(_target.x - 0.5f, _target.x + 0.5f), _target.y);
        if (_target == null) _target = Vector2.zero;
        return _target;
    }

    void SetWeapon()
    {
        foreach(GameObject w in Weapons)
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

    public override void OnDestroy()
    {
        base.OnDestroy();
        this.StopAllCoroutines();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        this.StopAllCoroutines();
    }

    void Shoot()
    {

    }
    //IEnumerator LoopShoot()
    //{
    //    yield return new WaitForSeconds(2f);
    //    while (true)
    //    {
    //        curGun.Shoot(ragdollLimbIK.curAngle);
    //        yield return new WaitForSeconds(2f);
    //    }
    //}

    IEnumerator AI_Enemy()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            curGun.Shoot(ragdollLimbIK.curAngle, dame, id);
            yield return new WaitForSeconds(2f);
        }
    }


    bool isTargetShotPlayer() // check xem có bắn chúng player hay ko
    {
        bool isTarget = false;

        return isTarget;
    }

    //IEnumerator MoveTarget()
    //{
    //    while (true)
    //    {
    //        targetMove = getTargetMove();
    //        StartCoroutine(MoveToTarget(targetMove));
    //        yield return new WaitForSeconds(4f);
            
    //    }
    //}
    void MoveTarget()
    {
        targetMove = getTargetMove();
        StartCoroutine(MoveToTarget(targetMove));
    }
    bool isMove;
    IEnumerator MoveToTarget(Vector2 _target)
    {
        isMove = true;
        Vector2 _curPos = body.transform.position;
        bool _isMoveLeft = true;
        float _timeMove = 0;
        int countJump = 0;
        if(_curPos.x > _target.x)
        {
            _isMoveLeft = true;
            GetComponent<RagdollCreatureController>().moveVector = new Vector3(-1, 0, 0);
        }
        else
        {
            _isMoveLeft = false;
            GetComponent<RagdollCreatureController>().moveVector = new Vector3(1, 0, 0);
        }
        while (_isMoveLeft?(body.transform.position.x > _target.x): (body.transform.position.x < _target.x) && _timeMove < 3f)
        {
            if(countJump ==0 && _timeMove > 0.5f)
            {
                countJump += 1;
                int isRand = UnityEngine.Random.Range(0, 100);
                if(isRand > 50)
                {
                    Jump();
                }
            }
            _timeMove += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        GetComponent<RagdollCreatureController>().moveVector = new Vector3(0, 0, 0);
        isMove = false;
        int randomIdle = UnityEngine.Random.Range(0, 100);
        if(randomIdle <= 20)
        {
            StartCoroutine(IdleTarget( UnityEngine.Random.Range(0.5f, 1f)));
        }else
            MoveTarget();
        yield break;
    }
    IEnumerator IdleTarget(float _timeIdle)
    {
        yield return new WaitForSeconds(Time.deltaTime);
        float deltaTime = 0;
        int countJump = 0;
        while (deltaTime < _timeIdle)
        {
            deltaTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
            if (countJump == 0 && deltaTime > UnityEngine.Random.Range(0f, _timeIdle))
            {
                countJump += 1;
                int isRand = UnityEngine.Random.Range(0, 100);
                if (isRand > 50)
                {
                    Jump();
                }
            }
        }
        MoveTarget();
        yield break;
    }
    

    
    void Move(Vector2 target)
    {
        GetComponent<RagdollCreatureController>().moveVector = new Vector3(-1, 0, 0);
    }
    void RandomWeapon()
    {
        
    }
    void Die()
    {

    }

    
}
