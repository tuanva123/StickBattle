using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BoosterType { Booster01, Booster02, Booster03 }
public class ItemBooster : MonoBehaviour
{
    [SerializeField] BoosterType boosterType;
    [SerializeField] Image imageValueTimeDelay;
    [SerializeField] float timeDelay;
    [SerializeField] float hpBuff;
    [SerializeField] float dameBuff;
    [SerializeField] Text textAmount;

    float curTimeDelay;
    private void Start()
    {
        if (timeDelay <= 0) timeDelay = 1;
        UpdateItem();
    }

    void UpdateItem()
    {
        textAmount.text = UserData.GetBooster(boosterType).ToString();
    }
    public void OnClick()
    {
        if (curTimeDelay > 0 || UserData.GetBooster(boosterType) <= 0) return;
        curTimeDelay = timeDelay;
        switch (boosterType)
        {
            case BoosterType.Booster01:
                UserData.UpdateBooster(BoosterType.Booster01, -1);
                GamesPlayController.Instance.player.BuffDame(dameBuff, timeDelay);
                break;
            case BoosterType.Booster02:
                UserData.UpdateBooster(BoosterType.Booster02, -1);
                GamesPlayController.Instance.player.BuffHp(hpBuff);
                break;
            case BoosterType.Booster03:
                UserData.UpdateBooster(BoosterType.Booster03, -1);
                break;
        }
        UpdateItem();
    }

    private void Update()
    {
        if(curTimeDelay >= 0)
        {
            curTimeDelay -= Time.deltaTime;
            if (curTimeDelay < 0) curTimeDelay = 0;
            imageValueTimeDelay.fillAmount = curTimeDelay / timeDelay;
        }
    }
}
