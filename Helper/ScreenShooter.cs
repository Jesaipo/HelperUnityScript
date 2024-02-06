using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShooter : MonoBehaviour
{
    static int count = 0;
    public float CDREf = 2f;
    float cd = 1f;
    // Update is called once per frame
    void Update()
    {
        cd -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) || cd < 0f)
        {
            ScreenCapture.CaptureScreenshot("C:\\Users\\Jesaipo\\Desktop\\screenshot-"+ count +".png");
            count++;
            Debug.Log("SCREEN");
            cd = CDREf;
        }
    }
}
