using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadBehaviour : MonoBehaviour
{

    #region Exposed

    public Rigidbody2D m_quadRigidbody;
    public float m_startSpeed;
    public float m_timeToStop;

    #endregion


    #region Unity API

    private void Awake()
    {
        m_quadRigidbody = GetComponent<Rigidbody2D>();
        
    }

    private void FixedUpdate()
    {
        m_quadRigidbody.AddForce(new Vector2(m_startSpeed *Time.deltaTime, 0), ForceMode2D.Impulse);

        
        Destroy(gameObject, 10);
    }

    #endregion


    #region Private



    #endregion
}
