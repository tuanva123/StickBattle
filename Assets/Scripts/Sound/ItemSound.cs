using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSound : MonoBehaviour
{
    

    public void StartSoundItem(AudioClip clip, float volume)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().volume = volume;
        GetComponent<AudioSource>().Play();
        StartCoroutine(_StartSound());
    }

    IEnumerator _StartSound()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        while (true)
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                gameObject.SetActive(false);
                yield break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
