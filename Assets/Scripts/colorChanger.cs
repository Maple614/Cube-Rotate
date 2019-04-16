using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChanger : MonoBehaviour {

    //色をヒエラルキーから設定
    public Color[] Colors;

    Renderer Renderer;
    public int CurrentColorIdx;

    private void Awake()
    {
        Renderer = GetComponent<Renderer>();
        //始めのカラー表示
        ChangeMaterialColor();
    }

    void OnCollisionEnter(Collision collision)
    {
        ChangeMaterialColor();
    }

    void ChangeMaterialColor()
    {
        CurrentColorIdx++;
        if(CurrentColorIdx >= Colors.Length)
        {
            CurrentColorIdx = 0;
        }
        Renderer.material.color = Colors[CurrentColorIdx];

    }

    public int ColorIdx
    {
        get
        {
            return CurrentColorIdx;
        }

        set
        {
            CurrentColorIdx = value;
            Renderer.material.color = Colors[CurrentColorIdx];
            if (CurrentColorIdx >= Colors.Length)
                CurrentColorIdx = 0;

        }
    }
}
