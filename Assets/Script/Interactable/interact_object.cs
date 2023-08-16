using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public interface interact_object
{
    bool IsInteractable { get; set; }
    Events events { get; set; }
    void interact_object();
}
