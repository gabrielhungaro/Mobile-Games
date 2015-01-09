using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using AquelaFrameWork.Core;

namespace com.globo.sitio.mobilegames.Balloon
{
    public class MovementSystem : ASingleton<MovementSystem>
    {
        private List<GameObject> _listOfObjectsWithMoveComponent;
        private GameController _gameController;
        private float _screenLimite;
        private float _velocity;

        public void Initialize()
        {
            _listOfObjectsWithMoveComponent = new List<GameObject>();
            _gameController = this.gameObject.GetComponent<GameController>();
            _screenLimite = (Screen.height / 50f);
        }

        public override void AFUpdate(double time)
        {
            foreach (GameObject obj in _listOfObjectsWithMoveComponent)
            {
                if (obj != null)
                {
                    if (obj.activeInHierarchy == true)
                    {
                        switch (TimeController.GetTimeMode())
                        {
                            case TimeController.FAST_TIME:
                                _velocity = obj.GetComponent<MoveComponent>().GetVelocity() * TimeController.GetTimeScaleFactor();
                                break;
                            case TimeController.SLOW_TIME:
                                _velocity = obj.GetComponent<MoveComponent>().GetVelocity() / TimeController.GetTimeScaleFactor();
                                break;
                            default:
                                _velocity = obj.GetComponent<MoveComponent>().GetVelocity();
                                break;
                        }
                        obj.transform.Translate(new Vector3(0, _velocity));
                    }
                }
            }
        }

        public void AddObjectToList(GameObject g)
        {
            _listOfObjectsWithMoveComponent.Add(g);
        }

        public List<GameObject> GetListOfObject()
        {
            return _listOfObjectsWithMoveComponent;
        }

        public void RemoveObjectToList(GameObject g)
        {
            for (int i = 0; i <= _listOfObjectsWithMoveComponent.Count; i++)
            {
                if (_listOfObjectsWithMoveComponent[i].name == g.name)
                {
                    _listOfObjectsWithMoveComponent.Remove(g);
                    return;
                }
            }
            Debug.LogWarning("Objeto " + g.name + "não existe na lista de MovementSystem");
        }
    }
}
