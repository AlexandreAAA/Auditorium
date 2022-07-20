using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    #region Exposed

    public AudioSource[] m_hajime;
    public GameObject m_messagWwin;

    #endregion


    #region Unity API

    private void Awake()
    {
        m_hajime = FindObjectsOfType<AudioSource>();
        _timeToWin = Time.time;
        
        
        
    }

    private void Start()
    {
        for(int i = 0; i < m_hajime.Length; i++)
        { 
            m_hajime[i].Play();
        }
    }

    public void YouWin()
    {
        //Time.timeScale = 0;
        m_messagWwin.SetActive(true);
        Debug.Log("GG!");
    }

    private void Update()
    {
        foreach (AudioSource _audio in m_hajime)
        {
            if (_audio.volume >= 0.90f)
            {
                _check++;
            }
            else
            {
                _check = 0;
            }
        }

        if (_check >= m_hajime.Length)
        {
            YouWin();
        }

    }
    #endregion


    #region Main Method
    
    

    #endregion

    #region Privates

    private float _timeToWin;
    private float _check;

    #endregion
}
