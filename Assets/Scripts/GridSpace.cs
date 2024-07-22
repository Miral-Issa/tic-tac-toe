using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    public Button button;
    public Text buttonText;

    private GameController gameController;

    public void SetSpace()
    {
        buttonText.text = gameController.GetPlayerSide();
        button.interactable = false;
        gameController.EndTurn();
    }

    public void SetGameControllerRefrence(GameController controller)
    {
        gameController = controller;
    }
    public void DisableButton()
    {
        button.interactable = false;
    }

    public void EnableButton()
    {
        button.interactable = true;
    }

}
