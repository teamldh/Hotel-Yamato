using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countEnemyDead : MonoBehaviour
{
    // public int maxcountEnemy;
    public GameObject[] NPC;
    // public int countEnemy {get; set;}
    public GameObject objectParent;
    public collide2D collide2D;

    private void Start() {
    }

    void Update()
    {
        Transform objectParentTransform = objectParent.transform;
        int childCount = objectParentTransform.childCount;
        if(collide2D.isPlayer == true){
            for(int i = 0; i < NPC.Length; i++){
                NPC[i].GetComponent<NPCshoot>().mulaiTembakan();
            }

            for (int i = 0; i < childCount; i++) {
                GameObject child = objectParentTransform.GetChild(i).gameObject;
                child.GetComponent<enemy>().mulaiTembakan();
            }
        }
        else{
            for(int i = 0; i < NPC.Length; i++){
                NPC[i].GetComponent<NPCshoot>().hentikanTembakan();
            }

            for (int i = 0; i < childCount; i++) {
                GameObject child = objectParentTransform.GetChild(i).gameObject;
                child.GetComponent<enemy>().hentikanTembakan();
            }
        }
        Debug.Log(objectParent.name + " " + objectParent.transform.childCount);
        if(objectParent.transform.childCount == 0){
            Debug.Log("Misi Selesai");
            for(int i = 0; i < NPC.Length; i++){
                NPC[i].GetComponent<NPCshoot>().hentikanTembakan();
            }
        }
    }
}
