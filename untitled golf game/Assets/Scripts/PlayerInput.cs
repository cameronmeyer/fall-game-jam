using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour {

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
        Forward ();
        Backward ();
        Turn ();
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
}