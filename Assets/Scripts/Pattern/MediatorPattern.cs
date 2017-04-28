using UnityEngine;
using System.Collections;



public class AttackBase
{
    public virtual void Reduce()
    {
    }
}

public class NPCAttack:AttackBase
{
    public override void Reduce()
    {
       
    }
}

public class PlayerAttack : AttackBase
{
    public override void Reduce()
    {
        
    }
}


/// <summary>
/// 中介者模式
/// </summary>
public class MediatorPattern : MonoBehaviour {

    PlayerAttack playerAttack;
    NPCAttack npcAttack;

	void Start () {
	    
	}
	
	void Update () {
        	
	}
}
