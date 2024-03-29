using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Customwindow : MonoBehaviour
{
    [Header("UI Window")]
    [SerializeField] private GameObject CustomWindow;

    [Header("Text essentials")]
    [SerializeField] private Text InfoText;
    [SerializeField] private TMP_Text PlaceholderText;
    [SerializeField] private TMP_InputField InputField;

    [Header("Buttons")]
    [SerializeField] private GameObject ExitButton;
    [SerializeField] private GameObject MinusButton;
    [SerializeField] private GameObject PlusButton;
    [SerializeField] private GameObject SaveButton;

    [Header("Misc")]
    [SerializeField] public static int SetupForResuseTurnPoint;
    [SerializeField] public static bool isThereSetup;
    [SerializeField] public static bool isThereAnythingWritten;

    // Start is called before the first frame update
    void Start()
    {
        isThereSetup = false;
        Setup();
    }
    void Update()
    {
        Setup();
        TextCheck();
    }

    /// <summary>
    /// These methods are for ui
    /// </summary>
    public void ValueUp()
    {
        if (!isThereAnythingWritten)
        {
            if (SetupForResuseTurnPoint == 1)
            {
                switch (GameSetupStats.GetTurnLimit())
                {
                    case < 5:
                        GameSetupStats.SetTurnLimit(5);
                        PlaceholderText.text = "5";
                        break;
                    case < 100:
                        GameSetupStats.SetTurnLimit((GameSetupStats.GetTurnLimit() + 1));
                        PlaceholderText.text = "" + GameSetupStats.GetTurnLimit();
                        break;
                    case > 99:
                        GameSetupStats.SetTurnLimit(99);
                        PlaceholderText.text = "99";
                        break;
                }
            }
            else
            {
                switch (GameSetupStats.GetPointLimit())
                {
                    case < 5:
                        GameSetupStats.SetTurnLimit(5);
                        PlaceholderText.text = "5";
                        break;
                    case < 100:
                        GameSetupStats.SetPointLimit((GameSetupStats.GetPointLimit() + 1));
                        PlaceholderText.text = "" + GameSetupStats.GetPointLimit();
                        break;
                    case > 99:
                        GameSetupStats.SetPointLimit(99);
                        PlaceholderText.text = "99";
                        break;
                }
            }
        }
    }
    public void ValueDown()
    {
        if (!isThereAnythingWritten)
        {
            if (SetupForResuseTurnPoint == 1)
            {
                switch (GameSetupStats.GetTurnLimit())
                {
                    case < 5:
                        GameSetupStats.SetTurnLimit(5);
                        PlaceholderText.text = "5";
                        break;
                    case < 101:
                        GameSetupStats.SetTurnLimit((GameSetupStats.GetTurnLimit() - 1));
                        PlaceholderText.text = "" + GameSetupStats.GetTurnLimit();
                        break;
                }
            }
            else
            {
                switch (GameSetupStats.GetPointLimit())
                {
                    case < 5:
                        GameSetupStats.SetPointLimit(5);
                        PlaceholderText.text = "5";
                        break;
                    case < 101:
                        GameSetupStats.SetPointLimit((GameSetupStats.GetPointLimit() - 1));
                        PlaceholderText.text = "" + GameSetupStats.GetPointLimit();
                        break;
                }
            }
        }
    }
    /// <summary>
    /// These methods are for ui
    /// </summary>
    private void TextCheck()
    {
        bool TurnOffButtons = false;
        isThereAnythingWritten = !(InputField.text.Length < 1);
        switch (isThereAnythingWritten)
        {
            case true:
                TurnOffButtons = true;
                break;
            case false:
                TurnOffButtons = false;
                break;
        }
        if (TurnOffButtons)
        {
            MinusButton.GetComponent<Button>().interactable = false;
            PlusButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            MinusButton.GetComponent<Button>().interactable = true;
            PlusButton.GetComponent<Button>().interactable = true;
        }
    }
    public static void SetupTheCustom(int Number)
    {
        SetupForResuseTurnPoint = Number;
    }

    /// <summary>
    /// This method saves the input to start a game
    /// </summary>
    private void SaveInput()
    {
        int InputInt;
        bool didItparse = int.TryParse(InputField.text, out InputInt);
        if (didItparse)
        {
            if (SetupForResuseTurnPoint == 1 && isThereAnythingWritten)
            {
                switch (InputInt)
                {
                    case < 5:
                        GameSetupStats.SetTurnLimit(5);
                        break;
                    case < 100:
                        GameSetupStats.SetTurnLimit(InputInt);
                        break;
                    case > 98:
                        GameSetupStats.SetTurnLimit(99);
                        break;
                }
            }
            else
            {
                switch (InputInt)
                {
                    case < 5:
                        GameSetupStats.SetPointLimit(5);
                        break;
                    case < 100:
                        GameSetupStats.SetPointLimit(InputInt);
                        break;
                    case > 98:
                        GameSetupStats.SetPointLimit(99);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// These methods are for ui
    /// </summary>
    public void Exit()
    {
        SaveInput();
        InputField.Select();
        InputField.text = "";
        isThereSetup = false;
        CustomWindow.SetActive(false);        
    }

    /// <summary>
    /// This method Sets everything in line for the controller to start working
    /// </summary>
    private void Setup()
    {
        if (!isThereSetup)
        {
            string InfoTextString = "Fill in the number between 0 - 99\r\neither keyboard or buttons\r\n";
            if (SetupForResuseTurnPoint == 1)
            {
                InfoTextString = InfoTextString + "Turn";
                switch (GameSetupStats.GetTurnLimit())
                {
                    case < 1:
                        PlaceholderText.text = "-";
                        break;
                    case < 99:
                        PlaceholderText.text = "" + GameSetupStats.GetTurnLimit();
                        ExitButton.GetComponent<Button>().interactable = true;
                        SaveButton.GetComponent<Button>().interactable = true;
                        break;
                    case > 99:
                        PlaceholderText.text = "No Limit";
                        break;
                }
            }
            else
            {
                InfoTextString = InfoTextString + "Point";
                switch (GameSetupStats.GetPointLimit())
                {
                    case < 1:
                        PlaceholderText.text = "-";
                        break;
                    case < 99:
                        PlaceholderText.text = "" + GameSetupStats.GetPointLimit();
                        ExitButton.GetComponent<Button>().interactable = true;
                        SaveButton.GetComponent<Button>().interactable = true;
                        break;
                    case > 99:
                        PlaceholderText.text = "No Limit";
                        break;
                }
            }
            isThereSetup = true;
            InfoText.text = InfoTextString;
        }
    }
}
