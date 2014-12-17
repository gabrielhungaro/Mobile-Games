using UnityEngine;
using System.Collections;

namespace com.globo.sitio.mobilegames.QuebraCuca.Controllers
{
    public class CameraSizeHandler : MonoBehaviour
    {

        //public Camera cam;

        // Use this for initialization
        void Start()
        {

            Resolution res = Screen.currentResolution;
            Camera cam = this.gameObject.GetComponent<Camera>();
            cam.orthographicSize = (res.height * 0.5f) * 0.01f;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}