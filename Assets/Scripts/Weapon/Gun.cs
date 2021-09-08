using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] int indexGun; 
    [SerializeField] UbhShotCtrl ubhShotCtrl;

    private void OnDisable()
    {
        this.StopAllCoroutines();
        ubhShotCtrl.StopShotRoutine();
        ubhShotCtrl.StopAllCoroutines();
    }

    public void Shoot(float _angle, float _dame, int _idChar)
    {
        switch (indexGun)
        {
            case 1:
                ubhShotCtrl.m_shotList[0].m_shotObj.gameObject.GetComponent<UbhLinearShot>().m_angle = _angle + 180;
                ubhShotCtrl.m_shotList[0].m_shotObj.gameObject.GetComponent<UbhLinearShot>().SetShoot(_dame, _idChar);
                SoundController.Instance.PlaySfx(SoundController.Instance.shoot1, 0.5f);
                break;
            case 2:
                ubhShotCtrl.m_shotList[0].m_shotObj.gameObject.GetComponent<UbhLinearShot>().m_angle = _angle + 180;
                ubhShotCtrl.m_shotList[0].m_shotObj.gameObject.GetComponent<UbhLinearShot>().SetShoot(_dame, _idChar);
                SoundController.Instance.PlaySfx(SoundController.Instance.shoot2, 0.5f);

                break;
            case 3:
                ubhShotCtrl.m_shotList[0].m_shotObj.gameObject.GetComponent<UbhNwayShot>().m_centerAngle = _angle + 180;
                ubhShotCtrl.m_shotList[0].m_shotObj.gameObject.GetComponent<UbhNwayShot>().SetShoot(_dame, _idChar);
                SoundController.Instance.PlaySfx(SoundController.Instance.shoot3, 0.5f);

                break;

        }
        //if(indexGun == 1)
        //{
        //    Debug.Log("ubhShotCtrl.m_shotList[0].m_shotObj.gameObject.GetComponent<UbhLinearShot>().m_angle: " + ubhShotCtrl.m_shotList[0].m_shotObj.gameObject.GetComponent<UbhLinearShot>().m_angle);

        //}
       // ubhShotCtrl.m_shotList[0].m_shotObj = _angle;
        ubhShotCtrl.StartShotRoutine();
    }
}
