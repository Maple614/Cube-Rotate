using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class modeSelect : MonoBehaviour {

	public void OnClick(int number)
    {
        switch (number)
        {
            case 0:
                SceneManager.LoadScene("easy");
                break;
            case 1:
                SceneManager.LoadScene("normal");
                break;
            case 2:
                SceneManager.LoadScene("hard");
                break;
            default:
                break;
        }
    }
}
