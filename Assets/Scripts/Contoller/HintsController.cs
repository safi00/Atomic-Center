using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Random = UnityEngine.Random;

public class HintsController : MonoBehaviour
{
    [Header("Big UIs")]
    [SerializeField] public GameObject DisplayQuestionUI;
    [SerializeField] public GameObject DisplayPowerUpUI;

    [Header("Questions")]
    [SerializeField] public TMP_Text QuestionText;
    [SerializeField] public TMP_Text AnswerA;
    [SerializeField] public TMP_Text AnswerB;
    [SerializeField] public TMP_Text AnswerC;
    [SerializeField] public TMP_Text AnswerD;

    [Header("cached quesyion")]
    [SerializeField] public Question currentQuestion;
    [SerializeField] public int questionIndex;

    [Header("Misc")]
    [SerializeField] public static bool GameIsinQuestion = false;
    [SerializeField] public bool questionIsAnswered;
    [SerializeField] public bool wrongAnswer;
    [SerializeField] public GameObject hurtScript;
    [SerializeField] public GameObject wrongAnswerPanel;

    // Start is called before the first frame update
    void Start()
    {
        DebuggingCode(getQuestionList());
    }
    // Update is called once per frame
    void Update()
    {
        if (wrongAnswer)
        {
            if (Input.anyKeyDown)
            {
                closeQuestionUI();
                wrongAnswer = !wrongAnswer;
            }
        }
    }
    private void DebuggingCode(List<Data> Data) 
    {
        Debug.Log(Data[0].Difficultytype);
        Debug.Log(Data[0].Elementlist[0].Elementcode);
    }
    /// <summary>
    /// This method prepares the questions and answer and qaches the question.
    /// </summary>
    /*
    private void FillinQuestions()
    {
        List<Data> Questionlist = getQuestionList();
        questionIndex = Random.Range(0,Questionlist.Count);
        Data randomQuestion = Questionlist[questionIndex];
        currentQuestion = randomQuestion;
        QuestionText.text = randomQuestion.questionid + "# Question " + randomQuestion.questionid +": " + Environment.NewLine + randomQuestion.question;
        List<string> randomAnswers = randomizeAnswers(randomQuestion.falseAnswers, randomQuestion.answer);
        AnswerA.text = randomAnswers[0];
        AnswerB.text = randomAnswers[1];
        AnswerC.text = randomAnswers[2];
        AnswerD.text = randomAnswers[3];
        questionIsAnswered = false;
        wrongAnswer = false;
    }
    */

    /// <summary>
    /// This method prepares the answers and grabs a random index between 0,1 and 2 & 
    /// adds it again and then change that specific indexso its always random and not the same button as the right answer.
    /// </summary>
    private List<string> randomizeAnswers(List<string> falseAnswers, string rightAnswer)
    {
        List<string> answers = new List<string>();
        int randomPlacement = Random.Range(0, 3);
        for (int i = 0; i < 3; i++)
        {
            answers.Add(falseAnswers[i]);
        }
        answers.Add(falseAnswers[randomPlacement]);
        answers[randomPlacement] = rightAnswer;
        return answers;
    }

    /// <summary>
    /// This method checks if the buttons answer was right
    /// </summary>
    private bool checkAnswers(string playerAnswer)
    {
        bool answer = false;
        if (currentQuestion.answer == playerAnswer)
        {
            answer = true;
        }
        return answer;
    }
    private List<Data> getQuestionList()
    {
        string jsontxt = File.ReadAllText(Application.dataPath + "/Resources/Hints.json");
        List<Data> questionList = JsonConvert.DeserializeObject<List<Data>>(jsontxt);
        return questionList;
    }

    /// <summary>
    /// These methods checks runs the check method, i made 4 different ones for clarity
    /// </summary>
    public void AnswerAButton()
    {
        if (!questionIsAnswered)
        {
            if (checkAnswers(AnswerA.text))
            {
                DisplayQuestionUI.SetActive(false);
                DisplayPowerUpUI.SetActive(true);
            }
            else
            {
                WrongAnswer(); 
            }
        }
        questionIsAnswered = true;
    }
    public void AnswerBButton()
    {
        if (!questionIsAnswered)
        {
            if (checkAnswers(AnswerB.text))
            {
                DisplayQuestionUI.SetActive(false);
                DisplayPowerUpUI.SetActive(true);
            }
            else
            {
                WrongAnswer();
            }
        }
        questionIsAnswered = true;
    }
    public void AnswerCButton()
    {
        if (!questionIsAnswered)
        {
            if (checkAnswers(AnswerC.text))
            {
                DisplayQuestionUI.SetActive(false);
                DisplayPowerUpUI.SetActive(true);
            }
            else
            {
                WrongAnswer();
            }
        }
        questionIsAnswered = true;
    }
    public void AnswerDButton()
    {
        if (!questionIsAnswered)
        {
            if (checkAnswers(AnswerD.text))
            {
                DisplayQuestionUI.SetActive(false);
                DisplayPowerUpUI.SetActive(true);
            }
            else
            {
                WrongAnswer();
            }
        }
        questionIsAnswered = true;
    }
    /// <summary>
    /// This method get called whenever the Question Coin is triggered, it fills in the questions and answers and stops time
    /// </summary>
    public void QuetionPOP()
    {
        DisplayQuestionUI.SetActive(true);
        //FillinQuestions();
        pauseGame();
    }
    /// <summary>
    /// This method shows the right annswer after the player got it wrong andgives feedback on what to do next
    /// </summary>
    public void WrongAnswer() 
    {
        wrongAnswerPanel.SetActive(true);
        AnswerA.text = currentQuestion.answer;
        AnswerB.text = "you got the answer wrong";
        AnswerC.text = "you lose a heart";
        AnswerD.text = "press any key to continue";
        wrongAnswer = true;
    }
    public void closeQuestionUI()
    {
        wrongAnswerPanel.SetActive(false);
        DisplayQuestionUI.SetActive(false);
        DisplayPowerUpUI.SetActive(false);
        ResumeGame();

        hurtPlayer();
    }
    private void hurtPlayer()
    {
        IEvent events = hurtScript.GetComponent<IEvent>();
        if (events != null)
        {
            events.playEvent("HURT");
        }
    }
    public void closePowerUpUI()
    {
        DisplayPowerUpUI.SetActive(false);
        ResumeGame();
    }
    private void pauseGame()
    {
        GameIsinQuestion = true;
        Time.timeScale = 0f;
    }
    private void ResumeGame()
    {
        GameIsinQuestion = false;
        Time.timeScale = 1f;
    }
    /// <summary>
    /// This methods are here to subcribe to events
    /// so when a Qcoin gets collected all the other scripts know to tun a method
    /// </summary>
    private void OnEnable()
    {
        //PowerUPController.OnQCoinsCollectable += QuetionPOP;
        Speed.OnSpeedGained += closePowerUpUI;
        Points.OnPointsGained += closePowerUpUI;
        Heart.OnHeartGained += closePowerUpUI;
    }
    private void OnDisable()
    {
        //PowerUPController.OnQCoinsCollectable -= QuetionPOP;
        Speed.OnSpeedGained -= closePowerUpUI;
        Points.OnPointsGained -= closePowerUpUI;
        Heart.OnHeartGained -= closePowerUpUI;
    }
    public class Question
    {
        public int questionid { get; set; }
        public string answertype { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public List<string> falseAnswers { get; set; }
    }
    //Variable names should awlays match the json and its case senstive
    [System.Serializable]
    public class Data
    {
        [JsonProperty("difficultyid")]   private int difficultyid;
        [JsonProperty("difficultytype")] private string difficultytype;
        [JsonProperty("elementlist")]    private List<Elementlist> elementlist;
        public int Difficultyid
        {
            get { return difficultyid; }
        }
        public string Difficultytype
        {
            get { return difficultytype; }
        }
        public List<Elementlist> Elementlist
        {
            get { return elementlist; }
        }
    }
    [System.Serializable]
    public class Elementlist
    {
        [JsonProperty("elementid")]    private int elementid;
        [JsonProperty("elementname")]  private string elementname;
        [JsonProperty("elementcode")]  private string elementcode;
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
        public List<string> ElementhintsList
        {
            get { return elementhintsList; }
        }
    }
}
