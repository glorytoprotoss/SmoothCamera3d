using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour
{
    [SerializeField] GameObject screen;


    [SerializeField] float speed;

    float pixelSize;
    float vertPixelBuffer;
    float horPixelBuffer;
    Vector3 screenPoint;
    Resolution res;
    int height;
    int width;
    private RenderTexture _renderTexture;
    Camera cam;

    
    int referenceHeight = 180;
    //How much we need to increase the size of quad for it to take entire camera
    float screenCompinsator = 1.4f;
    float canvasHight = 180f;
    float canvasWidth = 320f;

    private void Start()
    {
        cam = GetComponent<Camera>();
        pixelSize = (2f * cam.orthographicSize * screenCompinsator) / Screen.height;



        screenPoint = screen.transform.position;
        res = Screen.currentResolution;
        height = Screen.height;
        width = Screen.width;
        Debug.Log(res);
        Debug.Log(height);
        Debug.Log(width);
        //Based on the hight and with adjust the quad
        if ((float)width / height >= 1.333)
        {
            if ((float)width / height >= 1.777)
            {
                if ((float)width / height > 2.333)
                {
                    screen.transform.localScale = new Vector3(21f, 9f, 1f) * screenCompinsator;
                }
                else
                {
                    screen.transform.localScale = new Vector3(16f, 9f, 1f) * screenCompinsator;
                }
            }
            else
            {
                screen.transform.localScale = new Vector3(4f, 3f, 1f) * screenCompinsator;
            }
        }
        else
        {
            screen.transform.localScale = new Vector3(1f, 1f, 1f) * screenCompinsator;
        }
        //pixelSize = screen.transform.localScale.x / canvasWidth;

    }

    void Update()
    {
        GetBaseInput();

        if (Mathf.Abs(vertPixelBuffer) > pixelSize)
        {
            //buffer remembers how much we moved cam
            transform.position += new Vector3(0, 0, Mathf.Round(vertPixelBuffer / pixelSize) * pixelSize);
            vertPixelBuffer -= Mathf.Round(vertPixelBuffer / pixelSize) * pixelSize;
            screen.transform.position = screenPoint + new Vector3(0, vertPixelBuffer * screenCompinsator, 0);
        }
        if (Mathf.Abs(horPixelBuffer) > pixelSize)
        {
            transform.position += new Vector3(Mathf.Round(horPixelBuffer / pixelSize) * pixelSize, 0, 0);
            horPixelBuffer -= Mathf.Round(horPixelBuffer / pixelSize) * pixelSize;
            screen.transform.position = screenPoint + new Vector3(horPixelBuffer * screenCompinsator, 0, 0);
        }
    }





    private void GetBaseInput()
    {

        if (Input.GetKey(KeyCode.W))
        {
            vertPixelBuffer += speed * Time.deltaTime;
            screen.transform.position += new Vector3(0, speed * Time.deltaTime * screenCompinsator, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vertPixelBuffer -= speed * Time.deltaTime;
            screen.transform.position += new Vector3(0, -speed * Time.deltaTime * screenCompinsator, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            horPixelBuffer -= speed * Time.deltaTime;
            screen.transform.position += new Vector3(speed * Time.deltaTime * screenCompinsator, 0, 0);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            horPixelBuffer += speed * Time.deltaTime;
            screen.transform.position += new Vector3(-speed * Time.deltaTime * screenCompinsator, 0, 0);
        }

    }
}
