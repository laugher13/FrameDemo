using UnityEngine;
using System.Collections;

public class MsgBase  {

    public ushort msgID;


    public ManagerID GetManagerID()
    {
        int temp = msgID / FrameTools.msgSpan;
        return (ManagerID)(temp*FrameTools.msgSpan);
    }

    public MsgBase(ushort msgID)
    {
        this.msgID = msgID;
    }
   
}
