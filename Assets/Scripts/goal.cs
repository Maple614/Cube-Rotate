using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class goal : MonoBehaviour {
    public GameObject[] cubes;

    private AudioSource clear_sound;
    void Start()
    {
        clear_sound = GetComponent<AudioSource>();
    }

    //goalオブジェクトに触れ、同じ色ならクリア
    private void OnTriggerEnter(Collider other)
    {

        if ((other.gameObject.tag == "Player") && (getIdx() == true))
        {
            clear_sound.PlayOneShot(clear_sound.clip);
            StartCoroutine(toTitle());
            Debug.Log("goal");

        }
    }

    //各cubeの色を取得し、比較を行う
    bool getIdx()
    {
        int[] colorIdx = new int[cubes.Length];
        for(int i=0; i<cubes.Length; i++)
        {
            colorIdx[i] = cubes[i].GetComponent<colorChanger>().CurrentColorIdx;
            Debug.Log(i + ": " + colorIdx[i]);
        }

        for(int i=0; i<cubes.Length-1; i++)
        {
            if(colorIdx[i] != colorIdx[i + 1])
            {
                Debug.Log("false");
                return false;
            }
        }
        Debug.Log("true");
        return true;
    }

    IEnumerator toTitle()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("title");
    }
}
