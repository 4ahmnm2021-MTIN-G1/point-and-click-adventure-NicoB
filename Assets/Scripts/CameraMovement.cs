using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float cameraSensibility = 0.1f;
    float cameraSize;
    Camera camera;
    Vector3 positionToGo;
    bool startZoom;
    float speed;
    float speedZoom;
    Vector2 mousePosition;
    void Start()
    {
        positionToGo.y = this.transform.position.y;
        positionToGo.z = this.transform.position.z;
        camera = this.GetComponent<Camera>();
    }
    void Update()
    {
        mousePosition = Input.mousePosition;
        if (mousePosition.x >= 0 && mousePosition.x <= Screen.width && mousePosition.y >= 0 && mousePosition.y <= Screen.height)
        {
            DoZoom();
            DoMove();
            this.transform.position = positionToGo;
        }

    }
    void DoZoom()
    {
        float mouseWheel = Input.mouseScrollDelta.y;
        if (mouseWheel != 0 || startZoom == true)
        {
            float destination = camera.orthographicSize - mouseWheel;
            if (destination >= 0.83f && destination <= 6f)
            {
                startZoom = true;
                if (mouseWheel == 3 || mouseWheel == -3)
                {
                    speed = 3;
                }
                else if (mouseWheel == 2 || mouseWheel == -2)
                {
                    speed = 2;
                }
                else if (mouseWheel == 1 || mouseWheel == -1)
                {
                    speed = 1;
                }
                if (mouseWheel != 0)
                {
                    step = 0;
                    a = camera.orthographicSize;
                    b = camera.orthographicSize - mouseWheel;
                }
                if (speed == 1)
                {
                    speedZoom = 0.01f;
                }
                else if (speed == 2)
                {
                    speedZoom = 0.1f;
                }
                else if (speed == 3)
                {
                    speedZoom = 0.15f;
                }
                step += speedZoom + Time.deltaTime;
                float zoom = Mathf.Lerp(a, b, step);
                camera.orthographicSize = zoom;
                if (zoom == b)
                {
                    startZoom = false;
                }
            }
        }
    }
    void DoMove()
    {
        //left right
        //left
        if (mousePosition.x <= Screen.width / 4)
        {
            float intensity = Mathf.Abs(1 - mousePosition.x / (Screen.width / 4));
            positionToGo.x -= cameraSensibility * intensity;
        }
        //right
        if (mousePosition.x >= 3 * (Screen.width / 4))
        {
            float intensity = Mathf.Abs((mousePosition.x - (3 * (Screen.width / 4))) / (Screen.width / 4));
            positionToGo.x += cameraSensibility * intensity;
        }
        //top down
        //up
        if (mousePosition.y <= Screen.height / 4)
        {
            float intensity = Mathf.Abs(1 - mousePosition.y / (Screen.width / 4));
            positionToGo.z -= cameraSensibility * intensity;
        }
        //down
        if (mousePosition.y >= 3 * (Screen.height / 4))
        {
            float intensity = Mathf.Abs((mousePosition.y - (3 * (Screen.height / 4))) / (Screen.height / 4));
            positionToGo.z += cameraSensibility * intensity;
        }
    }
    float step;
    float a;
    float b;
}
