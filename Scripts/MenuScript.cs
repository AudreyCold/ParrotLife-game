using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject cont;
    public GameObject change;

    void Start()
    {
        if (!File.Exists(Application.dataPath + "/info.txt"))
        {
            cont.GetComponent<Image>().color = Color.gray;
            cont.GetComponent<Button>().enabled = false;

            change.GetComponent<Image>().color = Color.gray;
            change.GetComponent<Button>().enabled = false;
        }
        else if (System.IO.File.ReadAllLines(Application.dataPath + "/info.txt").Length == 0)
        {
            cont.GetComponent<Image>().color = Color.gray;
            cont.GetComponent<Button>().enabled = false;

            change.GetComponent<Image>().color = Color.gray;
            change.GetComponent<Button>().enabled = false;
        }
        else
        {
            cont.GetComponent<Button>().enabled = true;
            change.GetComponent<Button>().enabled = true;
        }
    }

	public void OnClickChoice()
    {
        SceneManager.LoadScene("choice");
    }

    public void OnClickNewGame()
    {
        SceneManager.LoadScene("change");
    }

    public void OnClickContinue()
    {
        SceneManager.LoadScene("game");
    }
}
