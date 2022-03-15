using UnityEngine;

public class CoolDownState : TankMineDeployerState
{
    private TankMineDeployer m_tankMineDeployer;
    private float m_TimeElapsed;
    public CoolDownState(Transform transform) : base (transform)
    {
        m_tankMineDeployer = transform.GetComponent<TankMineDeployer>();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }
    public override void Tick()
    {
        m_TimeElapsed += Time.deltaTime;
        if (m_TimeElapsed >= m_tankMineDeployer.m_coolDownDuration)
        {
            m_tankMineDeployer.SetState(new ReadyToDeployState(_transform));
        }
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}