using UnityEngine;

public class SmoothCamera1 : MonoBehaviour
{
    [Header("Main Params")]
    [SerializeField] private int pixelToScreenPixelsSize;

    [Header("Movement Params")]
    [SerializeField] private float movementSpeed;

    [Header("Link Params")]
    [SerializeField] private Camera renderTexCamera;
    [SerializeField] private Camera realWorldCamera;

    [SerializeField] private GameObject renderTexPlane;
    [SerializeField] private RenderTexture renderTexture;

    private Vector2Int resolution;

    private float realWorldCameraStep;

    private void Start()
    {
        int height = Screen.currentResolution.height / pixelToScreenPixelsSize * 2;
        int width = height / 45 * 80;
        
        resolution = new Vector2Int(width, height); Debug.Log(resolution.ToString());

        renderTexPlane.transform.localScale = new Vector3(resolution.x / 100f, resolution.y / 100f, 1f);

        renderTexCamera.orthographicSize = renderTexPlane.transform.localScale.y / 2;

        renderTexture.width = resolution.x;
        renderTexture.height = resolution.y;

        realWorldCameraStep = realWorldCamera.orthographicSize * 2 / height; Debug.Log(realWorldCameraStep);
    }

    private void Update()
    {
        MoveInput();

        if (renderTexCamera.gameObject.transform.localPosition.x >= 0.01f)
        {
            realWorldCamera.gameObject.transform.localPosition += new Vector3(realWorldCameraStep * Mathf.Round(renderTexCamera.gameObject.transform.localPosition.x / 0.01f), 0, 0);

            renderTexCamera.gameObject.transform.localPosition = new Vector3(0, renderTexCamera.gameObject.transform.localPosition.y, 0);
        }

        if (renderTexCamera.gameObject.transform.localPosition.x <= -0.01f)
        {
            realWorldCamera.gameObject.transform.localPosition += new Vector3(realWorldCameraStep * Mathf.Round(renderTexCamera.gameObject.transform.localPosition.x / 0.01f), 0, 0);

            renderTexCamera.gameObject.transform.localPosition = new Vector3(0, renderTexCamera.gameObject.transform.localPosition.y, 0);
        }

        if (renderTexCamera.gameObject.transform.localPosition.y >= 0.01f)
        {
            realWorldCamera.gameObject.transform.localPosition += new Vector3(0, realWorldCameraStep * Mathf.Round(renderTexCamera.gameObject.transform.localPosition.y / 0.01f), 0);

            renderTexCamera.gameObject.transform.localPosition = new Vector3(renderTexCamera.gameObject.transform.localPosition.x, 0, 0);
        }

        if (renderTexCamera.gameObject.transform.localPosition.y <= -0.01f)
        {
            realWorldCamera.gameObject.transform.localPosition += new Vector3(0, realWorldCameraStep * Mathf.Round(renderTexCamera.gameObject.transform.localPosition.y / 0.01f), 0);

            renderTexCamera.gameObject.transform.localPosition = new Vector3(renderTexCamera.gameObject.transform.localPosition.x, 0, 0);
        }

        //realWorldCamera.gameObject.transform.localPosition = new Vector3((float)Math.Round(realWorldCamera.gameObject.transform.localPosition.x, 2), (float)Math.Round(realWorldCamera.gameObject.transform.localPosition.y, 2), realWorldCamera.gameObject.transform.localPosition.z);
    }

    private void MoveInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            renderTexCamera.gameObject.transform.localPosition += new Vector3(0, movementSpeed, 0) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            renderTexCamera.gameObject.transform.localPosition += new Vector3(0, -movementSpeed, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            renderTexCamera.gameObject.transform.localPosition += new Vector3(movementSpeed, 0, 0) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            renderTexCamera.gameObject.transform.localPosition += new Vector3(-movementSpeed, 0, 0) * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            realWorldCamera.gameObject.transform.localPosition += new Vector3(0, realWorldCameraStep, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            realWorldCamera.gameObject.transform.localPosition -= new Vector3(0, realWorldCameraStep, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            realWorldCamera.gameObject.transform.localPosition += new Vector3(realWorldCameraStep, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            realWorldCamera.gameObject.transform.localPosition -= new Vector3(realWorldCameraStep, 0, 0);
        }
    }
}
