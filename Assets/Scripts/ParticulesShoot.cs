using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulesShoot : MonoBehaviour
{
    #region Exposed

    private Vector2 m_generateurPosition;
    private Transform m_generateurTransform;

    #endregion


    #region Unity API

    private void Awake()
    {

        
        
        _nextShotTime = Time.time;
    }

    void Update()
    {
        
        if (Time.time > _nextShotTime)
        {
            _nextShotTime =_delayBetweenShots;
            ShootParticules();
        }
    }

    #endregion


    #region Main Method

    private void ShootParticules()
    {
        m_generateurPosition = transform.position;
        m_generateurPosition += Random.insideUnitCircle * _generatorRadius;
        GameObject newQuad;
        newQuad = Instantiate(_quad, m_generateurPosition, Quaternion.identity);
    }

    #endregion

    #region Privates

    [SerializeField]
    private GameObject _quad;
    [SerializeField]
    private float _delayBetweenShots;
    private float _nextShotTime;
    [SerializeField]
    private float _generatorRadius;


    #endregion
}
