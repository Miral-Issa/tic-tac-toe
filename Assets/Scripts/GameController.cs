using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
    public Button button;
}

[System.Serializable]
public class PlayerColor
{
    public Color panelCoolor;
    public Color textColor;
}

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    Dictionary<char, int[]> Xhistory = new Dictionary<char, int[]>();
    Dictionary<char, int[]> Ohistory = new Dictionary<char, int[]>();

    private string playerSide;

    public GameObject gameOverPanel;
    public Text gameOverText;

    private int moveCount;

    public GameObject restartButton;

    public Player playerX;
    public Player playerO;

    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;

    void Awake()
    {
        gameOverPanel.SetActive(false);
        SetGameControllerRefrenceOnButtos();
        moveCount = 0;
        restartButton.SetActive(false);
    }

    public void SetGameControllerRefrenceOnButtos()
    {
        for(int i=0; i<buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerRefrence(this);
        }
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn()
    {
        moveCount++;
        //check winning condition
        //brut force method for now
        if(buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide) // top row
        {
            GameOver(playerSide);
        }else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide) // second row
        {
            GameOver(playerSide);
        }
        else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide) //bottom row
        {
            GameOver(playerSide);
        }
        else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide) //first column
        {
            GameOver(playerSide);
        }
        else if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide) //middle column
        {
            GameOver(playerSide);
        }
        else if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide) // third column
        {
            GameOver(playerSide);
        }
        else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide) // diagonal 1
        {
            GameOver(playerSide);
        }
        else if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide) // diagonal 2
        {
            GameOver(playerSide);
        }

        else if (moveCount >= 9)
        {
            GameOver("draw");
        }
        else
            ChangeSide();
    }

    void ChangeSide()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
        if(playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }else
        {
            SetPlayerColors(playerO, playerX);
        }
        
    }

    void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);
        
        if(winningPlayer == "draw")
        {
            SetGameOverText("It's a draw!");
            SetPlayerColorsInactive();
        }
        else
        {
            SetGameOverText(playerSide + " wins");
        }
       
    }

    void SetGameOverText(string winner)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = winner;
        restartButton.SetActive(true);
    }

    public void restartGame()
    {
        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
        //StartGame()
    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            if(toggle==true)
            {
                buttonList[i].GetComponentInParent<GridSpace>().EnableButton();
                buttonList[i].text = "";
            }else
            {
                buttonList[i].GetComponentInParent<GridSpace>().DisableButton();
            }
        }
    }
    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelCoolor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelCoolor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    public void SetPlayerSide(string startingSide)
    {
        playerSide = startingSide;
        if(playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
        StartGame();
    }

    void StartGame()
    {
        SetBoardInteractable(true);
        SetPlayerButtons(false);
    }

    void SetPlayerButtons(bool toggle)
    {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;   
    }

    void SetPlayerColorsInactive()
    {
        playerX.panel.color = inactivePlayerColor.panelCoolor;
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelCoolor;
        playerO.text.color = inactivePlayerColor.textColor;
    }
}
