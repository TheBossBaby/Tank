using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMineDeployer : MonoBehaviour
{
    public Complete.Mine m_MinePrefab;
    public string m_DeployButton;
    private Complete.TankShooting m_tankShooting;
    // Start is called before the first frame update
    void Start()
    {
        m_tankShooting = GetComponent<Complete.TankShooting>();
        m_DeployButton = "Deploy" + m_tankShooting.m_PlayerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(m_DeployButton))
        {
            var Mine = Instantiate(m_MinePrefab, transform.position, transform.rotation);
            Mine.Deploy(m_tankShooting.m_PlayerNumber);
        }
    }
}
