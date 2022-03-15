using UnityEngine;

public class ReadyToDeployState : TankMineDeployerState
{
    private TankMineDeployer m_TankMineDeployer;
    private TankShooting m_tankShooting;
    public ReadyToDeployState(Transform transform) : base(transform)
    {
        m_TankMineDeployer = transform.GetComponent<TankMineDeployer>();
        m_tankShooting = transform.GetComponent<TankShooting>();
    }

    public override void Tick()
    {
        TakeInput();
    }

    private void TakeInput()
    {
        if (Input.GetButtonDown(m_TankMineDeployer.DeployButton))
        {
            m_TankMineDeployer.DeployMine();
            m_TankMineDeployer.SetState(new CoolDownState(_transform));
        }
    }
}
