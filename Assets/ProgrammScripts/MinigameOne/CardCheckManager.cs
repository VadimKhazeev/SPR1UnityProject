using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCheckManager : MonoBehaviour
{
    [SerializeField]
    private List<ItemSlot> itemSlots;
    [SerializeField]
    private Button checkButton;
    [SerializeField]
    private GameObject objectToDisable;
    [SerializeField]
    private Text ButtonText;
    [SerializeField]
    private string ButtonOnEndText;

    private void Start()
    {
        checkButton.onClick.AddListener(CheckAnswers);
    }

    private void CheckAnswers()
    {
        bool allCorrect = true;

        foreach (ItemSlot slot in itemSlots)
        {
            bool isSlotCorrect = slot.CheckAnswer();
            slot.HandleObjectsActivation();

            if (!isSlotCorrect)
            {
                allCorrect = false;
            }
        }

        if (allCorrect)
        {
            OnCorrectAnswers();
        }
    }

    private void OnCorrectAnswers()
    {
        ButtonText.text = ButtonOnEndText;

        checkButton.onClick.RemoveListener(CheckAnswers);
        checkButton.onClick.AddListener(EndGame);
    }

    private void EndGame()
    {
        objectToDisable.SetActive(false);
    }
}


