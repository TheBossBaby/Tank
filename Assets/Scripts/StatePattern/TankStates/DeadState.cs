using UnityEngine;

public class DeadState : TankState
{
    public DeadState(Transform _tank) : base(_tank)
    {
    }

    public override void Tick()
    {
        throw new System.NotImplementedException();
    }
}
