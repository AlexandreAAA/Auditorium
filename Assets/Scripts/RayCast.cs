using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Auditorium
{

    public class RayCast : MonoBehaviour
    {
        #region Exposed

        public Texture2D m_iconMove;
        public Texture2D m_iconResize;
        public CursorMode m_cursorMode = CursorMode.Auto;
        public Vector3 m_mousePosition;
        public CircleCollider2D m_effectorCollider;
        public Transform m_effectorTransform;
        public Vector3 m_effectorPosition;
        public GameObject m_effectorActif;
        public float m_circleShapeRadius;
        #endregion


        #region Unity API

        private void Awake()
        {
            m_effectorCollider = GetComponent<CircleCollider2D>();
            m_effectorPosition = GetComponent<Transform>().position;
            m_circleShapeRadius = GetComponent<CircleShape>().Radius;
        }
        private void Update()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(_ray.origin, _ray.direction);
            _hit = Physics2D.GetRayIntersection(_ray);
            float distance = (_hit.point - new Vector2(transform.position.x, transform.position.y)).magnitude;
            if (_hit != false)
            {

                if (_hit.collider != null && _hit.collider.CompareTag("Effector"))
                {
                    if (distance < _fleche.radius)
                    {
                        Vector2 hotSpot = new Vector2(m_iconMove.width / 2, m_iconMove.height / 2);
                        Cursor.SetCursor(m_iconMove, hotSpot, m_cursorMode);
                    }
                    else
                    {
                        Vector2 hotSpot = new Vector2(m_iconResize.width / 2, m_iconResize.height / 2);
                        Cursor.SetCursor(m_iconResize, hotSpot, m_cursorMode);
                    }
                }
            }
            else
                Cursor.SetCursor(null, Vector2.zero, m_cursorMode);

            if (_hit.collider != null && _hit.collider.CompareTag("Effector") && Input.GetMouseButtonDown(0))
            {

                if (_hit.transform.position == transform.position)
                {
                    m_effectorActif = gameObject;
                }
            }

            else if (_hit.collider != null && _hit.collider.CompareTag("Effector") && Input.GetMouseButtonUp(0))
            {
                m_effectorActif = null;
            }

            if (_hit.collider != null && distance < _fleche.radius && Input.GetMouseButton(0))
            {
                m_mousePosition = _hit.point;
                Vector3 deltaMouseEffector = m_mousePosition - m_effectorActif.transform.position;
                m_effectorActif.transform.position = new Vector3(_ray.origin.x, _ray.origin.y, 0);
            }


            if (_hit.collider != null && distance > _fleche.radius && Input.GetMouseButtonDown(0))
            {
                _radiusStart = m_effectorActif.GetComponent<CircleShape>().Radius;
                _mousePositionStart = _hit.point;
            }
            else if (_hit.collider != null && distance > _fleche.radius && Input.GetMouseButton(0))
            {
                float moveDistance = Vector3.Distance(_mousePositionStart, _hit.point);
                float effectorRadius = _radiusStart + moveDistance;
                m_effectorActif.GetComponent<CircleShape>().Radius = Mathf.Clamp(effectorRadius, 0.01f, 5f);
                m_effectorActif.GetComponent<AreaEffector2D>().forceMagnitude = m_effectorActif.GetComponent<CircleShape>().Radius + 10f;
            }
        }



        #endregion


        #region Privates

        private Ray _ray;
        private RaycastHit2D _hit;
        [SerializeField]
        public CircleCollider2D _fleche;
        private float _radiusStart;
        private Vector3 _mousePositionStart;

        #endregion

    }

}