/**
保存飞机的一些属性
**/


using UnityEngine;
using System.Collections;

public class FlightSystem : MonoBehaviour {

    public float Speed = 50.0f;
    public float SpeedMax = 100;
    private float MoveSpeed = 10;
    public float RotationSpeed = 1.0f;
    public float TurnSpeed = 2;
    public float SpeedPitch = 1;
    public float SpeedRoll = 1;
    public float SpeedYaw = 1;
    public float DampingTarget = 10.0f;
    public bool AutoPilot = false;

}
