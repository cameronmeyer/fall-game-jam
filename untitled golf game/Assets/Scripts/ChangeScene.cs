using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void ChangeToScene (string SceneToChangeTo) {
        Application.LoadLevel(SceneToChangeTo);
	}
}
