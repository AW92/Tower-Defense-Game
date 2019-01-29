using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Panning")]
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public float minX = -80f;
    public float maxX = 70f;
    public float minZ = -80f;
    public float maxZ = 80f;

    private bool doMovement = true;

    [Header("Zooming")]
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 110f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown (KeyCode.Escape))
        {
            doMovement = !doMovement;
        }
        if (doMovement == false)
        {
            return;
        }
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        //these stop the camera going ridiculously far/close
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        transform.position = pos;
    }
}
