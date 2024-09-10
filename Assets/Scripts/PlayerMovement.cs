using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody _body;

    float _horizontalInput;
    float _verticalInput;

    Vector2 _axis = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Movement();
    }

    private void Movement()
    {
        Vector3 moveDirection;

        if (_axis.magnitude > 1) // Förklara detta 
        {
             moveDirection = transform.forward * _axis.normalized.y + transform.right * _axis.normalized.x;
        }
        else
        {
            moveDirection = transform.forward * _axis.y + transform.right * _axis.x;
        }

            Vector3 currentVelocity = new Vector3(_body.velocity.x, 0, _body.velocity.z);
            Vector3 simulatedVelocity = currentVelocity + moveDirection;
            float currentMagnitude = currentVelocity.magnitude;
            float simulatedMagnitude = simulatedVelocity.magnitude;

            if (currentMagnitude > 6)
            {
                if (simulatedMagnitude < currentMagnitude)
                {
                    _body.AddForce(moveDirection * 6 * 2, ForceMode.Force);
                }
                else
                {
                    Vector3 newDirection = simulatedVelocity * (currentMagnitude / simulatedMagnitude) - currentVelocity;

                    _body.AddForce(newDirection * 6 * 2, ForceMode.Force);
                }
            }
            else
            {
                _body.AddForce(moveDirection * 6 * 2, ForceMode.Force);
            }
        





        //Vector3 velocity = new Vector3(_body.velocity.x, _body.velocity.y, _body.velocity.z); // Är inte alla noll? Vad är rigidbody velocity?

        //if (_body.velocity.x <= 6) //Vad är targetspeed?
        //{
        //    velocity.x = Mathf.Lerp(_body.velocity.x, moveDirection.x * 6, 0.7f); // Vad menas med const acceleration? Också vad händer här lol
        //}

        //if (_body.velocity.z <= 6)
        //{
        //    velocity.x = Mathf.Lerp(_body.velocity.z, moveDirection.z * 6, 0.7f);
        //}

        //_body.velocity = velocity;

    }
}
