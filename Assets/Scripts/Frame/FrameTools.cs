using UnityEngine;
using System.Collections;


public enum ManagerID
{
    GameManager = 0,
    UIManager =FrameTools.msgSpan,
    AudioManager = FrameTools.msgSpan * 2,
    NPCManager = FrameTools.msgSpan * 3
}

public class FrameTools  {

    public const int msgSpan=3000;   
}
