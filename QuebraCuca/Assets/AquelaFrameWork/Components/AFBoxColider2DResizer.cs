using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

using AquelaFrameWork.Core;

namespace AquelaFrameWork.Components
{
    [RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
    public class AFBoxColider2DResizer : AFObject
    {
        [SerializeField]
        protected BoxCollider2D m_collider;

        [SerializeField]
        protected SpriteRenderer m_spriteRender;

        virtual public void Update()
        {
            m_collider = gameObject.GetComponent<BoxCollider2D>();
            m_spriteRender = gameObject.GetComponent<SpriteRenderer>();

            if (m_collider && m_spriteRender && m_spriteRender.sprite)
            { 
                Vector2 L_colliderSize = m_spriteRender.sprite.bounds.size;
                m_collider.size = L_colliderSize;
                m_collider.center = new Vector2(0, 0);
            }
        }
    }
}
