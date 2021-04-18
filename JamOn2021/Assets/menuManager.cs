using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuManager : MonoBehaviour
{

    [SerializeField] GameObject help;
    public void Play()
    {
        GameManager.instance.changeScene();
    }

    public void Exit()
    {
        GameManager.instance.exit();
    }

    public void helpPepe()
    {
        help.SetActive(!help.activeSelf);
    }
}
