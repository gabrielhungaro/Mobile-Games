using UnityEngine;
using System.Collections;

namespace com.globo.sitio.mobilegames.Balloon
{
    public class Balloon : MonoBehaviour {

        private string _type;
        private float _velocity;
        private int _touchesToExplode;
        public Sprite _sprite;
        private string _spriteName;
        private string _spritePath;

        private int _pointsToAdd;
        private int _pointsToRemove;
        private int _timeToAdd;
        private int _timeToRemove;
        private bool _isTouching;
        private bool _isGood;
        private bool _canExplode = true;
        public bool _canBeDestroyed;


	    // Use this for initialization
	    void Start () {
            _canBeDestroyed = false;
	    }

        public void LoadSprite()
        {
            _spritePath = _spritePath + _spriteName + "/";

            _sprite = Resources.Load<Sprite>(_spritePath + _spriteName + "0001");//_spriteName);
            //_sprite = Resources.Load<Sprite>(_spritePath + _spriteName + "/" + _spriteName + "0001");//_spriteName);

            this.gameObject.AddComponent<SpriteRenderer>();
            this.gameObject.GetComponent<SpriteRenderer>().sprite = _sprite;
            this.gameObject.AddComponent<BoxCollider>().size = new Vector3(this.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x, this.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y);
        }
	
	    // Update is called once per frame
	    void Update () {
	
	    }

        public void SetType(string value)
        {
            _type = value;
        }

        public string GetType()
        {
            return _type;
        }

        public void SetVelocity(float value)
        {
            _velocity = value;
        }

        public float GetVelocity()
        {
            return _velocity;
        }

        public void SetNumberOfTouchesToExplode(int value)
        {
            _touchesToExplode = value;
        }

        public int GetNumberOfTouchesToExplode()
        {
            return _touchesToExplode;
        }

        public void SetSpritePath(string value)
        {
            _spritePath = value;
        }

        public string GetSpritePath()
        {
            return _spritePath;
        }

        public void SetSpriteName(string value)
        {
            _spriteName = value;
        }

        public string GetSpriteName()
        {
            return _spriteName;
        }

        public void SetSprite(Sprite value)
        {
            _sprite = value;
        }

        public Sprite GetSprite()
        {
            return _sprite;
        }

        public void SetIsTouching(bool value)
        {
            _isTouching = value;
        }

        public bool GetIsTouching()
        {
            return _isTouching;
        }

        public void SetIsGood(bool value)
        {
            _isGood = value;
        }

        public bool GetIsGood()
        {
            return _isGood;
        }

        public void SetCanExplode(bool value)
        {
            _canExplode = value;
        }

        public bool GetCanExplode()
        {
            return _canExplode;
        }

        public void SetCanBeDestroyed(bool value)
        {
            _canBeDestroyed = value;
        }

        public bool GetCanBeDestroyed()
        {
            return _canBeDestroyed;
        }

    }
}