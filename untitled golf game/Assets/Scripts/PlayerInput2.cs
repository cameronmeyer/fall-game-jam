using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput2 : MonoBehaviour {

    //All of this is kind of dumb.
    //The whole thing could be solved with an "input window"
    //If (WaitingOnPlayer == 1), whoever presses a is assigned left.

    PlayerControls controls;
    Vector2 steer;
    float gas;
    float brake;

    //private Player player;
    public int playerNumber;

    public float rotationRate = 180;

    [SerializeField] float moveSpeed = 0.2f;
    [SerializeField] float brakeSpeed = 0.5f;
    [SerializeField] GameObject lWheel;

    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;

    void Awake () {
        controls = new PlayerControls ();
        //lambda expressions. feelsBadMan
        controls.Gameplay.Forward.performed += ctx => gas = ctx.ReadValue<float> ();
        controls.Gameplay.Forward.canceled += ctx => gas = 0;

        controls.Gameplay.Backward.performed += ctx => brake = ctx.ReadValue<float> ();
        controls.Gameplay.Backward.canceled += ctx => brake = 0;

        controls.Gameplay.Steer.performed += ctx => steer = ctx.ReadValue<Vector2> ();
    }

    void OnEnable () {
        controls.Gameplay.Enable ();
    }

    void FixedUpdate () {
        //Forward ();
        //Backward ();
        //Turn ();

        float motor = maxMotorTorque * gas;
        float steering = maxSteeringAngle * steer.x;

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }
    }

    void Forward () {
        transform.Translate (Vector3.forward * gas * moveSpeed);
    }

    void Backward () {
        transform.Translate (Vector3.forward * brake * -brakeSpeed);
    }

    void Turn () {
        if (gas + brake != 0) {
            if (gas + brake < 0) {
                transform.Rotate (0, -steer.x * rotationRate * Time.deltaTime, 0);
            } else {
                transform.Rotate (0, steer.x * rotationRate * Time.deltaTime, 0);
            }
        }
    }

    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor;
        public bool steering;
    }
}