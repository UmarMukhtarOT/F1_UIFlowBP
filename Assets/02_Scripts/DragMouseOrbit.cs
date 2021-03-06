using UnityEngine;

using System.Collections;
using ControlFreak2;

public class DragMouseOrbit : MonoBehaviour
{
   
    public float distance = 2.0f;
    public float xSpeed = 20.0f;
    public float ySpeed = 20.0f;
    public float yMinLimit = -90f;
    public float yMaxLimit = 90f;
    public float distanceMin = 10f;
    public float distanceMax = 10f;
    public float smoothTime = 2f;
    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;
    float velocityX = 0.0f;
    float velocityY = 0.0f;
    public static bool CantMoveAll=false;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationYAxis = angles.y;
        rotationXAxis = angles.x;
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }
    void LateUpdate()
    {
        if (!CantMoveAll)
        {

            if (CF2Input.GetMouseButton(0))
            {



                // Debug.Log("mouse y pos **** "+Input.mousePosition.y);
                velocityX += xSpeed * CF2Input.GetAxis("Mouse X") * -0.02f;
                velocityY += ySpeed * CF2Input.GetAxis("Mouse Y") * 0.02f;
            }
           // velocityX = -0.5f;
            rotationYAxis += velocityX;
            rotationXAxis -= velocityY;
            rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
            Quaternion fromRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            Quaternion toRotation = Quaternion.Euler(0, rotationYAxis, 0);
            Quaternion rotation = toRotation;

           
            transform.rotation = rotation;
           
            velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
            velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
        }
    }
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}