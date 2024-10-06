using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball
{
    private bool isRed;
    private bool is8Ball = false;
    private bool isCueBall = false;

    RigidBody rb;
    //Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<RigidBody>();
    }

    //Update is called once per frame 

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (rb.velocity.y > 0)
        {

            Vector3 newVelocity = rb.velocity;
            newVelocity.Y = 0f;
            rb.velocity = newVelocity;

        }
    }

    public bool IsBallRed()
    {
        return isRed;


    }


    public void BallSetup(bool red)
    {
        isRed = red;
    }

    public void MakeEightBall()
    {
        is8Ball = true;
        GetComponent<Renderer>().material.color = Color.Magenta;
    }
}
}