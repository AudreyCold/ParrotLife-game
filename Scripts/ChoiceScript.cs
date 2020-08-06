using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class ChoiceScript : MonoBehaviour
{
    string parrot_number;
    string parrot_name;

    string new_number;

    public InputField Name;
     
     void Start()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/info.txt");

        parrot_number = sr.ReadLine();
        parrot_name = sr.ReadLine();
         
        Name.text = parrot_name;
    }
     
    public void OnClickBlue()
    {
        PlayerPrefs.SetString(new_number, "1");
    }

    public void OnClickGreen()
    {
        PlayerPrefs.SetString(new_number, "2");
    }

    public void OnClickYellow()
    {
        PlayerPrefs.SetString(new_number, "3");
    }
     
     public void Save()
    {
        if (Name.text != "")
        {
            string str = string.Empty;

            using (System.IO.StreamReader reader = System.IO.File.OpenText(Application.dataPath + "/info.txt"))
            {
                str = reader.ReadToEnd();
            }

            str = str.Replace(parrot_name, Name.text);
            str = str.Replace(parrot_number, PlayerPrefs.GetString(new_number));

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.dataPath + "/info.txt"))
            {
                file.Write(str);
            }
        }

        SceneManager.LoadScene("menu");
    }
}
