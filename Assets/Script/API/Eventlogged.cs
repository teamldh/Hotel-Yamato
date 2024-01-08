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
    }

    public void PushEventLog()
    {
        if(APIManager.Instance.account != null)
        {
            foreach(EventLog log in eventLogs)
            {
                APIManager.Instance.account.updateEvent(new GameEvent(log.id_game, log.no_event), log.status);
            }
        }
    }
}
