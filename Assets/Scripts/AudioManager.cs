using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Auditorium
{
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
        }

        private void Start()
        {
            for (int i = 0; i < m_hajime.Length; i++)
            {
                m_hajime[i].Play();
            }
        }

        public void YouWin()
        {
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

        #region Privates
        private float _check;

        #endregion
    }
}
