using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTurn : MonoBehaviour
{
    private string turnInputAxis = "Horizontal";

    public float rotationRate = 360;

    void FixedUpdate()
    {
        float turnAxis = Input.GetAxis(turnInputAxis);

        if (turnAxis != 0)
        {
            Turn(turnAxis);
        }
    }

    void Turn(float input)
    {
        bool isRight = false; //whether the wheels are currently pointing to the right

        //Vector3 angles = transform.localEulerAngles;

        /*if(angles.y >= 0 && angles.y <= 180)
        {
            isRight = true;
        }*/

        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);

        Vector3 newAngles = transform.localEulerAngles;

        if (newAngles.y >= 0 && newAngles.y <= 180)
        {
            isRight = true;
        }

        if (isRight)
        {
            newAngles.y = Mathf.Clamp(newAngles.y, 0.0f, 30.0f);
        }
        else
        {
            newAngles.y = Mathf.Clamp(newAngles.y, 330.0f, 360.0f);
        }

        //Debug.Log(gameObject.transform.localEulerAngles.y);
        transform.localEulerAngles = newAngles;
        Debug.Log(gameObject.transform.localEulerAngles.y);

        /*if ((yRot <= 30 && yRot >= 0) || (yRot <= 360 && yRot >= 330))
        {
            Debug.Log(gameObject.transform.localEulerAngles.y);
            transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
        }

        if (yRot < 330 && yRot > 180)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 330, transform.localEulerAngles.z);
        }

        if (yRot <= 180 && yRot > 30)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 30, transform.localEulerAngles.z);
        }*/
    }
}
