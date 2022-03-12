using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete
{
    public class Mine : MonoBehaviour
    {
        public static event Action<Vector3> OnExplode;

        public float m_MineTimerDuration;
        public float m_TriggerRadius;
        public float m_MaximumDamage;

        private int m_DeployerPlayerNumber = 0;
        private Coroutine m_MineTimer;

        public void Deploy(int PlayerNumber)
        {
            m_DeployerPlayerNumber = PlayerNumber;
            m_MineTimer = StartCoroutine(MineTimer());
        }

        private void FixedUpdate()
        {
            DetectEnemy();
        }

        private void Explode()
        {
            Collider[] objectsInRange = Physics.OverlapSphere(transform.position, m_TriggerRadius);

            foreach (Collider col in objectsInRange)
            {
                TankShooting enemy = col.GetComponent<TankShooting>();
                if (enemy != null)
                {
                    StopCoroutine(m_MineTimer);

                    // linear falloff of effect
                    float proximity = (transform.position - enemy.transform.position).magnitude;
                    float effect = 1 - (proximity / m_TriggerRadius);
                    enemy.gameObject.GetComponent<TankHealth>().TakeDamage(effect * m_MaximumDamage);
                }
            }
            OnExplode?.Invoke(transform.position);
            Destroy(gameObject);
        }

        private void DetectEnemy()
        {
            Collider[] objectsInRange = Physics.OverlapSphere(transform.position, m_TriggerRadius);

            foreach (Collider col in objectsInRange)
            {
                TankShooting enemy = col.GetComponent<TankShooting>();

                if (enemy != null 
                    && (Vector3.Distance(transform.position, enemy.transform.position) < m_TriggerRadius)
                    && enemy.m_PlayerNumber != m_DeployerPlayerNumber)
                {
                    float proximity = (transform.position - enemy.transform.position).magnitude;
                    float effect = 1 - (proximity / m_TriggerRadius);
                    enemy.gameObject.GetComponent<TankHealth>().TakeDamage(effect * m_MaximumDamage);
                    OnExplode?.Invoke(transform.position);
                    Destroy(this.gameObject);
                }
            }
        }

        void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, m_TriggerRadius);
        }
        private IEnumerator MineTimer()
        {
            yield return new WaitForSeconds(m_MineTimerDuration);
            Explode();
        }
    }
}