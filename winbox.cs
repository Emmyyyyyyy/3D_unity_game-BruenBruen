using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        GameObject.Find("Car");
    }
}
