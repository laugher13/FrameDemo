using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : ManagerBase
{

    public void SendMsg(MsgBase msg)
    {
        if (msg.GetManagerID() == ManagerID.UIManager)
        {
            ProcessMsg(msg);
        }
        else
        {
            MsgCenter.Instance.SendMsg(msg);
        }
    }


}
