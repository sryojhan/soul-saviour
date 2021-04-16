using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeBoss : MonoBehaviour
{
    [SerializeField] int numTorches;
    [SerializeField] GameObject boss;

    public void lightTorch()
    {
        numTorches--;
        if (numTorches <= 0)
        {
            invokeBoss();
        }
    }

    void invokeBoss()
    {
        print("invoke boss");
    }


}
