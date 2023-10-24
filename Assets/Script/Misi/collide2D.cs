using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collide2D : MonoBehaviour
{
    public bool isPlayer;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            isPlayer = true;
        }
    }
}
