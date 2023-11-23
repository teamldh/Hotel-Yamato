using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Eventlogged : MonoBehaviour
{
    [SerializeField] private EventLog[] eventLogs;

    void Start(){
        PushEventLog();
        // EventLog[] arr = new EventLog[APIManager.Instance.account.eventLogDict.Count];
        // Debug.Log(arr);
        getValueDict();
    }

    public void PushEventLog()
    {
        foreach(EventLog log in eventLogs)
        {
            APIManager.Instance.account.updateEvent(new GameEvent(log.id_game, log.no_event), log.status);
        }
    }

    public EventLog[] getValueDict(){
        EventLog[] arr = new EventLog[APIManager.Instance.account.eventLogDict.Count];
        //APIManager.Instance.account.eventLogDict.Values.CopyTo(arr, 0);
        arr = APIManager.Instance.account.eventLogDict.Values.ToArray();
        Debug.Log(arr);
        return arr;
    }
}
