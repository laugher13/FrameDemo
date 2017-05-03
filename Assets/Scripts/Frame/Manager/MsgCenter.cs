using UnityEngine;
using System.Collections;

public class MsgCenter : MonoBase {

    protected static MsgCenter _intance = null;
    public static MsgCenter Instance
    {
        get
        {
            if (_intance == null)
            {
                _intance = new MsgCenter();
            }
            return _intance;
        }
    }

    public void SendMsg(MsgBase msg)
    {
        AnalysisMsg(msg);
    }

    private void AnalysisMsg(MsgBase msg)
    {
        ManagerID tempID = msg.GetManagerID();
        switch (tempID)
        {
            case ManagerID.GameManager:
                break;
            case ManagerID.UIManager:
                break;
            case ManagerID.AudioManager:
                break;
            case ManagerID.NPCManager:
                break;
            default:
                break;
        }
    }

    public override void ProcessMsg(MsgBase msg)
    {
        
    }
}
