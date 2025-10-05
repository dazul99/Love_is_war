using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private GameObject datingPanel;

    [SerializeField] private GameObject[] daters;
    private bool[] dated;

    [SerializeField] private Sprite[] datersImages;

    private int love;
    private int likeliness;
    private int ghosting;


    [SerializeField] private Slider loveSlider;
    [SerializeField] private Slider likelinessSlider;
    [SerializeField] private Slider ghostingSlider;

    [SerializeField] private GameObject datingImage;

    private bool dating;

    [SerializeField] private int standardValue = 30;

    [SerializeField] private Sprite up;
    [SerializeField] private Sprite down;

    [SerializeField] private Image loveUpDown;
    [SerializeField] private Image likeUpDown;
    [SerializeField] private Image ghostUpDown;

    [SerializeField] private TextMeshProUGUI leftOption;
    [SerializeField] private TextMeshProUGUI rightOption;
    [SerializeField] private TextMeshProUGUI answer;


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
        dated = new bool[daters.Length];
        dating = false;
        for (int i = 0; i < dated.Length; i++) dated[i] = false;
        ChangeToSelection();
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

    private void ChangeToSelection()
    {
        datingPanel.SetActive(false);
        selectionPanel.SetActive(true);
        loveSlider.value = likelinessSlider.value = ghostingSlider.value = standardValue;
    }

    public void StartDating(int person)
    {
        datingPanel.SetActive(true);
        selectionPanel.SetActive(false);
        Debug.Log((dater) person);
        datingImage.GetComponent<Image>().sprite = datersImages[person];
        dating = true;
    }

    public void ConversationChoice(bool x)
    {
        Debug.Log(x);


    }

    private void CheckParameters()
    {
        if(ghosting >= 100)
        {

        }
        if(love <= 0)
        {

        }
        else if (love >= 100)
        {

        }
        if(likeliness <= 0)
        {
            
        }
        else if(likeliness >= 100)
        {

        }
    }

}
