using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TestDialog : MonoBehaviour, interactable
{
    public bool IsInteractable { get; set; } = true;
    public TextAsset inkJSON;
    public void interact()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }
}
