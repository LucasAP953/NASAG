using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 offset;
    [SerializeField] float downAngle;
    private float horizontalInput;

    Transform cueBall;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            if (ball.GetComponent<Ball>().IsCueBall)
            {
                cueBall = ball.transform;
                break;
            }
        }

        ResetCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (cueBall != null)
        {
            horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.RotateAround(cueBall.position, Vector3.up, horizontalInput);
        }
        //temporary
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetCamera();
        }// end temporary

        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 hitDirection = transform.forward;
            hotDirection = new Vector3(hitDirection.x, 0, hitDirection.x).normalized;

            cueBall.gameObject.GetComponent<RigidBody>().AddForce(hitDirection * power, ForceMode);
            Debug.Break();
        }
    }

    public void ResetCamera()
    {
        transform.position = cueBall.position + offset;
        transform.LookAt(cueBall.position);
        transform.localEulerAngles = new Vector3(downAngle, transform.localEulerAngles.y, 0);
    }
}