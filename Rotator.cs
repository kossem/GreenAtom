using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;
    public bool a = true;


    void Update()
    {
        if (a)
            transform.Rotate(new Vector3(0, Time.deltaTime * 10, 0));
        if (Input.GetMouseButton(0) && (Input.mousePosition.x <= 3 * Screen.width / 4) && (Input.mousePosition.x > Screen.width / 4))
        {
            float h = horizontalSpeed * Input.GetAxis("Mouse X");
            float v = verticalSpeed * Input.GetAxis("Mouse Y");
            transform.Rotate(-v, -h, 0);
            a = false;
        }
    }
}
