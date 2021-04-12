using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EscenaInicial : MonoBehaviour
{
    static string[] NAMES = { "Yojhan Steven García Peña", "Iván Sánchez Míguez", "Pablo Fernández Álvarez" };

    public Transform logo;
    public GameObject yojhanNoSabeHacerTortitas;

    public GameObject title;
    public Text groupMembers;

    public float logoTime = 1;
    public float aGameByTime = 1;


    private float t = 0;
    bool sceneSwap = false;
    private void Update()
    {
        if (!sceneSwap)
        {
            if ((t += Time.deltaTime) > logoTime)
            {
                Change();
            }
            logo.localScale = Vector3.one * Mathf.Lerp(.5f, .7f, t / logoTime);
        }
        else
        {
            if ((t += Time.deltaTime) > aGameByTime)
            {
                SceneManager.LoadScene((int)Escenas.Menu);
            }
        }
    }


    void Change()
    {
        t = 0;
        sceneSwap = true;

        logo.gameObject.SetActive(false);
        yojhanNoSabeHacerTortitas.SetActive(false);

        title.SetActive(true);
        groupMembers.gameObject.SetActive(true);
        createText();
    }

    void createText()
    {
        int[] orden = { 0, 1, 2 };
        for (int i = 1; i < 3; i++)
        {
            int random = Random.Range(0, 3);
            int c = orden[random];
            orden[random] = orden[i];
            orden[i] = c;
        }

        groupMembers.text = "";
        for (int i = 0; i < 3; i++)
        {
            groupMembers.text += NAMES[orden[i]] + "\n";
        }
    }
}
