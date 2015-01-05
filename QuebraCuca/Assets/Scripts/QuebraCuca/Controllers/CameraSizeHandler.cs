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
#if UNITY_EDITOR
            
            Camera cam = this.gameObject.GetComponent<Camera>();
            cam.orthographicSize = (1536 * 0.5f) * 0.01f;

#else
            Resolution res = Screen.currentResolution;
            Camera cam = this.gameObject.GetComponent<Camera>();
            cam.orthographicSize = (res.height * 0.5f) * 0.01f;
#endif //UNITY_EDITOR

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}