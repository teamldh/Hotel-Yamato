using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mision_act1 : MonoBehaviour
{
    private int benderaCount = 0;
    public int benderaCountMax;
    public GameObject quest1;
    public GameObject quest2;
    public Collider2D pintuCollider2D;

    public TextMeshProUGUI benderaCountText;

    private void Awake() {
        quest1.SetActive(true);
        quest2.SetActive(false);
        pintuCollider2D.enabled = false;
    }
    private void Update() {
        benderaCountText.text = "" + benderaCount + " / " + benderaCountMax;
    }
    public void benderaCountUp()
    {
        benderaCount++;
        if (benderaCount >= benderaCountMax)
        {
            quest1.SetActive(false);
            quest2.SetActive(true);
            pintuCollider2D.enabled = true;
        }
    }
}
