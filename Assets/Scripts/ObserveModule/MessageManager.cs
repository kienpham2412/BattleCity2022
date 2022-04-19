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
    /// <summary>
    /// Handle the event when it is invoked
    /// </summary>
    /// <param name="message">The content of the event</param>
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

    /// <summary>
    /// Register a listener of the message
    /// </summary>
    /// <param name="type">The type of a message</param>
    /// <param name="subscriber">The instance of registered object</param>
    public void AddSubscriber(MessageType type, ISubscriber subscriber)
    {
        if (!subscribers.ContainsKey(type))
            subscribers.Add(type, new List<ISubscriber>());
        if (!subscribers[type].Contains(subscriber))
            subscribers[type].Add(subscriber);
    }

    /// <summary>
    /// Remove the register of a message
    /// </summary>
    /// <param name="type">The type of a message</param>
    /// <param name="subscriber">The instance of registered object</param>
    public void RemoveSubscriber(MessageType type, ISubscriber subscriber)
    {
        if (subscribers.ContainsKey(type))
            if (subscribers[type].Contains(subscriber))
                subscribers[type].Remove(subscriber);
    }

    /// <summary>
    /// Broadcast the message for all subscribers
    /// </summary>
    /// <param name="message">The message content</param>
    public void SendMessage(Message message)
    {
        for (int i = 0; i < subscribers[message.type].Count; i++)
        {
            subscribers[message.type][i].Handle(message);
        }
    }
}
