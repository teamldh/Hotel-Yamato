using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface interactable
{
    bool IsInteractable { get; set; }
    void interact();   
}