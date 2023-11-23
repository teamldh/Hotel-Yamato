using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Account
{
    // Subject to change
    public string accessToken;
    public string id_player;
    public string username;
    public string nama_player;

    public EventLog[] eventLogs;
    public Dictionary<GameEvent, EventLog> eventLogDict = new Dictionary<GameEvent, EventLog>();

    public Account(string id, string username, string nama_player)
    {
        this.id_player = id;
        this.username = username;
        this.nama_player = nama_player;

        // //dummy (id_game 1 = Hotel Yamato)
        // eventLogs = new EventLog[3];
        // eventLogs[0] = new EventLog(1, 1, EventStatus.selesai);
        // eventLogs[1] = new EventLog(1, 2, EventStatus.selesai);
        // eventLogs[2] = new EventLog(1, 3, EventStatus.belum);
        // //end dummy

        // foreach(EventLog log in eventLogs)
        // {
        //     eventLogDict.Add(new GameEvent(log.id_game, log.no_event), log);
        // }
    }

    /// <summary>
    /// Set the event log dictionary
    /// </summary>
    /// <param name="eventLogs"></param>
    public void SetEventLog(List<List<EventLog>> eventLogs)
    {
        if(eventLogs == null)
        {
            return;
        }

        foreach(List<EventLog> logs in eventLogs)
        {
            foreach(EventLog log in logs)
            {
                // if(log.id_game == APIManager.ID_GAME)
                // {
                //     continue;
                // }
                eventLogDict[new GameEvent(log.id_game, log.no_event)] = log;
            }
        }
    }

    public void setEventNewAccount()
    {
        eventLogs = new EventLog[4];
        eventLogs[0] = new EventLog(1, 1, EventStatus.belum);
        eventLogs[1] = new EventLog(1, 2, EventStatus.belum);
        eventLogs[2] = new EventLog(1, 3, EventStatus.belum);
        eventLogs[3] = new EventLog(1, 4, EventStatus.belum);
        foreach(EventLog log in eventLogs)
        {
            //cek apakah eventlog sudah ada di dictionary
            if(eventLogDict.ContainsKey(new GameEvent(log.id_game, log.no_event)))
            {
                continue;
            }
            eventLogDict.Add(new GameEvent(log.id_game, log.no_event), log);
        }
    }

    public void updateEvent(GameEvent gameEvent, EventStatus status)
    {
        if(CheckEventLog(gameEvent, status))
        {
            eventLogDict[gameEvent].status = status;
        }
    }

    public bool CheckEventLog(GameEvent gameEvent, EventStatus status)
    {
        if(eventLogDict.ContainsKey(gameEvent))
        {
            return eventLogDict[gameEvent].status == status;
        }
        else
        {
            return false;
        }
    }

    public EventLog[] getValueFromDict(){
        return eventLogDict.Values.ToArray();
    }
}


/// <summary>
/// Struct to simplify the Account's eventLog dictionary key
/// </summary>
public struct GameEvent
{
    public int id_game;
    public int no_event;

    public GameEvent(int id_game, int no_event)
    {
        this.id_game = id_game;
        this.no_event = no_event;
    }
}