using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncVolume : MonoBehaviour
{

    #region Exposed

    public Renderer[] m_volState;
    public AudioSource m_audioClip;

    #endregion


    #region unity API

    private void Awake()
    {
        m_volState = GetComponentsInChildren<Renderer>();
        
        m_audioClip = GetComponent<AudioSource>();

        

        
    }

    private void Update()
    {
        if (m_audioClip.volume >= 0.20f)
        {
            m_volState[0].material = _on;
        }
        else
        {
            m_volState[0].material = _off;
        }

        if (m_audioClip.volume >= 0.40f)
        {
            m_volState[1].material = _on;
        }
        else
        {
            m_volState[1].material = _off;
        }
        if (m_audioClip.volume >= 0.60f)
        {
            m_volState[2].material = _on;
        }
        else
        {
            m_volState[2].material = _off;
        }
        if (m_audioClip.volume >= 0.60f)
        {
            m_volState[3].material = _on;
        }
        else
        {
            m_volState[3].material = _off;
        }
        if (m_audioClip.volume >= 0.80f)
        {
            m_volState[4].material = _on;
        }
        else
        {
            m_volState[4].material = _off;
        }
    }

    #endregion


    #region Privates 

    [SerializeField]
    private Material _on;
    [SerializeField]
    private Material _off;


    #endregion
}
