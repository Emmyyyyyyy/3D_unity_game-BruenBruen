using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject UIRacePanel;

    //ในเกมเรามีการสร้างui textขึ้นมา ซึ่งโค้ดเหล่านี้จะควบคุมtextแต่ละอันที่สร้างขึ้นมา
    public Text UITextCurrentLap; 
    public Text UITextCurrentLapTime;
    public Text UITextLastLapTime;
    public Text UITextBestLapTime;

    //ดึงโค้ดจากplayer.csมาใช้ร่วมกันโดยใช้ชื่อว่าUpdateUIForPlayer
    public player UpdateUIForPlayer;

    private int currentLap = -1;
    private float currentLapTime = -1;
    private float lastLapTime;
    private float bestLapTime;

    //functionที่ใช้ในการupdate
    void Update(){
        //ถ้าไม่มีอะไรให้updateไม่ต้องreturnค่าอะไรกลับไป
        if(UpdateUIForPlayer == null){
            return;
        }
        //ถ้าlapไม่ตรงกับlapที่โชว์ออกไปในปัจจุบันให้ตั้งค่าlapปัจจุบันใหม่และโชว์ออกไป
        if(UpdateUIForPlayer.CurrentLap != currentLap){
            currentLap = UpdateUIForPlayer.CurrentLap;
            UITextCurrentLap.text = $"{currentLap}"; //$"{}" เป็นfunctionที่pass valueแล้วแสดงออกมา
        }
        //ถ้าCurrentLapTimeไม่ตรงกับCurrentLapTimeที่โชว์ออกไปในปัจจุบันให้ตั้งค่าCurrentLapTimeปัจจุบันใหม่และโชว์ออกไป
        if(UpdateUIForPlayer.CurrentLapTime != currentLapTime){
            currentLapTime = UpdateUIForPlayer.CurrentLapTime;
            UITextCurrentLapTime.text = $"TIME: {(int)currentLapTime/60}:{(currentLapTime)%60:00.000}";
        }
        //ถ้าLastLapTimeไม่ตรงกับLastLapTimeที่โชว์ออกไปในปัจจุบันให้ตั้งค่าLastLapTimeปัจจุบันใหม่และโชว์ออกไป
        if(UpdateUIForPlayer.LastLapTime != lastLapTime){
            lastLapTime = UpdateUIForPlayer.LastLapTime;
            UITextLastLapTime.text = $"LAST: {(int)lastLapTime/60}:{(lastLapTime)%60:00.000}";
        }
        //ถ้าBestLapTimeไม่ตรงกับBestLapTimeที่โชว์ออกไปในปัจจุบันให้ตั้งค่าBestLapTimeปัจจุบันใหม่และโชว์ออกไป
        if(UpdateUIForPlayer.BestLapTime != bestLapTime){
            bestLapTime = UpdateUIForPlayer.BestLapTime;
            //if statementที่ใช้ในการเทียบcondition ถ้าไม่ตรงเงื่อนไขให้set bestเป็นnone
            UITextBestLapTime.text = bestLapTime < 1000000 ? $"BEST: {(int)bestLapTime/60}:{(bestLapTime)%60:00.000}" : "BEST: NONE";
        }
    }
}
