using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private GameObject datingPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private AudioSource audioS;

    [SerializeField] private GameObject[] daters;
    private bool[] dated;

    [SerializeField] private Sprite[] datersImages;

    [SerializeField] private Color[] datersColors;

    private int love;
    private int likeliness;
    private int ghosting;

    private float standardSize = 50.54f;
    private float creepySize = 61.1f;
    private Color red = Color.red;

    [SerializeField] private Slider loveSlider;
    [SerializeField] private Slider likelinessSlider;
    [SerializeField] private Slider ghostingSlider;

    [SerializeField] private GameObject datingImage;

    private bool dating;
    private int personDating;

    [SerializeField] private int standardValue = 30;

    [SerializeField] private Sprite up;
    [SerializeField] private Sprite down;

    [SerializeField] private Image loveUpDown;
    [SerializeField] private Image likeUpDown;
    [SerializeField] private Image ghostUpDown;

    [SerializeField] private TextMeshProUGUI leftOption;
    [SerializeField] private TextMeshProUGUI rightOption;
    [SerializeField] private TextMeshProUGUI answer;

    [SerializeField] private Image backgroundImage;

    [SerializeField] TextManager textManager;

    private int iD = 1;

    private bool screwedUp = false;

    public enum dater
    {
        Alex = 0,
        Jacob = 1,
        Emily = 2,
        Olivia = 3,
        Otakon = 4,
        Paula = 5
    }

    private void Start()
    {
        screwedUp = false;
        dated = new bool[daters.Length];
        dating = false;
        for (int i = 0; i < dated.Length; i++) dated[i] = false;
        ChangeToSelection(-1);
    }


    private void Update()
    {
        if (dating)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            datingImage.GetComponent<RectTransform>().position = new Vector3(worldPos.x * 13 + 960, datingImage.transform.position.y, datingImage.transform.position.z);
        }
    }

    private void ChangeToSelection(int person)
    {
        if(person != -1)
        {
            daters[person].gameObject.SetActive(false);
            personDating = -1;
        }
        pausePanel.SetActive(false);
        datingPanel.SetActive(false);
        selectionPanel.SetActive(true);
        // StayPixel says no to this line, pls help
        loveSlider.value = likelinessSlider.value = ghostingSlider.value = love = likeliness = ghosting = standardValue;
        datingImage.SetActive(true);
        answer.fontSize = standardSize;
        iD = 1;
    }

    public void StartDating(int person)
    {
        textManager.LoadAppCSV(person);
        personDating = person;
        datingPanel.SetActive(true);
        selectionPanel.SetActive(false);
        datingImage.GetComponent<Image>().sprite = datersImages[person];
        dating = true;
        backgroundImage.color = datersColors[person];
        NextConvo();
    }

    public void ConversationChoice(bool x)
    {
        if(iD == 1000)
        {
            if(x)
            {
                ChangeToSelection(personDating);
                return;
            }
            else
            {
                gameOverPanel.SetActive(true);
                return;
            }
        }

        if (screwedUp)
        {
            ChangeToSelection(personDating);
            return;
        }

        ChangeParameters(x);

        if(iD != 1000) CheckParameters();
        if (!screwedUp) NextConvo();
    }

    private void NextConvo()
    {
        if(iD == 1000)
        {
            answer.text = "Congratulations, you got a date!";
            leftOption.text = "Stay with this person";
            rightOption.text = "Try someone else";
            return;
        }
        string[] textos = textManager.GetText(iD);
        answer.text = textos[0];
        leftOption.text = textos[1];
        rightOption.text = textos[2];
        
        
    }

    private void ChangeParameters(bool x)
    {
        int[] parametros = textManager.GetParameters(x, iD);

        ghosting += parametros[2]; 
        ghostingSlider.value = ghosting;

        love += parametros[1]; 
        loveSlider.value = love;

        likeliness += parametros[3]; 
        likelinessSlider.value = likeliness;

        iD = parametros[3];

    }

    private void CheckParameters()
    {
        if (ghosting >= 100) Ghosting();
        if (likeliness <= 0) Blocked();
        else if (likeliness >= 100) Friendzoned();
        if (love <= 0) Rejection();
        else if (love >= 100) Yandere();
        
    }

    private void Ghosting()
    {
        screwedUp = true;
        datingImage.SetActive(false);
        answer.text = "Days passed, and there is no answer...";
        leftOption.text = "Hi?";
        rightOption.text = "Is someone there...?";
    }

    private void Rejection()
    {
        screwedUp = true;
        datingImage.SetActive(false);

        answer.text = "I don't think this is gonna work, bye";
        leftOption.text = "Wait but-";
        rightOption.text = "Bye";
    }

    private void Yandere()
    {

        //Dazul does not approve of any of this, please help me I am scared

        screwedUp = true;
        datingImage.SetActive(false);
        answer.fontSize = creepySize;
        answer.color = red;

        audioS.Play();

        answer.text = "IloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyouIloveyou";
        leftOption.text = "Open the door";
        rightOption.text = "Get the gun and hide";
    }

    private void Blocked()
    {
        screwedUp = true;
        datingImage.SetActive(false);

        answer.text = "*you have meen blocked by this user*";
        leftOption.text = "Okay? lmao";
        rightOption.text = "T-T";
    }

    private void Friendzoned()
    {
        screwedUp = true; 
        datingImage.SetActive(false);

        answer.text = "Sorry, I don't think I can see you as more than a friend";
        leftOption.text = "BuT I lOvE YoU";
        rightOption.text = "okay bro";
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);

    }

    public void ShowPause()
    {
        pausePanel.SetActive(true);
    }

    public void HidePause()
    {
        pausePanel.SetActive(false);

    }

}
