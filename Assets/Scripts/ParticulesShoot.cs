using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Auditorium
{
    public class ParticulesShoot : MonoBehaviour
    {
        #region Exposed

        [SerializeField]
        private GameObject _quad;
        [SerializeField]
        private float _delayBetweenShots;
        [SerializeField]
        private float _generatorRadius;

        #endregion

        #region Unity API

        private void Awake()
        {
            _nextShotTime = Time.time;
        }

        private void Update()
        {
            if (Time.time > _nextShotTime)
            {
                ShootParticules();
            }
        }

        #endregion

        #region Main Method

        private void ShootParticules()
        {
            _nextShotTime = _delayBetweenShots;
            m_generateurPosition = transform.position;
            m_generateurPosition += Random.insideUnitCircle * _generatorRadius;
            GameObject newQuad = Instantiate(_quad, m_generateurPosition, Quaternion.identity);
        }

        #endregion

        #region Privates

        private float _nextShotTime;

        private Vector2 m_generateurPosition;

        #endregion
    }
}
