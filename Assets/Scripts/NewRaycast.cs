using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Auditorium
{
    public class NewRaycast : MonoBehaviour
    {
        #region Exposed

        [SerializeField]
        private Texture2D _moveIcon;

        [SerializeField]
        private Texture2D _resizeIcon;

        [SerializeField]
        private LayerMask _effectorLayer;

        #endregion

        #region Unity API

        private void Update()
        {
            PlayerInput();
            ManageCursor();
            MoveEffector();
            ResizeEffector();
        }

        #endregion

        #region Main Method

        private void PlayerInput()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            _hit = Physics2D.GetRayIntersection(_ray, Mathf.Infinity, _effectorLayer);
        }

        private void ResizeEffector()
        {
            if (_effectorToResize != null)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float currentDistance = Vector2.Distance(mousePosition, _effectorToResize.transform.position);
                float difference = currentDistance - _lastframeDistance;
                float newRadius = _effectorToResize.Radius + difference;
                _effectorToResize.Radius = Mathf.Clamp(newRadius, 1f, 5f);
                _effectorToResize.GetComponent<AreaEffector2D>().forceMagnitude = _effectorToResize.Radius + 5;
                _lastframeDistance = currentDistance;

                if (Input.GetMouseButtonUp(0))
                {
                    _effectorToResize = null;
                }
            }
        }

        private void MoveEffector()
        {
            if (_effectorToMove != null)
            {
                _effectorToMove.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (Input.GetMouseButtonUp(0))
                {
                    _effectorToMove = null;
                }
            }
        }
        private void ManageCursor()
        {
            if (_hit.collider == null)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                return;
            }

            if (_hit.collider != null)
            {
                hoverItem = _hit.collider;

                if (hoverItem.CompareTag("MoveZone"))
                {
                    ChangeCursorIcon(_moveIcon);

                    if (Input.GetMouseButtonDown(0))
                    {
                        _effectorToMove = hoverItem.transform.parent;
                    }
                }

                if (hoverItem.CompareTag("ResizeZone"))
                {
                    ChangeCursorIcon(_resizeIcon);

                    if (Input.GetMouseButtonDown(0))
                    {
                        _effectorToResize = hoverItem.GetComponent<CircleShape>();
                        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        _lastframeDistance = Vector2.Distance(mousePosition, _effectorToResize.transform.position);
                    }
                }
            }
        }

        private void ChangeCursorIcon(Texture2D iconToChange, float anchorRatio = 0.5f)
        {
            Vector2 hotspot = new Vector2(iconToChange.width, iconToChange.height) * anchorRatio;
            Cursor.SetCursor(iconToChange, hotspot, CursorMode.Auto);
        }

        #endregion

        #region¨Privates

        private Ray _ray;
        private RaycastHit2D _hit;
        private Collider2D hoverItem;

        private Transform _effectorToMove = null;
        private CircleShape _effectorToResize = null;
        private float _lastframeDistance;

        #endregion
    }
}
