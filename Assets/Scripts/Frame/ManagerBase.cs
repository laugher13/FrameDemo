using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EventNode
{
    //当前数据
    public MonoBase mono;
    //下一个节点
    public EventNode next;

    public EventNode(MonoBase mono)
    {
        this.mono = mono;
        this.next = null;
    }
}

public class ManagerBase : MonoBase
{

    Dictionary<ushort, EventNode> eventNode = new Dictionary<ushort, EventNode>();

    public void RegisterMsg(MonoBase mono, params ushort[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            EventNode temp = new EventNode(mono);
            RegisterMsg(args[i], temp);
        }
    }

    public void RegisterMsg(ushort id, EventNode node)
    {
        if (!eventNode.ContainsKey(id))
        {
            eventNode.Add(id, node);
        }
        else
        {
            EventNode temp = eventNode[id];
            while (temp.next != null)
            {
                temp = temp.next;
            }
            temp.next = node;
        }
    }

    public void UnRegisterMsg(MonoBase mono, params ushort[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            UnRegisterMsg(args[i], mono);
        }
    }

    public void UnRegisterMsg(ushort id, MonoBase mono)
    {
        if (!eventNode.ContainsKey(id))
        {
            return;
        }
        else
        {
            EventNode temp = eventNode[id];
            if (temp.mono == mono)  //去掉头部数据
            {
                EventNode header = temp;
                if (header.next != null)
                {
                    header.mono = temp.mono;
                    header.next = temp.next;
                }
                else               //只有一个数据
                {
                    eventNode.Remove(id);
                }
            }
            else                   //去掉中间、尾部数据
            {
                while (temp.next != null && temp.next.mono != null)
                {
                    temp = temp.next;
                }
                if (temp.next.next != null)
                {
                    temp.next = temp.next.next; //去掉中间数据
                }
                else
                {
                    temp.next = null;           //去掉尾部数据
                }
            }
        }
    }

    /// <summary>
    /// 消息到达，通知整个消息列表
    /// </summary>
    /// <param name="msg"></param>
    public override void ProcessMsg(MsgBase msg)
    {
        if (!eventNode.ContainsKey(msg.msgID))
        {
            Debug.Log("key is null");
            Debug.Log("msg manager" + msg.GetManagerID());
            return;
        }
        else
        {
            EventNode temp = eventNode[msg.msgID];
            do
            {
                //策略模式
                temp.mono.ProcessMsg(msg);
            }
            while (temp != null);
        }
    }
}
