using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHandler : MonoBehaviour
{
    public UnityEvent OnAttackEvent;

    public void TriggerEvent(){
        OnAttackEvent?.Invoke();
    }
}
