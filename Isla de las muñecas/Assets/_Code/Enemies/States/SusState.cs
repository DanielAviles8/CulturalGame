using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SusState : EnemyState
{
    public SusState(Enemy controller) : base(controller) { }

    public override void OnEnter()
    {
        Controller.EnteringSusState();
    }
    public override void Update()
    {
        Controller.UpdatingSusState();
    }
    public override void OnExit() 
    {
        Controller.ExitingSusState();
    }
}
