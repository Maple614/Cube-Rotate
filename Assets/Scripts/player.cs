using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    Vector3 rotatePoint = Vector3.zero; //回転の中心
    Vector3 rotateAxis = Vector3.zero;  //回転軸
    float cubeAngle = 0f;   //回転角

    float cubeSizeHarf; //キューブの大きさの半分
    bool isRotate = false;  //回転中フラグ
    public GameObject startObj;  //スタート位置

    public GameObject[] cubes;

    private AudioSource sound;

    void Start () {
        cubeSizeHarf = transform.localScale.x / 2f;
        Vector3 pos = startObj.transform.position;
        pos.y += 2;
        transform.position = pos;
        sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        //回転中は入力を受け付けない
        if (isRotate)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            rotatePoint = transform.position + new Vector3(0f, -cubeSizeHarf, cubeSizeHarf);
            rotateAxis = new Vector3(1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rotatePoint = transform.position + new Vector3(0f, -cubeSizeHarf, -cubeSizeHarf);
            rotateAxis = new Vector3(-1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rotatePoint = transform.position + new Vector3(-cubeSizeHarf, -cubeSizeHarf, 0f);
            rotateAxis = new Vector3(0, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rotatePoint = transform.position + new Vector3(cubeSizeHarf, -cubeSizeHarf, 0f);
            rotateAxis = new Vector3(0, 0, -1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            restartGame();
        }
        if(rotatePoint == Vector3.zero)
            return;

        StartCoroutine(MoveCube());
    }

    IEnumerator MoveCube()
    {
        //回転フラグを立てる
        isRotate = true;

        //回転処理
        float sumAngle = 0f;
        while(sumAngle < 90f)
        {
            cubeAngle = 30f;
            sumAngle += cubeAngle;

            if(sumAngle > 90f)
            {
                cubeAngle -= sumAngle - 90f;
            }

            transform.RotateAround(rotatePoint, rotateAxis, cubeAngle);

            yield return null;
        }
        sound.PlayOneShot(sound.clip);
        //回転フラグ終了
        isRotate = false;
        rotatePoint = Vector3.zero;
        rotateAxis = Vector3.zero;

        yield break;
    }

    //fallcheckオブジェクトに触れたらリスタート
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FallCheck")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().rotation = Quaternion.identity;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            Vector3 pos = startObj.transform.position;
            pos.y += 2;
            transform.position = pos;

            Debug.Log("restart");
        }
    }

    //リスタート
    void restartGame()
    {
        Vector3 pos = startObj.transform.position;
        pos.y += 2;
        transform.position = pos;
        //各cubeの色を戻す
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].GetComponent<colorChanger>().ColorIdx = 0;
        }
    }

}
