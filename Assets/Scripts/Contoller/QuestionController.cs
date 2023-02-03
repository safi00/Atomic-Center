using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuestionController : MonoBehaviour
{
    [Header("Big UIs")]
    [SerializeField] public GameObject VisualsUI;
    [SerializeField] public GameObject DisplayQuestionUI;
    [SerializeField] public GameObject QuestionEventUI;

    [SerializeField] private GameObject EndUI;
    [SerializeField] private GameObject EndText;
    [SerializeField] private GameObject EndClickText;
    [SerializeField] private GameObject EndButton;

    [Header("Question 'n Answers")]
    [SerializeField] public Text QuestionText;
    [SerializeField] public Text AnswerA;
    [SerializeField] public Text AnswerB;
    [SerializeField] public Text AnswerC;
    [SerializeField] public Question currentQuestion;
    [SerializeField] public int questionIndex;

    [Header("Event")]
    [SerializeField] public GameObject EndTurnEvent;
    [SerializeField] public GameObject PointUPEvent;

    [Header("Player Stats")]
    [SerializeField] public PlayerMovement CurrentGuessingPlayer;

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
    /// <summary>
    /// This method prepares the questions and answer and qaches the question.
    /// </summary>
    private void FillinQuestions()
    {
        List<Question> Questionlist = getQuestionList();
        questionIndex = Random.Range(0, Questionlist.Count);
        Question randomQuestion = Questionlist[questionIndex];
        currentQuestion = randomQuestion;

        QuestionText.text = randomQuestion.Questiontext;
        List<string> randomAnswers = randomizeAnswers(randomQuestion.Answerlist);
        AnswerA.text = randomAnswers[0];
        AnswerB.text = randomAnswers[1];
        AnswerC.text = randomAnswers[2];
    }

    /// <summary>
    /// This method prepares the answers and grabs a random index between 0,1 and 2 & 
    /// adds it again and then change that specific indexso its always random and not the same button as the right answer.
    /// </summary>
    private List<string> randomizeAnswers(List<string> falseAnswers)
    {
        List<string> RandomizedAnswers = new List<string>();
        int ListSize = falseAnswers.Count;
        for (int i = 0; i < ListSize; i++)
        {
            RandomizedAnswers.Add(falseAnswers[i]);
        }
        while (ListSize > 1)
        {
            ListSize--;
            int RandomInt = Random.Range(0, ListSize+1);
            string value = RandomizedAnswers[RandomInt];
            RandomizedAnswers[RandomInt] = RandomizedAnswers[ListSize];
            RandomizedAnswers[ListSize] = value;
        }
        return RandomizedAnswers;
    }

    /// <summary>
    /// This method checks if the buttons answer was right
    /// </summary>
    private bool checkAnswers(string playerAnswer)
    {
        bool answer = false;
        if (currentQuestion.Correctanswer == playerAnswer)
        {
            answer = true;
        }
        return answer;
    }
    private List<Question> getQuestionList()
    {
        string jsontxt = File.ReadAllText(Application.dataPath + "/Resources/questions.json");
        List<Question> questionList = JsonConvert.DeserializeObject<List<Question>>(jsontxt);
        return questionList;
    }

    /// <summary>
    /// These methods checks runs the check method, i made 4 different ones for clarity
    /// </summary>
    public void AnswerAButton()
    {
        if (checkAnswers(AnswerA.text))
        {
            GoodAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }
    public void AnswerBButton()
    {
        if (checkAnswers(AnswerB.text))
        {
            GoodAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }
    public void AnswerCButton()
    {
        if (checkAnswers(AnswerC.text))
        {
            GoodAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }
    /// <summary>
    /// This method get called whenever the Question Coin is triggered, it fills in the questions and answers and stops time
    /// </summary>
    public void QuetionPOP()
    {
        DisplayQuestionUI.SetActive(true);
        QuestionEventUI.SetActive(true);
        OpenQuestEventUI();
        FillinQuestions();
    }
    /// <summary>
    /// This method shows the right annswer after the player got it wrong andgives feedback on what to do next
    /// </summary>
    public void GoodAnswer()
    {
        PointUpEvent();
        PointUpEvent();
        TurnOffUI();
        TurnOnTurnUI("Good Answer! +2 Points.");
    }
    public void WrongAnswer()
    {
        CurrentGuessingPlayer.PlayerScore--;
        TurnOffUI();
        TurnOnTurnUI("Wrong Guess! you lose a point.");
    }
    /// <summary>
    /// This method are event methods they make an invoke call and they trigger methods all over the project
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
        DisplayQuestionUI.SetActive(false);
        EndUI.SetActive(false);
        EndText.SetActive(false);
        EndButton.SetActive(false);
        EndClickText.SetActive(false);

        TurnEndEvent();
    }
    public void TurnOnTurnUI(string Text)
    {
        VisualsUI.SetActive(true);
        DisplayQuestionUI.SetActive(true);
        EndUI.SetActive(true);
        EndText.SetActive(true);
        EndText.transform.GetComponent<Text>().text = Text;
        EndClickText.SetActive(true);
        EndButton.SetActive(true);
    }
    public void OpenQuestEventUI()
    {
        VisualsUI.SetActive(true);
        DisplayQuestionUI.SetActive(true);
        QuestionEventUI.SetActive(true);
    }
    public void TurnOffUI()
    {
        VisualsUI.SetActive(false);
        DisplayQuestionUI.SetActive(false);
        QuestionEventUI.SetActive(false);
    }
    [System.Serializable]
    public class Question
    {
        [JsonProperty("questionid")] private int questionid;
        [JsonProperty("questiontext")] private string questiontext;
        [JsonProperty("correctanswer")] private string correctanswer;
        [JsonProperty("answerlist")] private List<string> answerlist;
        public int Questionid
        {
            get { return questionid; }
        }
        public string Questiontext
        {
            get { return questiontext; }
        }
        public string Correctanswer
        {
            get { return correctanswer; }
        }
        public List<string> Answerlist
        {
            get { return answerlist; }
        }
    }
}