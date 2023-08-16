using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_bendera_Act1 : MonoBehaviour, interactable
{
    public GameObject bendera;
    private bool benderanaik;
    public bool IsInteractable { get; set; } = true;
    [SerializeField] protected TextAsset inkJSON;

    public virtual void interact()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }
}
