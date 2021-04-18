using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuManager : MonoBehaviour
{
    public void Play()
    {
        GameManager.instance.changeScene();
    }

    public void Exit()
    {
        GameManager.instance.exit();
    }
}
