using System.Collections;
using UnityEngine;

public class TankMineDeployer : MonoBehaviour
{
    public Complete.Mine m_MinePrefab;
    [Range(1,5)]
    public float m_coolDownDuration;
    private Complete.TankShooting m_tankShooting;
    private TankMineDeployerState m_currentState;
    private string m_DeployButton;
    public string DeployButton => m_DeployButton;
    // Start is called before the first frame update
    void Start()
    {
        m_tankShooting = GetComponent<Complete.TankShooting>();
        m_DeployButton = "Deploy" + m_tankShooting.m_PlayerNumber;
        SetState(new ReadyToDeployState(transform)) ;
    }

    // Update is called once per frame
    void Update()
    {
        m_currentState.Tick();
    }

    public void DeployMine()
    {
        var Mine = Instantiate(m_MinePrefab, transform.position, transform.rotation);
        Mine.Deploy(m_tankShooting.m_PlayerNumber);
    }

    public void SetState(TankMineDeployerState state)
    {
        if (m_currentState != null)
            m_currentState.OnStateExit();

        m_currentState = state;

        if (m_currentState != null)
            m_currentState.OnStateEnter();
    }
}
