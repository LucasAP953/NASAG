using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code : MonoBehaviour
{
    int redBallsRemaining = 7;
    int blueBallsRemaining = 7;
    float ballRadius;
    float ballDiameter;
    float ballDiameterWithBuffer;

    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform cueBallPosition;
    [SerializeField] Transform headBallPosition;

    // Start is called before the first frame update
    void Start()
    {
        ballRadius = ballPrefab.GetComponent<SphereCollider>().radius * 100f;
        ballDiameter = ballRadius * 2f;
        PlaceAllBalls();
    }

    void PlaceAllBalls()
    {
        PlaceCueBall();
        PlaceRandomBalls();
    }

    void PlaceCueBall()
    {
        GameObject ball = Instantiate(ballPrefab, cueBallPosition.position, Quaternion.identity);
        ball.GetComponent<Ball>().MakeCueBall();
    }

    void PlaceEightBall(Vector3 position)
    {
        GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
        ball.GetComponent<Ball>().MakeEightBall();
    }

    void PlaceRandomBalls()
    {
        int numInThisRow = 1;
        int rand;
        Vector3 firstInRowPosition = headBallPosition.position;
        Vector3 currentPosition = firstInRowPosition;

        void PlaceRedBall(Vector3 position)
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(true);
            redBallsRemaining--;
        }
    }

    void PlaceRandomBall()
    {
        int rand;
        Vector3 firstRowPosition = headBallPosition.position;
        Vector3 currentPosition = firstRowPosition;

        void PlaceBallAt(Vector3 position)
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(true);
            redBallsRemaining--;
        }

        void PlaceBlueBall(Vector3 position)
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(false);
            blueBallsRemaining--;
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < NumThisRow; j++)
            {
                if (i == 2 && j == 1)
                    PlaceBallAt(currentPosition);
                else if (redBallsRemaining > 0 && blueBallsRemaining > 0)
                {
                    rand = Random.Range(0, 2);
                    if (rand == 0)
                        PlaceBallAt(currentPosition);
                    else
                        PlaceBlueBall(currentPosition);
                }
                else
                {
                    PlaceBallAt(currentPosition);
                }
            }
            if (redBallsRemaining > 0)
                PlaceBallAt(currentPosition);
            else
                PlaceBlueBall(currentPosition);
        }

        currentPosition += new Vector3(1, 0, 0).normalized * ballDiameter;
        firstRowPosition += new Vector3(-1, 0, 0).normalized * ballDiameter;
        currentPosition += firstRowPosition;
        NumThisRow++;
    }
}
