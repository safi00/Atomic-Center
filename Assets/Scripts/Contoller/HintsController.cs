using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static GameSetupStats;
using Random = UnityEngine.Random;

public class HintsController : MonoBehaviour
{
    [Header("Big UIs")]
    [SerializeField] public GameObject VisualsUI;
    [SerializeField] public GameObject GuessUI;
    [SerializeField] public GameObject HintsUI;
    [SerializeField] public GameObject PeriodicUI;

    [SerializeField] public Text Hint1;
    [SerializeField] public Text Hint2;
    [SerializeField] public Text Hint3;

    [SerializeField] private GameObject EndUI;
    [SerializeField] private GameObject EndText;
    [SerializeField] private GameObject EndClickText;
    [SerializeField] private GameObject EndButton;

    [Header("cached Elementlist")]
    [HideInInspector] public Data CachedData;
    [HideInInspector] public string CachedDataDiffuculty;
    [HideInInspector] public List<Element> CachedDataElementList;

    [Header("Random Element")]
    [SerializeField] public Element GuessElement;
    [SerializeField] public List<string> GuessElementHintsList;

    [Header("Setup Stats")]
    [SerializeField] public Difficulty ChosenDiffuculty;

    [Header("Player Stats")]
    [SerializeField] public PlayerMovement CurrentGuessingPlayer;
    [SerializeField] public static int ElementIndex;
    [SerializeField] public static int AmountofHints;

    [Header("Event")]
    [SerializeField] public GameObject EndTurnEvent;
    [SerializeField] public GameObject PointUPEvent;

    [Header("Misc")]
    [SerializeField] public bool isElementGuessed;
    [SerializeField] public bool wrongAnswer;
    [HideInInspector] public static int? MyChosenElement;
    [HideInInspector] public static bool didHintsSetup;

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (!didHintsSetup)
        {
            Setup();
        }
        if (MyChosenElement != null)
        {
            Debug.Log(MyChosenElement);
            if (CheckAnswers(MyChosenElement))
            {
                GoodAnswer();
            }
            else
            {
                WrongAnswer();
            }
            MyChosenElement = null;
        }
    }
    private void Setup()
    {
        AddElementList(GetHintsList());
        isElementGuessed = false;
    }
    /// <summary>
    ///  This method prepares the answers and grabs a random index between 0,1 and 2 & 
    /// adds it again and then change that specific indexso its always random and not the same button as the right answer.
    /// </summary>
    private List<Data> GetHintsList()
    {
        string jsontxt = File.ReadAllText(Application.dataPath + "/Resources/Hints.json");
        List<Data> questionList = JsonConvert.DeserializeObject<List<Data>>(jsontxt);
        return questionList;
    }
    /// <summary>
    /// This method prepares the answers and grabs a random index between 0,1 and 2 & 
    /// adds it again and then change that specific indexso its always random and not the same button as the right answer.
    /// </summary>
    private void AddElementList(List<Data> Datalist)
    {
        int DatalistAmount = Datalist.Count;
        if (DatalistAmount > 0)
        {
            for (int i = 0; i < DatalistAmount; i++)
            {
                if (Datalist[i].Difficultytype == ChosenDiffucultyConverter(ChosenDiffuculty))
                {
                    CachedData = Datalist[i];
                    CachedDataDiffuculty = CachedData.Difficultytype;
                    CachedDataElementList = CachedData.Elementlist;
                }
            }
        }
    }
    /// <summary>
    /// This method prepares the questions and answer and qaches the question.
    /// </summary>
    private void FillInUI()
    {
        CurrentPlayerData();
        if (ElementIndex <= -1)
        {
            ElementIndex = Random.Range(0, CachedDataElementList.Count);
            Element RandomElement = CachedDataElementList[ElementIndex];
            GuessElement = RandomElement;
            CurrentGuessingPlayer.CurrentElementIndex = ElementIndex;
        }
        wrongAnswer = false;
        UpdateElements(ElementIndex);
    }

    /// <summary>
    /// This method prepares the questions and answer and qaches the question.
    /// </summary>
    private void CurrentPlayerData()
    {
        ElementIndex = CurrentGuessingPlayer.CurrentElementIndex;
        AmountofHints= CurrentGuessingPlayer.AmountofHints;
    }

    /// <summary>
    /// This method checks if the buttons answer was right
    /// </summary>
    private void UpdateElements(int LastElementIndex)
    {
        Element RandomElement = CachedDataElementList[LastElementIndex];
        GuessElement = RandomElement;
        switch (AmountofHints)
        {
            case < 1:
                CurrentGuessingPlayer.AmountofHints++;
                break;
            case > 3:
                CurrentGuessingPlayer.AmountofHints = 3;
                break;
        }
        switch (AmountofHints)
        {
            case <= 1:
                Hint1.text = RandomElement.ElementhintsList[0];
                Hint2.text = "-";
                Hint3.text = "-";
                break;
            case 2:
                Hint1.text = RandomElement.ElementhintsList[0];
                Hint2.text = RandomElement.ElementhintsList[1];
                Hint3.text = "-";
                break;
            case >=3:
                Hint1.text = RandomElement.ElementhintsList[0];
                Hint2.text = RandomElement.ElementhintsList[1];
                Hint3.text = RandomElement.ElementhintsList[2];
                break;
        }
    }
    /// <summary>
    /// This method checks if the buttons answer was right
    /// </summary>
    private bool CheckAnswers(int? playerAnswer)
    {
        bool answer = false;
        if (GuessElement.Elementnnumber == playerAnswer)
        {
            answer = true;
        }
        return answer;
    }

    /// <summary>
    /// This method get called whenever the Question Coin is triggered, it fills in the questions and answers and stops time
    /// </summary>
    public void HintPOP()
    {
        FillInUI();
        OpenHintsUI();
    }
    /// <summary>
    /// This method shows that the player chose right annswer and gives feedback on what to do next
    /// </summary>
    public void GoodAnswer()
    {
        PointUpEvent();
        CurrentGuessingPlayer.CurrentElementIndex = -1;
        CurrentGuessingPlayer.AmountofHints = 0;
        TurnOffUI();
        TurnOnTurnUI("Good Answer");
    }
    /// <summary>
    /// This method shows that the player chose bad annswer and gives feedback on what to do next
    /// </summary>
    public void WrongAnswer()
    {
        CurrentGuessingPlayer.AmountofHints++;
        TurnOffUI();
        TurnOnTurnUI("Wrong Guess, Next Try will give you an hint");
    }
    /// <summary>
    /// These methods are events so they trigger all over the project
    /// </summary>
    private void PointUpEvent()
    {
        IEvent events = PointUPEvent.GetComponent<IEvent>();
        if (events != null)
        {
            events.playEvent("PointUP");
        }
    }
    private void TurnEndEvent()
    {
        IEvent events = EndTurnEvent.GetComponent<IEvent>();
        if (events != null)
        {
            events.playEvent("EndTurn");
        }
    }

    /// <summary>
    /// These methods are for ui
    /// </summary>
    public void TurnOffVisuals()
    {
        VisualsUI.SetActive(false);
        GuessUI.SetActive(false);
        EndUI.SetActive(false);
        EndText.SetActive(false);
        EndButton.SetActive(false);
        EndClickText.SetActive(false);

        TurnEndEvent();
    }
    public void TurnOnTurnUI(string Text)
    {
        VisualsUI.SetActive(true);
        GuessUI.SetActive(true);
        EndUI.SetActive(true);
        EndText.SetActive(true);
        EndText.transform.GetComponent<Text>().text = Text;
        EndClickText.SetActive(true);
        EndButton.SetActive(true);
    }
    private string ChosenDiffucultyConverter(Difficulty chosenDiffuculty)
    {
        string ReturnDiffuculty = "";
        switch (chosenDiffuculty)
        {
            case Difficulty.Easy:
                ReturnDiffuculty = "Easy";
                break;
            case Difficulty.Hard:
                ReturnDiffuculty = "Hard";
                break;
        }
        return ReturnDiffuculty;
    }
    public void OpenHintsUI()
    {
        VisualsUI.SetActive(true);
        GuessUI.SetActive(true);
        HintsUI.SetActive(true);
        PeriodicUI.SetActive(false);
    }
    public void OpenPeriodicUI()
    {
        HintsUI.SetActive(false);
        PeriodicUI.SetActive(true);
    }
    public void TurnOffUI()
    {
        VisualsUI.SetActive(false);
        GuessUI.SetActive(false);
        HintsUI.SetActive(false);
        PeriodicUI.SetActive(false);
    }
    public void ContinueGameUI()
    {
        CurrentGuessingPlayer.AmountofHints++;
        VisualsUI.SetActive(false);
        GuessUI.SetActive(false);
        HintsUI.SetActive(false);
        PeriodicUI.SetActive(false);
        TurnOffVisuals();
    }

    /// <summary>
    /// Variable names should awlays match the json and its case senstive So i left these lowercase
    /// </summary>
    [System.Serializable]
    public class Data
    {
        [JsonProperty("difficultyid")] private int difficultyid;
        [JsonProperty("difficultytype")] private string difficultytype;
        [JsonProperty("elementlist")] private List<Element> elementlist;
        public int Difficultyid
        {
            get { return difficultyid; }
        }
        public string Difficultytype
        {
            get { return difficultytype; }
        }
        public List<Element> Elementlist
        {
            get { return elementlist; }
        }
    }
    [System.Serializable]
    public class Element
    {
        [JsonProperty("elementid")] private int elementid;
        [JsonProperty("elementname")] private string elementname;
        [JsonProperty("elementcode")] private string elementcode;
        [JsonProperty("elementnnumber")] private int elementnnumber;
        [JsonProperty("elementhints")] private List<string> elementhintsList;
        public int Elementid
        {
            get { return elementid; }
        }
        public string Elementname
        {
            get { return elementname; }
        }
        public string Elementcode
        {
            get { return elementcode; }
        }
        public int Elementnnumber
        {
            get { return elementnnumber; }
        }
        public List<string> ElementhintsList
        {
            get { return elementhintsList; }
        }
    }
}
