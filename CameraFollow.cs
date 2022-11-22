using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float moveSmoothness; //ตั้งให้ค่าความsmoothของการเคลื่อนที่ของกล้องเป็นfloat
    public float rotSmoothness; //ตั้งให้ค่าความsmoothของการหมุน(rot = rotation)ของกล้องเป็นfloat

    public Vector3 moveOffset; //เป็นการสร้างvector3dขึ้นมา
    public Vector3 rotOffset;

    public Transform carTarget; //กำหนดตำแหน่ง, การหมุน และ scaleของobject

    //functionเอาไว้update
    void FixedUpdate(){ 
        FollowTarget();
    }

    //functionเอาไว้ตามtargetโดยแบ่งป็นmovementกับrotation
    void FollowTarget(){
        HandleMovement();
        HandleRotation();
    }

    //functionที่เอาไว้จัดการmovement
    void HandleMovement(){
        Vector3 targetPos = new Vector3(); //เอาไว้เก็บตำแหน่งต่อไป
        targetPos = carTarget.TransformPoint(moveOffset); 
        //transferpointเป็ยfunctionไว้ใช้ในการreturnพิกัดโดยจะเปลื่ยนแบบ(ตำแหน่งปัจจุบัน+ตำแน่งใหม่)
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSmoothness*Time.deltaTime); 
        //transform positionโดยใช้function lerp(a,b,c)ซึ่งจะเดินทางระหว่างระยะaถึงbโดยมีcที่มีค่า0-1เป็นตัวควบคุม มีสูครเป็นa+(b-a)* t
    }

    //functionที่เอาไว้จัดการrotation
    void HandleRotation(){
        var direction = carTarget.position - transform.position;
        var rotation = new Quaternion(); //Quaternionคือสิ่งที่เอาไว้represent rotationซึ่งมีพิกัดเป็น(z,y,z,w)

        rotation = Quaternion.LookRotation(direction + rotOffset, Vector3.up); 
        //function lookrotationใช้ในการreturnค่าพิกัดเมื่อเกิดการrotateของobject

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotSmoothness * Time.deltaTime);
    }
}
