using UnityEngine;

public abstract class TankState
{
    protected Transform _transform;

    public abstract void Tick();
    public virtual void FixedTick() { }

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public TankState(Transform transform)
    {
        this._transform = transform;
    }
}