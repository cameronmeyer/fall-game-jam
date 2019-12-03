using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayerController : MonoBehaviour {
    private List<int> assignedControllers = new List<int> ();
    //private PlayerPanel[] playerPanels;
    public int maxPlayerCount = 2;

    private void Awake () {
        //playerPanels = FindObjectsOfType
    }

    void Update () {
        for (int i = 1; i <= maxPlayerCount; i++) {
            if (Input.GetButton ("A P" + i)) {
                //AddPlayerController (i);
                break;
            }
        }
    }
}