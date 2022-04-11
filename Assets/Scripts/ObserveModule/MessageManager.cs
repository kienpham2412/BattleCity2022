using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MessageType
{
    OnGameRestart = 0,
    OnEnemyDestroyed = 1,
    OnGrenadeAcquired = 2,
    OnClockAcquired = 3,
    OnGameFinish = 4,
    OnPlayerDestroyed = 5,
    OnGameOver = 6
}

public class Message
{
    public Message(MessageType type)
    {
        this.type = type;
    }

    public Message(MessageType type, object content)
    {
        this.type = type;
        this.content = content;
    }
    public MessageType type;
    public object content;
}

public interface ISubscriber
{
    public void Handle(Message message);
}

public class MessageManager : MonoBehaviour
{
    public static MessageManager Instance;
    private Dictionary<MessageType, List<ISubscriber>> subscribers;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        subscribers = new Dictionary<MessageType, List<ISubscriber>>();
    }
    public void AddSubscriber(MessageType type, ISubscriber subscriber)
    {
        if (!subscribers.ContainsKey(type))
            subscribers.Add(type, new List<ISubscriber>());
        if (!subscribers[type].Contains(subscriber))
            subscribers[type].Add(subscriber);
    }

    public void RemoveSubscriber(MessageType type, ISubscriber subscriber)
    {
        if (subscribers.ContainsKey(type))
            if (subscribers[type].Contains(subscriber))
                subscribers[type].Remove(subscriber);
    }

    public void SendMessage(Message message)
    {
        for (int i = 0; i < subscribers[message.type].Count; i++)
        {
            subscribers[message.type][i].Handle(message);
        }
    }
}
