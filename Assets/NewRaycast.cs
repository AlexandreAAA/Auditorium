using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRaycast : MonoBehaviour
{

    #region Exposed

    [SerializeField]
    Texture2D _moveIcon;

    [SerializeField]
    Texture2D _resizeIcon;

    [SerializeField]
    LayerMask _effectorLayer;

    #endregion


    #region Unity API

    private void Update()
    {
        Ray _ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        RaycastHit2D _hit = Physics2D.GetRayIntersection(_ray, Mathf.Infinity, _effectorLayer);

        if (_hit.collider == null)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            return;
        }

        if (_hit.collider != null)
        {
            Collider2D hoverItem = _hit.collider;

            if (hoverItem.CompareTag("MoveZone"))
            {
                ChangeCursorIcon(_moveIcon);

                if(Input.GetMouseButtonDown(0))
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

        if (_effectorToMove != null)
        {
            _effectorToMove.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if( Input.GetMouseButtonUp(0))
            {
                _effectorToMove = null;
            }
        }

        if ( _effectorToResize != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
            float currentDistance = Vector2.Distance( mousePosition, _effectorToResize.transform.position );
            float difference = currentDistance - _lastframeDistance;
            float newRadius =_effectorToResize.Radius + difference;
            _effectorToResize.Radius = Mathf.Clamp(newRadius, 1f, 5f);
            _effectorToResize.GetComponent<AreaEffector2D>().forceMagnitude = _effectorToResize.Radius + 5;
            _lastframeDistance = currentDistance;

            if ( Input.GetMouseButtonUp(0))
            {
                _effectorToResize = null;
            }
        }
    }

    #endregion


    #region Main Method
    
    private void ChangeCursorIcon( Texture2D iconToChange, float anchorRatio = 0.5f)
    {
        Vector2 hotspot = new Vector2( iconToChange.width, iconToChange.height ) * anchorRatio;
        Cursor.SetCursor(iconToChange, hotspot, CursorMode.Auto);
    }

    #endregion


    #region¨Privates

    Transform _effectorToMove = null;
    CircleShape _effectorToResize = null;
    float _lastframeDistance;

    #endregion
}
