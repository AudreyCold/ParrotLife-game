using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveScript : MonoBehaviour
{
    string parrot_number;

    public InputField name;

    public GameObject Error;
    public Text TextError;

    void Start()
    {
        Error.SetActive(false);

        if (!File.Exists(Application.dataPath + "/info.txt"))
        {
            FileStream fileInfo = new FileStream(Application.dataPath + "/info.txt", FileMode.Create);
            fileInfo.Close();

            StreamWriter sw = new StreamWriter(Application.dataPath + "/info.txt");
            sw.Close();
        }
    }

    public void OnClickBlue()
    {
        File.WriteAllText(Application.dataPath + "/info.txt", string.Empty);

        StreamWriter sw = new StreamWriter(Application.dataPath + "/info.txt");
        sw.WriteLine("1");

        sw.Close();
    }

    public void OnClickGreen()
    {
        File.WriteAllText(Application.dataPath + "/info.txt", string.Empty);

        StreamWriter sw = new StreamWriter(Application.dataPath + "/info.txt");
        sw.WriteLine("2");

        sw.Close();
    }

    public void OnClickYellow()
    {
        File.WriteAllText(Application.dataPath + "/info.txt", string.Empty);

        StreamWriter sw = new StreamWriter(Application.dataPath + "/info.txt");
        sw.WriteLine("3");

        sw.Close();
    }

    public void SaveChanges()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/info.txt");
        string line = sr.ReadLine();
        sr.Close();

        if (line == null)
        {
            Error.SetActive(true);
            TextError.text = "Не выбран попугай!";
            return;
        }
        else if (name.text == "")
        {
            Error.SetActive(true);
            TextError.text = "Не выбрано имя для попугая!";
            return;
        }

        string file_info = Application.dataPath + "/info.txt";

        System.IO.StreamWriter sw = new System.IO.StreamWriter(Application.dataPath + "/info.txt", true);

        sw.WriteLine(name.text);
        sw.WriteLine("100");
        sw.WriteLine("100");
        sw.WriteLine("100");
        sw.WriteLine("off");

        sw.Close();

        SceneManager.LoadScene("game");

    }

    public void CloseError()
    {
        Error.SetActive(false);
    }
}
