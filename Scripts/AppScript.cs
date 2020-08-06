using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO; 

public class AppScript : MonoBehaviour
{
    public void OnClickBack()
    {
         if(System.IO.File.ReadAllLines(Application.dataPath + "/info.txt").Length == 1)
            File.WriteAllText(Application.dataPath + "/info.txt", string.Empty);
          
        SceneManager.LoadScene("menu");
    }
}
