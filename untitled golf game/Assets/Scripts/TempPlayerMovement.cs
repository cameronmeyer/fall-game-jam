using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerMovement : MonoBehaviour
{
    private string moveInputAxis = "Vertical P1";
    private string turnInputAxis = "Horizontal P1";

    public float rotationRate = 360;

    [SerializeField] float moveSpeed = 2;
    [SerializeField] GameObject lWheel;

    void FixedUpdate()
    {
        float moveAxis = Input.GetAxis(moveInputAxis);
        float turnAxis = Input.GetAxis(turnInputAxis);

        ApplyInput(moveAxis, turnAxis);
    }

    private void ApplyInput(float moveInput, float turnInput)
    {
        Move(moveInput);
        Turn(turnInput, moveInput);
    }

    private void Move(float input)
    {
        /*if (lWheel != null)
        {
            transform.Translate(lWheel.transform.forward * input * moveSpeed);
        }
        else
        {*/
            transform.Translate(Vector3.forward * input * moveSpeed);
        //}
    }

    private void Turn(float turnInput, float moveInput)
    {
        if (moveInput != 0)
        {
            if (moveInput > 0)
            {
                transform.Rotate(0, turnInput * rotationRate * Time.deltaTime, 0);
            }
            else
            {
                transform.Rotate(0, -turnInput * rotationRate * Time.deltaTime, 0);
            }
        }
    }
}
