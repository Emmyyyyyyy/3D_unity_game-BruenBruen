using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public enum ControlType {HumanInput, AI} //ประกาศให้สามารถเลือกได้ว่าจะบังคับด้วยhumanหรือAI
    public ControlType controlType = ControlType.HumanInput;

    public float BestLapTime {get; private set;} = Mathf.Infinity; //ตั้งค่าเริ่มต้นให้bestเป็นinfinity
    public float LastLapTime {get; private set;} = 0; //ตั้งค่าเริ่มต้นให้เวลาตาที่แล้วเป็น0
    public float CurrentLapTime {get; private set;} = 0; //ตั้งค่าให้เริ่มต้นให้เวลาตาปัจจุบันเป็น0
    public int CurrentLap = 0; //ตั้งค่าเริ่มต้นให้lapปัจจุบันเป็น0
    private bool startTimer = true; //ตั้งค่าให้ตัวเปิดปิดตัวจับเวลาเป็นtrueคือทำงานตั้งแต่เริ่มเล่นเกม

    //functionในการตั้งค่าตัวแปรเมื่อมีการเล่นเกม
    void Start(){
        CurrentLap = 0;
    }

    //functionที่ใช้ในการupdateตัวแปรต่างๆ
    void Update(){
        if(startTimer == true){
            //Time.deltaTimeเป็นfuntionที่ใช้จับเวลาตั้งแต่frameที่แล้วจนถึงframeปัจจุบันซึ่งจะต่างกับTime.timeตรงที่ว่า
            //Time.deltaTimeสามารถresetเวลาใหม่ได้แต่Time.Timeไม่สามารถทำได้
            CurrentLapTime = CurrentLapTime + Time.deltaTime;
        }
        if(startTimer == false){
            CurrentLapTime = 0; //ทำการsetเวลาใหม่เพื่อที่จะเริ่มเวลาในlapต่อไป
            CurrentLap++; //บวกจำนวนlapเพื่อแสดงว่ากำลังเล่นอยู่แลปถัดไป
            startTimer = true; //setให้startTimerเป็นtrueเพื่อเริ่มจับเวลาใหม่
        }
    }

    //functionใช้ในการเปลี่ยนแปลงค่าเมื่อเจอobjectที่กำหนดไว้
    void OnTriggerEnter(Collider other){
        //เมื่อเจอgame objaectที่ชื่อว่าWinBox
        if(other.gameObject.name == "WinBox"){ 
            startTimer = false; //setให้ตัวจับเวลาหยุดทำงาน
            LastLapTime = CurrentLapTime; //setให้เวลาตาที่แล้วเท่ากับเวลาปัจจุบัน
            BestLapTime = Mathf.Min(LastLapTime, BestLapTime); //ใช้function Mathf.Min ในการเปรียบเทียบหาตัวที่มีค่าน้อยกว่า
        }
    }
}
