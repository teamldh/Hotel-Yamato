using System;
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
        
        // Delete all event log with id_game = 1
        List<GameEvent> eventToRemove = new List<GameEvent>();
        foreach(EventLog log in eventLogDict.Values)
        {
            if(log.id_game == 1)
            {
                eventToRemove.Add(new GameEvent(log.id_game, log.no_event));
                //Debug.Log("Remove event log: " + log.id_game + " " + log.no_event);
            }
        }
        foreach(GameEvent gameEvent in eventToRemove)
        {
            eventLogDict.Remove(gameEvent);
            //Debug.Log("Remove event log: " + gameEvent.id_game + " " + gameEvent.no_event);
        }

        // Add new event log
        foreach(EventLog log in eventLogs)
        {
            eventLogDict.Add(new GameEvent(log.id_game, log.no_event), log);
            //Debug.Log(eventLogDict[new GameEvent(log.id_game, log.no_event)].status);
        }

        Debug.Log("Event log dict count: " + eventLogDict.Count);
    }

    public void updateEvent(GameEvent gameEvent, EventStatus status)
    {
        if(eventLogDict.ContainsKey(gameEvent))
        {
            eventLogDict[gameEvent].status = status;
            //Debug.Log("Update event log: " + gameEvent.id_game + " " + gameEvent.no_event + " " + status);
        }
    }

    /// <summary>
    /// Check if the event log is the same as the status specified
    /// </summary>
    /// <param name="gameEvent"></param>
    /// <param name="status"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Get the event log value from the dictionary with id_game = APIManager.ID_GAME
    /// </summary>
    /// <returns></returns>
    public EventLog[] getValueFromDict(){
        List<EventLog> arr = new List<EventLog>();
        foreach(EventLog log in eventLogDict.Values)
        {

            if(log.id_game == APIManager.ID_GAME)
            {
                arr.Add(log);
                //Debug.Log(log.id_game + " " + log.no_event + " " + log.status);
            }
        }
        return arr.ToArray();
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