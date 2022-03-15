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
        public ParticleSystem m_ExplosionParticleEffect;
        public AudioSource m_LandMineExplosionAudioSource;
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
            PlayParticleEffect();
            m_LandMineExplosionAudioSource.Play();
            Destroy(gameObject);
        }

        private void PlayParticleEffect()
        {
            m_ExplosionParticleEffect.transform.SetParent(null);
            m_ExplosionParticleEffect.Play();
            Destroy(m_ExplosionParticleEffect.gameObject, m_ExplosionParticleEffect.main.duration);
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
                    enemy.gameObject.GetComponent<TankHealth>().TakeDamage(CalculateDamage(enemy.transform));
                    OnExplode?.Invoke(transform.position);
                    PlayParticleEffect();
                    m_LandMineExplosionAudioSource.Play();
                    Destroy(this.gameObject);
                }
            }
        }

        private float CalculateDamage(Transform enemyTransform)
        {
            float proximity = (transform.position - enemyTransform.position).magnitude;
            float effect = 1 - (proximity / m_TriggerRadius);
            return effect * m_MaximumDamage;
        }


        private IEnumerator MineTimer()
        {
            yield return new WaitForSeconds(m_MineTimerDuration);
            Explode();
        }
    }
}