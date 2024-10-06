sing System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player Player1;
    public Player Player2;

    CurrentPlayer currentPlayer;
    bool isPlayingScratch = false;
    bool isInningPlayer1 = false;
    int player1BallsRemaining;
    int player2BallsRemaining;

    [SerializeField] TextMeshProUGUI player1BallsText;
    [SerializeField] TextMeshProUGUI player2BallsText;
    [SerializeField] TextMeshProUGUI currentTurnText;
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] GameObject restartButton;
    [SerializeField] Transform headPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = CurrentPlayer.Player1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool Scratch()
    {
        if (currentPlayer == currentPlayer.Player1)
        {

            if (isWinningShotForPlayer1)
            {
                ScratchOnWinningShot("Player 1");
                return true;
            }
            else
            {
                if (isWinnningShotForPlayer2)
                {
                    ScratchOnWinningShot("Player 2");
                    return true;
                }
            }
            NextPlayerTurn();
            return false;
        }

    }

    void EarlyEightBall()
    {
        if (currentPlayer == CurrentPlayer.Player1)
        {
            Lose("Player 1 Hit in the eight ball too early and has lost!")
        }
        else
        {

            Lose("Player 2 Hit in the eight ball too early and has lost!")
        }
    }

    void ScratchOnWinningShot(string player)
    {
        Lose(player + " Scratched on Their Final Shot and Has Lost!");
    }

    void NoMoreBalls()
    {
        if (player == CurrentPlayer.Player1)
        {
            isWinningShotForPlayer1 = true;
        }
        else
        {
            isWinningShotForPlayer2 = true;
        }
    }

    bool CheckBall(Ball ball)

    {
        if (ball.IsCueBall())
        {
            if (Scratch())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (ball.IsEightBall())
        {
            if (currentPlayer == CurrentPlayer.Player1)
            {
                if (isWinningShotForPlayer1)
                {
                    Win("Player 1");
                    return true;
                }
            }
            else
            {
                if (isWinningShotForPlayer2)
                {
                    Win("Player 2");
                    return true;
                }
            }
            EarlyEightBall();
        }
        else
        {
            // All other logic when not eight ball or cue ball
            if (ball.IsBallRed())
            {
                playerBallsRemaining--;
                if (playerBallsRemaining <= 0)
                {
                    isWinningShotForPlayer1 = true;
                }
                if (currentPlayer != CurrentPlayer.Player1)
                {
                    NextPlayerTurn();
                }
            }
            else
            {
                playerBallsRemaining--;
                if (playerBallsRemaining <= 0)
                {
                    isWinningShotForPlayer2 = true;
                }
                if (currentPlayer != CurrentPlayer.Player2)
                {
                    NextPlayerTurn();
                }
            }
        }
        return = true;

    }

    void Lose(string message)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = message;
        restartButton.SetActive(true);
    }

    void Win(string player)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = message;
        restartButton.SetActive(true);
    }

    void NextPlayerTurn()
    {
        if (currentPlayer == CurrentPlayer.Player1)
        {
            currentPlayer = CurrentPlayer.Player2;
            currentTurnText.text = "Current Turn: Player 2";
        }
        else
        {
            currentPlayer = CurrentPlayer.Player1;
            currentTurnText.text = "Current Turn: Player 1";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if (CheckBall(other.gameObject.GetComponent<Ball>()))
            {
                Destroy(other.gameObject);
            }
        }
        else
        {
            other.gameObject.transform.position = headPosition.position;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}