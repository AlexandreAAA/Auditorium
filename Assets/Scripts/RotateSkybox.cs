using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Auditorium
{
    public class RotateSkybox : MonoBehaviour
    {
        private void Update()
        {
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotatespeed);
        }

        [SerializeField]
        private float rotatespeed;
    }
}

