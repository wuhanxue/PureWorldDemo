using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeToBattle()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeToMain()
    {
        SceneManager.LoadScene(0);
    }
}
