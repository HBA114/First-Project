using System;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    private float _speed;

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        _speed = _rigidbody.velocity.magnitude;
        float wheelSpeed = (float)(_speed / (2 * Math.PI * axleInfos[0].leftWheel.radius));

        Debug.Log("Speed (m/s): " + _speed);
        Debug.Log("Wheel Speed (rps): " + wheelSpeed);

        foreach (AxleInfo axleInfo in axleInfos)
        {
            axleInfo.leftWheelShape.transform.localRotation =
                Quaternion.Euler(
                    new Vector3(wheelSpeed * 360,
                        axleInfo.leftWheelShape.transform.localRotation.y - 180,
                        axleInfo.leftWheelShape.transform.localRotation.z));
            axleInfo.rightWheelShape.transform.localRotation =
                Quaternion.Euler(
                    new Vector3(-wheelSpeed * 360,
                        axleInfo.rightWheelShape.transform.localRotation.y,
                        axleInfo.rightWheelShape.transform.localRotation.z));
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
                
                axleInfo.leftWheelShape.transform.localRotation =
                    Quaternion.Euler(
                        new Vector3(axleInfo.leftWheelShape.transform.localRotation.x,
                            steering - 180,
                            axleInfo.leftWheelShape.transform.localRotation.z));
                axleInfo.rightWheelShape.transform.localRotation =
                    Quaternion.Euler(
                        new Vector3(axleInfo.rightWheelShape.transform.localRotation.x,
                            steering,
                            axleInfo.rightWheelShape.transform.localRotation.z));
            }

            if (axleInfo.motor)
            {
                if (axleInfo.rightWheel.rpm < 5500)
                {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                }
                else
                {
                    axleInfo.leftWheel.motorTorque -= 100;
                    axleInfo.rightWheel.motorTorque -= 100;
                }

                // axleInfo.leftWheelShape.transform.localRotation =
                //     Quaternion.Euler(
                //         new Vector3((float)_speed * 100,
                //             axleInfo.leftWheelShape.transform.localRotation.y - 180,
                //             axleInfo.leftWheelShape.transform.localRotation.z));
                // axleInfo.rightWheelShape.transform.localRotation =
                //     Quaternion.Euler(
                //         new Vector3((float)-_speed * 100,
                //             axleInfo.rightWheelShape.transform.localRotation.y,
                //             axleInfo.rightWheelShape.transform.localRotation.z));
            }
        }
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public GameObject leftWheelShape;
    public GameObject rightWheelShape;
    public bool motor;
    public bool steering;
}
