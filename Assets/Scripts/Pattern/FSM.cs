using UnityEngine;
using System.Collections;


public abstract class FSMState
{
    private byte stateID;

    public virtual void OnBeforEnter()
    {

    }
    public virtual void CapyState()
    {

    }
    public abstract void OnEnter();

    public virtual  void Update()
    {

    }
    public virtual void OnLeave()
    {

    }
}

public class FSMManager
{
    private FSMState[] fsmState;
    public byte curAdd;
    public byte curStateID;

    public FSMManager(byte number)
    {
        curAdd = 0;
        curStateID = 0;
        fsmState=new FSMState[number];
    }

    public void AddState(FSMState tempState)
    {
        if (curAdd<fsmState.Length)
        {
            fsmState[curAdd] = tempState;
            curAdd++;
        }
    }

    public void ChangeState(byte stateID)
    {
        fsmState[curStateID].OnLeave();
        fsmState[stateID].CapyState();
        fsmState[stateID].OnBeforEnter();
        fsmState[stateID].OnEnter();
        curStateID = stateID;
    }


}

public class IdleState:FSMState
{
    public override void OnEnter()
    {
        
    }
}

public class AttackState:FSMState
{
    public override void OnEnter()
    {
       
    }
}




/// <summary>
/// 状态机
/// </summary>
public class FSM : MonoBehaviour {

    FSMManager fsmManager;
    IdleState idle;
    AttackState attack;

    void Start()
    {
        fsmManager = new FSMManager(2);
        idle = new IdleState();
        attack = new AttackState();
        fsmManager.AddState(idle);
        fsmManager.AddState(attack);
    }
	
	
	void Update () {
	
	}
}
