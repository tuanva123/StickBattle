using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public static float Coin
    {
        set
        {
            PlayerPrefs.SetFloat("Coin", value);
        }
        get
        {
            return PlayerPrefs.GetFloat("Coin", 0);
        }
    }
    public static float Gem
    {
        set
        {
            PlayerPrefs.SetFloat("Gem", value);
        }
        get
        {
            return PlayerPrefs.GetFloat("Gem", 0);
        }
    }
    public static void UpdateBooster(BoosterType boost, int amountUpdate) // amountUpdate < 0: giam , >0: tang
    {
        string key = "BOOSTER_" + ((int)boost).ToString();
        int amount = PlayerPrefs.GetInt(key);
        amount += amountUpdate;
        if(amount <0) amount =0;
        PlayerPrefs.SetInt(key,amount );
    }
    public static int GetBooster(BoosterType boost)
    {
        string key = "BOOSTER_" + ((int)boost).ToString();
        int amountB = PlayerPrefs.GetInt(key);
        return amountB;
    }
    public static void UpdateAmountBullet(int idWeapon, int amountUpdate) // amountUpdate < 0: giam , >0: tang
    {
        string key = "AmountBulletInGun_" + idWeapon.ToString();
        int amount = PlayerPrefs.GetInt(key);
        amount += amountUpdate;
        if (amount < 0) amount = 0;
        PlayerPrefs.SetInt(key, amount);
    }
    public static int GetAmountBullet(int idWeapon)
    {
        string key = "AmountBulletInGun_" + idWeapon.ToString();
        int amountB = PlayerPrefs.GetInt(key);
        return amountB;
    }
    public static bool IsNewGame
    {
        set
        {
            PlayerPrefs.SetInt("IsNewGame", value == false ? 1 : 0);
        }
        get
        {
            return  PlayerPrefs.GetInt("IsNewGame",0) == 0 ? true : false;
        }
    }


}
