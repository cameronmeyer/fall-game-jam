using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeToScene (string SceneToChangeTo)
    {
        //Application.LoadLevel(SceneToChangeTo);
        SceneManager.LoadScene(SceneToChangeTo);
	}
}
