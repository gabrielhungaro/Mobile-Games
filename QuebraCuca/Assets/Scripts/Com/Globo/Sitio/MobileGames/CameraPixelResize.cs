using UnityEngine;
using System.Collections;
namespace Com.Globo.Sitio.MobileGames.Balloon
{

    public class CameraPixelResize : MonoBehaviour
    {

        void Awake()
        {
            //camera.orthographicSize = Screen.height / 2f;
            Resolution res = Screen.currentResolution;
            //Debug.Log("Resolution " + res.width);
            Camera cam = this.gameObject.GetComponent<Camera>();
            cam.orthographicSize = (res.width * 0.5f) * 0.01f;

        }


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
