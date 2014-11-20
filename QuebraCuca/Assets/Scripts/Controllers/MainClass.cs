using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Controllers;
using Constants;
using Scenes;

namespace Controllers
{
    public class MainClass : MonoBehaviour
    {
        ResolutionController _resolutionController;
        PathConstants _paths;
        SoundConstants _sounds;
        SceneManager _sceneManager;

        void Start()
        {
            _sceneManager = SceneManager.Instance();
            SceneManager.ChangeScene(SceneManager.START_SCENE);
            _resolutionController = ResolutionController.Instance();
            _paths = PathConstants.Instance();
            _sounds = SoundConstants.Instance();
            this.gameObject.AddComponent<SoundManager>().Init();
            //SoundManager.PlaySoundByName(SoundConstants.BG_SOUND);
        }
    }
}
