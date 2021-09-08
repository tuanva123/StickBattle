using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Net;
using System.Linq;
using DG.Tweening;

public class Commons : MonoBehaviour
{
    public static string LoadResourceTextfile(string path)
    {
        string filePath = "Datas/" + path.Replace(".json", "");
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);

        return targetFile.text;
    }
    public static GameObject GetMap(int idMap)
    {
        return Resources.Load<GameObject>("Maps/Map"+ idMap.ToString());
    }

    public static void ShowPouup(Transform panel, UnityAction actionComplete = null)
    {
        panel.localScale = new Vector3(0, 0, 0);
        panel.DOScale(new Vector2(1.3f, 1.3f), 0.2f).OnComplete(() =>
        {
            panel.DOScale(new Vector2(1f, 1f), 0.1f).OnComplete(()=> { actionComplete(); });
        });
    }
    public static void HidePopup(Transform panel, GameObject parPanel, UnityAction action = null)
    {
        panel.DOScale(new Vector2(1.3f, 1.3f), 0.1f).OnComplete(() =>
        {
            panel.DOScale(new Vector2(0f, 0f), 0.2f).OnComplete(() =>
            {
                parPanel.SetActive(false);
                action();
            });
        });
    }
        public static IEnumerator LoadImage(Image img, string url)
    {
        //  Debug.Log("url: "+ url);
        if (string.IsNullOrEmpty(url))
        {
            img.sprite = null;
            yield break;
        }
        if (url.ToLower().StartsWith("http"))
        {
            WWW www = new WWW(url);
            yield return www;
            img.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        }
        else
        {
            //  Debug.Log("IS LOAD AVATAR");
            img.sprite = Resources.Load<Sprite>(url);
        }
    }
    public static DateTime TimeSpanToDateTime(TimeSpan timeSpan) { 
        DateTime dt = new DateTime(1970, 01, 01);
        //TimeSpan ts = timeSpan;
        return dt + timeSpan;
    }
    public static long ConvertToTimestamp(DateTime value)
    {
        //create Timespan by subtracting the value provided from
        //the Unix Epoch
        TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

        //return the total seconds (which is a UNIX timestamp)
        return (long)span.TotalSeconds;
    }
    public static int ConvertToTimestampDay(DateTime value)
    {
        //create Timespan by subtracting the value provided from
        //the Unix Epoch
        TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
         
        //return the total seconds (which is a UNIX timestamp)
        return (int)span.TotalDays;
    }
    public static float curDay()
    {
        TimeSpan span = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

        //return the total seconds (which is a UNIX timestamp)
        return (float)span.TotalDays;
    }

    public static long timeDelta(long fromTime)
    {
        var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        long _delta = Timestamp - fromTime;
        return _delta;
    }

    public static void TimerCountDown(Image imgClock, float totalTime, float remainTime)
    {
        imgClock.fillAmount = remainTime / totalTime;
        if (imgClock.fillAmount > 0.5f) imgClock.GetComponent<Image>().color = new Color32(41, 208, 13, 255);
        else if (imgClock.fillAmount > 0.25f) imgClock.GetComponent<Image>().color = new Color32(244, 239, 81, 255);
        else
        {
            decimal t = Math.Round(0.25m - (decimal)imgClock.fillAmount, 2);
            //Debug.Log(t + " - " + t * 100 % 2);
            if (t * 100 % 2 == 0)
                imgClock.GetComponent<Image>().color = new Color32(248, 244, 13, 255);
            else
                imgClock.GetComponent<Image>().color = new Color32(225, 21, 13, 255);
        }
    }
    public static IEnumerator CurrencyChange(Text textToDisplay, double orgNum, double desNum, float timePlay, bool DisplayZero)
    {
        float timeSinceStarted = 0f;
        while (true)
        {
            timeSinceStarted += Time.deltaTime;
            double fracJourney = (double)(timeSinceStarted / timePlay);
            if (fracJourney > 1)
                fracJourney = 1;
            double curNum = (desNum - orgNum) * fracJourney + orgNum;
            textToDisplay.text = GetFriendlyShortNumber(System.Math.Truncate(curNum));
            // If the object has arrived, stop the coroutine 
            if (curNum == desNum)
            {
                if (desNum == 0 && !DisplayZero)
                    textToDisplay.text = "0";
                textToDisplay.text = GetFriendlyShortNumber(System.Math.Truncate(desNum));
                yield break;
            }
            yield return null;
        }
    }
    public static IEnumerator CurrencyChangeNumber(double num, double orgNum, double desNum, float timePlay, bool DisplayZero)
    {
        float timeSinceStarted = 0f;
        while (true)
        {
            timeSinceStarted += Time.deltaTime;
            double fracJourney = (double)(timeSinceStarted / timePlay);
            if (fracJourney > 1)
                fracJourney = 1;
            double curNum = (desNum - orgNum) * fracJourney + orgNum;
            //textToDisplay.text = GetFriendlyNumber(curNum);
            num = curNum;
            // If the object has arrived, stop the coroutine 
            if (curNum == desNum)
            {
                if (desNum == 0 && !DisplayZero)
                    //    textToDisplay.text = "0";
                    //textToDisplay.text = GetFriendlyNumber(desNum);
                    num = 0;
                num = desNum;

                yield break;
            }
            yield return null;
        }
    }

    public static string GetFriendlyNumber(double inputNumber)
    {
        return string.Format("{0:n0}", inputNumber);
    }

    public static string GetFriendlyShortNumber(double inputNumber)
    {
        string retval = "";
        if (inputNumber < 1000)
        {
            retval = inputNumber.ToString("0.##");
        }
        else if (inputNumber < 1000000)
        {
            retval = (inputNumber / 1000).ToString("0.##") + "K";
        }
        else if (inputNumber < 1000000000)
        {
            retval = (inputNumber / 1000000).ToString("0.##") + "M";
        }
        else
        {
            retval = (inputNumber / 1000000000).ToString("0.##") + "B";
        }
        return retval;
    }

    public static IEnumerator MoveObjectToPos(GameObject gObject, Vector3 newPos, Vector3 newScale, float timeMove, bool isActive, bool isDestroy, float timeBeforeDestroy = 0)
    {
        if (gObject == null)
            yield break;
        float timeSinceStarted = 0f;
        Vector3 startPos = gObject.transform.position;
        Vector3 startScale = gObject.transform.localScale;
        while (true)
        {
            if (gObject == null)
                yield break;
            timeSinceStarted += Time.deltaTime;
            float fracJourney = timeSinceStarted / timeMove;
            if (fracJourney > 1)
                fracJourney = 1;
            gObject.transform.position = Vector3.Lerp(startPos, newPos, fracJourney);
            gObject.transform.localScale = Vector3.Lerp(startScale, newScale, fracJourney);
            // If the object has arrived, stop the coroutine
            if (gObject.transform.position == newPos && gObject.transform.localScale == newScale)
            {
                gObject.SetActive(isActive);
                if (isDestroy)
                {
                    Destroy(gObject, timeBeforeDestroy);
                }
                yield break;
            }

            // Otherwise, continue next frame
            yield return null;
        }
    }
    public static IEnumerator MoveObjectToLocalPos(GameObject gObject, Vector3 newLocalPos, Vector3 newRotate, Vector3 newScale, float timeMove, bool isActive, bool isDestroy, float timeBeforeDestroy = 0)
    {
        if (gObject == null)
            yield break;
        float timeSinceStarted = 0f;
        Vector3 startPos = gObject.transform.localPosition;
        Vector3 startScale = gObject.transform.localScale;
        Vector3 startRotate = gObject.transform.localEulerAngles;

        while (true)
        {
            if (gObject == null)
                yield break;
            timeSinceStarted += Time.deltaTime;
            float fracJourney = timeSinceStarted / timeMove;
            if (fracJourney > 1)
                fracJourney = 1;
            gObject.transform.localPosition = Vector3.Lerp(startPos, newLocalPos, fracJourney);
            gObject.transform.localScale = Vector3.Lerp(startScale, newScale, fracJourney);
            gObject.transform.localEulerAngles = Vector3.Lerp(startRotate, newRotate, fracJourney);
            // If the object has arrived, stop the coroutine
            if (gObject.transform.localPosition == newLocalPos && gObject.transform.localScale == newScale && gObject.transform.localEulerAngles == newRotate)
            {
                gObject.SetActive(isActive);
                if (isDestroy)
                {
                    Destroy(gObject, timeBeforeDestroy);
                }
                yield break;
            }

            // Otherwise, continue next frame
            yield return null;
        }
    }

    // public static string secondsToDisplayTime(float seconds)
    // {
    //     TimeSpan time = TimeSpan.FromSeconds(seconds);
    //     DateTime dateTime = DateTime.Today.Add(time);
    //     string displayTime = dateTime.ToString("hh:mm:tt");
    //     return displayTime;
    // }
    // // 0.33 ms
    // static string Method5(int secs)
    // {
    //     // Fastest way to create a DateTime at midnight
    //     return DateTime.FromBinary(599266080000000000).AddSeconds(secs).ToString("HH:mm:ss");
    // }

    // // 0.34 ms
    // static string Method4(int secs)
    // {
    //     return TimeSpan.FromSeconds(secs).ToString(@"hh\:mm\:ss");
    // }

    // 0.70 ms
    public static string secondsToDisplayTime(float secs)
    {
        TimeSpan t = TimeSpan.FromSeconds(secs);
        return string.Format("{0:D2}:{1:D2}:{2:D2}",
            t.Hours,
            t.Minutes,
            t.Seconds);
    }
   

    public static void ScrollSnapToSelect(ScrollRect scroll, RectTransform parContent, RectTransform target)
    {
        float heightTarget = target.rect.height;
        Canvas.ForceUpdateCanvases();
        Vector2 temp = (Vector2)scroll.transform.InverseTransformPoint(parContent.position)
            - (Vector2)scroll.transform.InverseTransformPoint(target.position);
        parContent.anchoredPosition = new Vector2(parContent.anchoredPosition.x, temp.y - heightTarget / 2);
    }
   

    public static bool HasConnection()
    {
        try
        {
            using (var client = new WebClient())
            using (var stream = new WebClient().OpenRead("http://www.google.com"))
            {
                return true;
            }
        }
        catch
        {
            return false;
        }
    }
  
}
