using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankMineDeployerState 
{
    // Start is called before the first frame update
    protected Transform _transform;

    public abstract void Tick();
    public virtual void FixedTick() { }

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public TankMineDeployerState(Transform transform)
    {
        this._transform = transform;
    }
}
