using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    // Daniels verk
    [SerializeField] Rigidbody rigibogy;

    float horizontalInput;
    float verticalInput;

    Vector2 axis = new Vector2();

    const float acceleration = 0.7f;
    float moveSpeed = 6.0f;
    float sprintSpeed = 10.0f;

    bool sprinting = false;
    bool sprintingInitial = false;
    float sprintingFOVMultiplier = 1.2f;
    bool moving = false;
    float targetSpeed;

    float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 moveDirection;
        SpeedControl();

        if (axis.magnitude > 1)
        {
            moveDirection = transform.forward * axis.normalized.y + transform.right * axis.normalized.x;
        }
        else
        {
            moveDirection = transform.forward * axis.y + transform.right * axis.x;
        }

        if (moving)
        {
            Vector3 velocity = new Vector3(rigibogy.velocity.x, rigibogy.velocity.y, rigibogy.velocity.z);

            if (rigibogy.velocity.x <= targetSpeed)
            {
                velocity.x = Mathf.Lerp(rigibogy.velocity.x, moveDirection.x * targetSpeed, acceleration);
            }
            if (rigibogy.velocity.z <= targetSpeed)
            {
                velocity.z = Mathf.Lerp(rigibogy.velocity.z, moveDirection.z * targetSpeed, acceleration);
            }

            rigibogy.velocity = velocity;
            currentSpeed = new Vector3(rigibogy.velocity.x, 0, rigibogy.velocity.z).magnitude;
        }
    }

    private void SpeedControl()
    {
        targetSpeed = moveSpeed;

        if (Input.GetButton("Sprint"))
        {
            sprinting = true;
        }
        if (sprinting && axis.y > 0.8f)
        {
            targetSpeed = sprintSpeed;
        }
        else
        {
            sprinting = false;
        }

        if (axis.magnitude > 0.2) // This counts as a deadzone.
        {
            moving = true;
        }
        else
        {
            if (moving) // If the player WAS moving 
            {
                rigibogy.velocity = new Vector3(rigibogy.velocity.x * 0.5f, rigibogy.velocity.y, rigibogy.velocity.z * 0.5f);
            }

            moving = false;
        }
    }

    //private void Movement()
    //{
    //    Vector3 moveDirection;

    //    if (_axis.magnitude > 1) // Förklara detta 
    //    {
    //         moveDirection = transform.forward * _axis.normalized.y + transform.right * _axis.normalized.x;
    //    }
    //    else
    //    {
    //        moveDirection = transform.forward * _axis.y + transform.right * _axis.x;
    //    }

    //        Vector3 currentVelocity = new Vector3(_body.velocity.x, 0, _body.velocity.z);
    //        Vector3 simulatedVelocity = currentVelocity + moveDirection;
    //        float currentMagnitude = currentVelocity.magnitude;
    //        float simulatedMagnitude = simulatedVelocity.magnitude;

    //        if (currentMagnitude > 6)
    //        {
    //            if (simulatedMagnitude < currentMagnitude)
    //            {
    //                _body.AddForce(moveDirection * 6 * 2, ForceMode.Force);
    //            }
    //            else
    //            {
    //                Vector3 newDirection = simulatedVelocity * (currentMagnitude / simulatedMagnitude) - currentVelocity;

    //                _body.AddForce(newDirection * 6 * 2, ForceMode.Force);
    //            }
    //        }
    //        else
    //        {
    //            _body.AddForce(moveDirection * 6 * 2, ForceMode.Force);
    //        }
}
