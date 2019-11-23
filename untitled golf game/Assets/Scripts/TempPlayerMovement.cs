using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempPlayerMovement : MonoBehaviour
{
    private string moveInputAxis = "Vertical";
    private string turnInputAxis = "Horizontal";

    private int score;

    private float lastCollision;

    public Text scoreText;

    [SerializeField] float rotationRate = 360;
    [SerializeField] float moveSpeed = 2;
    [SerializeField] float minTimeBetCollisions = 0.5f;

    private void Start()
    {
        lastCollision = Time.time;
        score = 0;
        updateScore();
    }

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
        transform.Translate(Vector3.forward * input * moveSpeed);
    }

    private void Turn(float turnInput, float moveInput)
    {
        if (moveInput != 0)
        {
            if(moveInput < 0)
            {
                turnInput = -turnInput;
            }

            transform.Rotate(0, turnInput * rotationRate * Time.deltaTime, 0);
        }
    }

    private void updateScore()
    {
        scoreText.text = score.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            if(Time.time - lastCollision > minTimeBetCollisions)
            {
                lastCollision = Time.time;
                score++;
                updateScore();
            }
        }
    }
}
