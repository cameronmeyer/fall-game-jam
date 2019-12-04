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
    public float maxBrakeTorque;
    public float maxSteeringAngle;
    public float wheelRpm;
    public float brakePause = 0f;

    void Awake()
    {
        controls = new PlayerControls();
        //lambda expressions. feelsBadMan
        controls.Gameplay.Forward.performed += ctx => gas = ctx.ReadValue<float>();
        controls.Gameplay.Forward.canceled += ctx => gas = 0;

        controls.Gameplay.Backward.performed += ctx => brake = ctx.ReadValue<float>();
        controls.Gameplay.Backward.canceled += ctx => brake = 0;

        controls.Gameplay.Steer.performed += ctx => steer = ctx.ReadValue<Vector2>();
        //controls.Gameplay.Steer.canceled += ctx => steer = new Vector2(0, 0);
    }

    void OnEnable () {
        controls.Gameplay.Enable ();
    }

    void FixedUpdate () {
        //Forward ();
        //Backward ();
        //Turn ();

        float motor = maxMotorTorque * gas;
        float brakes = maxBrakeTorque * brake;
        float steering = maxSteeringAngle * steer.x;

        wheelRpm = axleInfos[1].leftWheel.rpm;

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
                
                if ((axleInfo.leftWheel.rpm > 0 && brakes > 0) || motor > 0)
                {
                    axleInfo.leftWheel.brakeTorque = brakes;
                    axleInfo.rightWheel.brakeTorque = brakes;

                    brakePause = 2.0f;
                }
                if(axleInfo.leftWheel.rpm <= 0 && brakes > 0 && brakePause <= 0)
                {
                    axleInfo.leftWheel.brakeTorque = 0;
                    axleInfo.rightWheel.brakeTorque = 0;

                    axleInfo.leftWheel.motorTorque = .5f * maxMotorTorque * brake * (-1);
                    axleInfo.rightWheel.motorTorque = .5f * maxMotorTorque * brake * (-1);
                }
            }
        }
    }

    private void Update()
    {
        if(brakePause > 0)
        {
            brakePause -= Time.deltaTime;
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