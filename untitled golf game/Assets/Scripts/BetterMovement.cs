using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterMovement : MonoBehaviour
{
    public GameObject RearLWheel;
    public GameObject RearRWheel;

    Vector3 forward = new Vector3(100, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RearLWheel.transform.Rotate(forward * Time.deltaTime);
        RearRWheel.transform.Rotate(forward * Time.deltaTime);
    }
}
