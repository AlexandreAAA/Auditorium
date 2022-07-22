using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Auditorium
{
    public class BoxCollision : MonoBehaviour
    {
        #region Exposed

        public AudioSource m_audioClip;
        [SerializeField]
        private float _delayBetweenDecay;

        #endregion

        #region Unity API

        private void Awake()
        {
            m_audioClip = GetComponent<AudioSource>();
            m_audioClip.volume = 0f;
            _timeBeforeDecay = Time.time;
        }

        private void Update()
        {
            if (Time.time > _timeBeforeDecay)
            {
                _timeBeforeDecay = Time.time + _delayBetweenDecay;
                m_audioClip.volume -= 0.05f;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("quad"))
            {
                m_audioClip.volume += 0.01f;
            }
        }

        #endregion

        #region Privates
        private float _timeBeforeDecay;

        #endregion
    }
}