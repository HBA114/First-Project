using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;

    void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
                axleInfo.leftWheelShape.transform.localRotation =
                    Quaternion.Euler(
                        new Vector3(motor,
                            steering - 180,
                            axleInfo.leftWheelShape.transform.localRotation.z));
                axleInfo.rightWheelShape.transform.localRotation =
                    Quaternion.Euler(
                        new Vector3(-motor,
                            steering,
                            axleInfo.rightWheelShape.transform.localRotation.z));
            }

            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
                axleInfo.leftWheelShape.transform.localRotation =
                    Quaternion.Euler(
                        new Vector3(motor,
                            axleInfo.leftWheelShape.transform.localRotation.y - 180,
                            axleInfo.leftWheelShape.transform.localRotation.z));
                axleInfo.rightWheelShape.transform.localRotation =
                    Quaternion.Euler(
                        new Vector3(-motor,
                            axleInfo.rightWheelShape.transform.localRotation.y,
                            axleInfo.rightWheelShape.transform.localRotation.z));
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
