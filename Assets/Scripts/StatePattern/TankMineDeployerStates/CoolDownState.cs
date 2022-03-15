using UnityEngine;

public class CoolDownState : TankMineDeployerState
{
    private TankMineDeployer m_tankMineDeployer;

    public CoolDownState(Transform transform) : base (transform)
    {
        m_tankMineDeployer = transform.GetComponent<TankMineDeployer>();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        m_tankMineDeployer.CoolDown();
    }
    public override void Tick()
    {
        
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}