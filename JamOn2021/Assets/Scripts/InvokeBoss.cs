using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeBoss : MonoBehaviour
{
    [SerializeField] GameObject[] torches;
    [SerializeField] GameObject boss;

    [SerializeField] Sprite lightedTorch;

    int numTorches;

    int indexAux = 0;

    private void Start()
    {
        numTorches = torches.Length;
    }

    public void lightTorch()
    {
        if (indexAux < torches.Length)
        {
            numTorches--;
            torches[indexAux].GetComponent<SpriteRenderer>().sprite = lightedTorch;
            indexAux++;
            SoundManager.instance.putTorch();
            if (numTorches <= 0) invokeBoss();
        }
    }

    void invokeBoss()
    {
        boss.SetActive(true);
        boss.GetComponent<BossBattle>().StartBattle();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (numTorches <= 0)
        {
            if (collision.GetComponent<PlayerSweepAttack>())
            {
                invokeBoss();
            }
        }
    }

}
