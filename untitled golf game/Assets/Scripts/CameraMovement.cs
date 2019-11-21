using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;

    [SerializeField] Vector3 offset = new Vector3(0, 17, -6);

    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
