using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Globalization;

public class GameScript : MonoBehaviour
{
    public Text CurrentHungry_text;
    public Text CurrentHappy_text;
    public Text CurrentFatigue_text;

    public Text Name;

    public Image CurrentHungry_image;
    public Image CurrentHappy_image;
    public Image CurrentFatigue_image;

    public Image current_parrot;

    float happy;
    float hungry;
    float fatigue;

    string parrot_number;
    string parrot_name;
    string clicked;

    public Button sleep;
    public Button play;
    public Button food;

    public GameObject GameOver;
    public GameObject night;

    void Awake()
    {
        Saves();         
    }

    void Start()
    {
        GameOver.SetActive(false);

        if (clicked == "off")
        {
            night.SetActive(false);

            current_parrot.GetComponent<Image>().sprite = Resources.Load<Sprite>("попуг" + parrot_number);
        }
        else
        {
            night.SetActive(true);

            current_parrot.GetComponent<Image>().sprite = Resources.Load<Sprite>("попуг" + parrot_number + "(sleep)");

            play.GetComponent<Image>().color = Color.gray;
            play.GetComponent<Button>().enabled = false;

            food.GetComponent<Image>().color = Color.gray;
            food.GetComponent<Button>().enabled = false;
        }

        Button btn1 = food.GetComponent<Button>();
        btn1.onClick.AddListener(Feed);

        Button btn2 = play.GetComponent<Button>();
        btn2.onClick.AddListener(Play);

        Button btn3 = sleep.GetComponent<Button>();
        btn3.onClick.AddListener(Sleep);

        UpdateHungry();
        UpdateHappy();
        UpdateFatigue();

        Update();
    }

    void Saves()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/info.txt");

        parrot_number = sr.ReadLine();
        parrot_name = sr.ReadLine();
        Name.text = parrot_name;

        happy = float.Parse(sr.ReadLine(), CultureInfo.InvariantCulture.NumberFormat);
        hungry = float.Parse(sr.ReadLine(), CultureInfo.InvariantCulture.NumberFormat);
        fatigue = float.Parse(sr.ReadLine(), CultureInfo.InvariantCulture.NumberFormat);

        clicked = sr.ReadLine();

        sr.Close();
    }

    void Update()
    {
        if (clicked == "off")
            night.SetActive(false);
        else
            night.SetActive(true);

        happy -= 0.5f * Time.deltaTime;

        if (happy < 0)
            happy = 0;

        UpdateHappy();

        hungry -= 0.5f * Time.deltaTime;

        if (hungry < 0)
            hungry = 0;

        UpdateHungry();

        if (clicked == "off")
        {
            fatigue -= 0.5f * Time.deltaTime;

            if (fatigue < 0)
                fatigue = 0;
        }
        else
        {
            fatigue += 0.5f * Time.deltaTime;

            if (fatigue > 100)
            {
                fatigue = 100;

                play.GetComponent<Image>().color = new Color(236, 0, 0);
                play.GetComponent<Button>().enabled = true;

                food.GetComponent<Image>().color = new Color(236, 230, 0);
                food.GetComponent<Button>().enabled = true;

                current_parrot.GetComponent<Image>().sprite = Resources.Load<Sprite>("попуг" + parrot_number);

                clicked = "off";
            }
        }

        UpdateFatigue();

        EndGame();

        SaveInTxt();
    }

    void SaveInTxt()
    {
        File.WriteAllText(Application.dataPath + "/info.txt", string.Empty);

        StreamWriter sw = new StreamWriter(Application.dataPath + "/info.txt");

        sw.WriteLine(parrot_number);
        sw.WriteLine(parrot_name);
        sw.WriteLine(happy.ToString());
        sw.WriteLine(hungry.ToString());
        sw.WriteLine(fatigue.ToString());
        sw.WriteLine(clicked.ToString());

        sw.Close();
    }

    void UpdateHungry()
    {
        float value = hungry / 100;

        CurrentHungry_image.rectTransform.localScale = new Vector3(value, 1, 1);
        CurrentHungry_text.text = (value * 100).ToString("0") + "%";
    }

    void UpdateHappy()
    {
        float value = happy / 100;

        CurrentHappy_image.rectTransform.localScale = new Vector3(value, 1, 1);
        CurrentHappy_text.text = (value * 100).ToString("0") + "%";
    }

    void UpdateFatigue()
    {
        float value = fatigue / 100;

        CurrentFatigue_image.rectTransform.localScale = new Vector3(value, 1, 1);
        CurrentFatigue_text.text = (value * 100).ToString("0") + "%";
    }

    void Play()
    {
        happy += 10;

        if (happy > 100)
        {
            happy = 100;
        }

        UpdateHappy();
    }

    void Feed()
    {
        hungry += 10;

        if (hungry > 100)
        {
            hungry = 100;
        }

        UpdateHungry();
    }

    void Sleep()
    {
        if (clicked == "off")
        {
            if (fatigue < 100)
            {
                play.GetComponent<Image>().color = Color.gray;
                play.GetComponent<Button>().enabled = false;

                food.GetComponent<Image>().color = Color.gray;
                food.GetComponent<Button>().enabled = false;

                current_parrot.GetComponent<Image>().sprite = Resources.Load<Sprite>("попуг" + parrot_number + "(sleep)");

                clicked = "on";
            }
        }
        else
        {
            play.GetComponent<Image>().color = new Color(236, 0, 0);
            play.GetComponent<Button>().enabled = true;

            food.GetComponent<Image>().color = new Color(236, 230, 0);
            food.GetComponent<Button>().enabled = true;

            current_parrot.GetComponent<Image>().sprite = Resources.Load<Sprite>("попуг" + parrot_number);

            clicked = "off";
        }
    }

    void EndGame()
    {
        if (hungry == 0)
        {
            GameOver.SetActive(true);
        }
        else
        {
            UpdateHappy();
            UpdateHungry();
            UpdateFatigue();
        }
    }
}