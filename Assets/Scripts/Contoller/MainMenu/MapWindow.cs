using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameSetupStats;

public class MapWindow : MonoBehaviour
{
    [Header("Player UI Labels")]
    [SerializeField] private Text Player1Lable;
    [SerializeField] private Text Player2Lable;
    [SerializeField] private Text Player3Lable;
    [SerializeField] private Text Player4Lable;
    [SerializeField] private Text Player5Lable;

    [Header("Player UI InputFields")]
    [SerializeField] private TMP_InputField Player1InputField;
    [SerializeField] private TMP_InputField Player2InputField;
    [SerializeField] private TMP_InputField Player3InputField;
    [SerializeField] private TMP_InputField Player4InputField;
    [SerializeField] private TMP_InputField Player5InputField;

    [Header("Map buttons")]
    [SerializeField] private GameObject Map1Button;
    [SerializeField] private GameObject Map2Button;

    [Header("Buttons")]
    [SerializeField] private GameObject StartButton;

    [Header("Misc")]
    [SerializeField] private int PlayerAmount;
    [SerializeField] public static bool isThereAnythingWrittenInput1;
    [SerializeField] public static bool isThereAnythingWrittenInput2;
    [SerializeField] public static bool isThereAnythingWrittenInput3;
    [SerializeField] public static bool isThereAnythingWrittenInput4;
    [SerializeField] public static bool isThereAnythingWrittenInput5;

    // Start is called before the first frame update
    void Start()
    {
        StartButton.GetComponent<Button>().interactable = false;
        //this should be removed once i make map2
        Map2Button.GetComponent<Button>().interactable = false;
        Setup();
    }
    // Update is called once per frame
    void Update()
    {
        TextCheck();
    }
    private void Setup()
    {
        PlayerAmount = GameSetupStats.GetPlayerAmount();
        switch (PlayerAmount)
        {
            case 1:
                ActivateFields(1);
                break;
            case 2:
                ActivateFields(2);
                break;
            case 3:
                ActivateFields(3);
                break;
            case 4:
                ActivateFields(4);
                break;
            case 5:
                ActivateFields(5);
                break;
        }
    }
    public void SelectMap1()
    {
        GameSetupStats.SetMap(1);
        StartButton.GetComponent<Button>().interactable = true;
    }
    public void SelectMap2()
    {
        GameSetupStats.SetMap(2);
        StartButton.GetComponent<Button>().interactable = true;
    }
    private void ActivateFields(int Amount) 
    {
        switch (Amount)
        {
            case 1:
                Player1Lable.gameObject.SetActive(true);
                Player1InputField.gameObject.SetActive(true);
                break;
            case 2:
                Player1Lable.gameObject.SetActive(true);
                Player1InputField.gameObject.SetActive(true);
                Player2Lable.gameObject.SetActive(true);
                Player2InputField.gameObject.SetActive(true);
                break;
            case 3:
                Player1Lable.gameObject.SetActive(true);
                Player1InputField.gameObject.SetActive(true);
                Player2Lable.gameObject.SetActive(true);
                Player2InputField.gameObject.SetActive(true);
                Player3Lable.gameObject.SetActive(true);
                Player3InputField.gameObject.SetActive(true);
                break;
            case 4:
                Player1Lable.gameObject.SetActive(true);
                Player1InputField.gameObject.SetActive(true);
                Player2Lable.gameObject.SetActive(true);
                Player2InputField.gameObject.SetActive(true);
                Player3Lable.gameObject.SetActive(true);
                Player3InputField.gameObject.SetActive(true);
                Player4Lable.gameObject.SetActive(true);
                Player4InputField.gameObject.SetActive(true);
                break;
            case 5:
                Player1Lable.gameObject.SetActive(true);
                Player1InputField.gameObject.SetActive(true);
                Player2Lable.gameObject.SetActive(true);
                Player2InputField.gameObject.SetActive(true);
                Player3Lable.gameObject.SetActive(true);
                Player3InputField.gameObject.SetActive(true);
                Player4Lable.gameObject.SetActive(true);
                Player4InputField.gameObject.SetActive(true);
                Player5Lable.gameObject.SetActive(true);
                Player5InputField.gameObject.SetActive(true);
                break;
        }
    }
    private List<Player> SavePlayers()
    {
        List<Player> ReturnList = new List<Player>(); 
        for (int i = 1; i < (PlayerAmount + 1); i++)
        {
            Player player = new Player();
            player.playerid = i;
            switch (i)
            {
                case 1:
                    if (isThereAnythingWrittenInput1)
                    {
                        player.playername = Player1InputField.text;
                    }
                    else
                    {
                        player.playername = "player" + i.ToString();
                    }
                    break;
                case 2:
                    if (isThereAnythingWrittenInput2)
                    {
                        player.playername = Player2InputField.text;
                    }
                    else
                    {
                        player.playername = "player" + i.ToString();
                    }
                    break;
                case 3:
                    if (isThereAnythingWrittenInput3)
                    {
                        player.playername = Player3InputField.text;
                    }
                    else
                    {
                        player.playername = "player" + i.ToString();
                    }
                    break;
                case 4:
                    if (isThereAnythingWrittenInput4)
                    {
                        player.playername = Player4InputField.text;
                    }
                    else
                    {
                        player.playername = "player" + i.ToString();
                    }
                    break;
                case 5:
                    if (isThereAnythingWrittenInput5)
                    {
                        player.playername = Player5InputField.text;
                    }
                    else
                    {
                        player.playername = "player" + i.ToString();
                    }
                    break;
            }
            player.points = 0;
            player.debuffs = new List<Debuff>();
            player.powerups = new List<PowerUP>();
            ReturnList.Add(player);
        }
        return ReturnList;
    }
    private void TextCheck()
    {
        isThereAnythingWrittenInput1 = !(Player1InputField.text.Length < 1);
        isThereAnythingWrittenInput2 = !(Player2InputField.text.Length < 1);
        isThereAnythingWrittenInput3 = !(Player3InputField.text.Length < 1);
        isThereAnythingWrittenInput4 = !(Player4InputField.text.Length < 1);
        isThereAnythingWrittenInput5 = !(Player5InputField.text.Length < 1);
    }
    private Loader.Scene ConvertMapToScene()
    {
        Loader.Scene Scene = new Loader.Scene();
        switch (GameSetupStats.GetSelectedMap())
        {
            case GameSetupStats.Map.Map1:
                Scene = Loader.Scene.Map1;
                break;
            case GameSetupStats.Map.Map2:
                break;
        }
        return Scene;
    }
    public void StartGame()
    {
        GameSetupStats.SetplayerList(SavePlayers());
        Loader.Load(ConvertMapToScene());
    }
}
