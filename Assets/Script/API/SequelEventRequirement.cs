using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SequelEventRequirement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool executeOnStart = false;
    
    [SerializeField] private EventLog[] requiredEvents;
    [SerializeField] private UnityEvent onRequirementFulfilled;
    [SerializeField] private UnityEvent onRequirementNotFulfilled;
    



    private void Start() 
    {
        if(executeOnStart)
        {
            ExecuteRequirement();
        }
    }

    

    /// <summary>
    /// Checks if the sequel/previous game event has been fulfilled, and execute the requirement based on the result
    /// </summary>
    public void ExecuteRequirement()
    {
        if(APIManager.Instance.account != null)
        {
            if(IsRequirementFulfilled())
            {
                onRequirementFulfilled.Invoke();
                Debug.Log("Requirement fulfilled");
            }
            else
            {
                onRequirementNotFulfilled.Invoke();
                Debug.Log("Requirement not fulfilled");
            }
        }
        else
        {
            onRequirementNotFulfilled.Invoke();
            Debug.Log("there is no account");
        }
    }


    /// <summary>
    /// Checks if the required events have been fulfilled based on Account's eventLog API
    /// </summary>
    public bool IsRequirementFulfilled()
    {
        foreach(EventLog log in requiredEvents)
        {
            if(APIManager.Instance.account == null) return false;
            
            GameEvent gameEvent = new GameEvent(log.id_game, log.no_event);
            if(APIManager.Instance.account.CheckEventLog(gameEvent, log.status) == false)
            {
                return false;
            }
        }
        return true;
    }
}
