using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using com.globo.sitio.mobilegames.QuebraCuca.Characters;

namespace com.globo.sitio.mobilegames.QuebraCuca.Controllers
{
    public class IndexController : MonoBehaviour
    {
        private List<GameObject> _listOfObjects;
        private int _numberOfIndex = 4;
        private Dictionary<int, List<GameObject>> _listOfLayers;
        private List<GameObject> _listOfFirstLayerObjects;
        private List<GameObject> _listOfSecondLayerObjects;
        private List<GameObject> _listOfThirdLayerObjects;
        private List<GameObject> _listOfFouthLayerObjects;

        public void Start()
        {
            _listOfObjects = new List<GameObject>();
            _listOfLayers = new Dictionary<int, List<GameObject>>();
            _listOfLayers.Add(1, _listOfFirstLayerObjects = new List<GameObject>());
            _listOfLayers.Add(2, _listOfSecondLayerObjects = new List<GameObject>());
            _listOfLayers.Add(3, _listOfThirdLayerObjects = new List<GameObject>());
            _listOfLayers.Add(4, _listOfFouthLayerObjects = new List<GameObject>());
        }

        public void AddObjectToList(GameObject obj)
        {
            _listOfObjects.Add(obj);
        }

        public void AddObjectToLIstByIndex(GameObject obj, int index)
        {
            _listOfLayers[index].Add(obj);
            SortIndexOfObjects();
        }

        public void SortIndexOfObjects()
        {
            for (int i = 1; i <= _listOfLayers.Count; i++)
            {
                foreach (GameObject obj in _listOfLayers[i])
                {
                    if (obj.GetComponent<Character>())
                    {
                        obj.GetComponent<UIWidget>().depth = i;
                    }
                    else
                    {
                        obj.GetComponent<UIWidget>().depth = i+1;
                    }
                }
            }
        }
    }
}
