using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
public enum Axel{
    Front,
    Rear
}

[Serializable]
public struct Wheel{
    public GameObject wheelModel;
    public WheelCollider wheelCollider;
    public Axel axel;
}

public float maxAcceleration = 30.0f; //ความเร่งสูงสุด
public float brakeAcceleration = 50.0f; //ความหน่วงสูงสุด

public float turnSensitivity = 1.0f; //ใช้ในการเลี้ยวรถ
public float maxSteerAngle = 30.0f; //องศาสูงสุดในการเลี้ยวรถ

public Vector3 _centerOfMass;

//เก็บค่าของแต่ละล้อ
public List<Wheel> wheels;
float moveInput;
float steerInput;

private Rigidbody carRb;

void Start(){
    carRb = GetComponent<Rigidbody>();
    carRb.centerOfMass = _centerOfMass;
}

void Update(){
    GetInputs();
    AnimateWheels();
}

void LateUpdate(){
    Move();
    steer();
    Brake();
}

//functionรับพิกัดแกน
void GetInputs(){
    moveInput = Input.GetAxis("Vertical");
    steerInput = Input.GetAxis("Horizontal");
}

//fuctionเคลื่อนไหวโดยจะป้อนค่าให้แต่ละล้อ
void Move(){
    foreach(var wheel in wheels){
        wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime; 
        //motortorqueคือfunctionที่ใช้ในการทำให้รถขับเคลื่อนโดยการgetค่าaxisเข้ามา 600คือค่าtorque 
        //และนำมาคูนกับความเร่งและเวลาที่เปลี่ยนแปลงไป
    }
}

//functionเลี้ยวที่กระทำแค่เฉพาะล้อหน้าเท่านั้น
void steer(){
    foreach(var wheel in wheels){
        if (wheel.axel == Axel.Front){ //เอาไว้บอกว่าเงื่อนไขต่อไปนี้จะกระทำแค่กับที่ล้อหน้าเท่านั้น
            var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
            wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
        }
    }
}

//functionเบรคที่กระทำเมื่อมีการกดspacebar 
void Brake(){
    if(Input.GetKey(KeyCode.Space)){ //GetKey()คือfunctionที่ใช้ในการget inputเข้ามา เช่นกรณีนี้ถ้าเกิดการกดspacebarขึ้น
        foreach(var wheel in wheels){
            //สำหรับunityในการbrakeเราจะไม่ใช้ -motortorqueแต่เราจะใช้เป็นbraketpurqeแทน
            //โดยการเบรคไม่ต้องรับค่าAxisเข้ามา
            wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime; //300คือค่าtorque
        }
    }
    else{
        foreach(var wheel in wheels){ 
            wheel.wheelCollider.brakeTorque = 0; //ถ้าไม่กดspacebarจะไม่ให้มีการลดความเร็วลง
        }
    }
}

//functionไว้แสดงanimationของล้อ เมื่อล้อเิดการหมุนหรือขึ้นเนินเตี้ย 
void AnimateWheels(){
    foreach(var wheel in wheels){
        Quaternion rot;
        Vector3 pos;
        wheel.wheelCollider.GetWorldPose(out pos, out rot); //functionที่ใ้ชในการget positionของwheels
        wheel.wheelModel.transform.position = pos; //transform positionของล้อใหม่
        wheel.wheelModel.transform.rotation = rot;
    }
}
}
