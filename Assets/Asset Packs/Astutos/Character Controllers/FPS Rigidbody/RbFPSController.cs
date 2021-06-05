using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RbFPSController : MonoBehaviour
{
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeedFactor = 2f;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [Tooltip("LayerMask ground check checks for")] [SerializeField] LayerMask groundMask;
    [Tooltip("Time: Slope angle, Value: Speed factor")][SerializeField] AnimationCurve slopeCurveModifier;

    Rigidbody rigidBody;
    Vector3 groundNormal;
    float desiredSpeed;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        desiredSpeed = walkSpeed;
    }
    private void Update()
    {
        UpdateDesiredSpeed();
    }

    private void UpdateDesiredSpeed()
    {
        if (Input.GetKey(sprintKey))
        {
            desiredSpeed = walkSpeed * sprintSpeedFactor;
        }
        else
        {
            desiredSpeed = walkSpeed;
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();
        Vector2 input = GetInput();

        Vector3 movement = transform.forward * input.y + transform.right * input.x;
        movement = Vector3.ProjectOnPlane(movement,groundNormal).normalized * desiredSpeed;

        if(rigidBody.velocity.sqrMagnitude <= desiredSpeed * desiredSpeed)
        {
            rigidBody.AddForce(movement * SlopeMultiplier(), ForceMode.Impulse);
        }
        
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private float SlopeMultiplier()
    {
        return slopeCurveModifier.Evaluate(Vector3.Angle(groundNormal,Vector3.up));
    }

    private void GroundCheck()
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, .5f, Vector3.down, out hitInfo,
                               2, groundMask, QueryTriggerInteraction.Ignore))
        {
            groundNormal = hitInfo.normal;
        }
    }
}
