using UnityEngine;
using System.Collections;

using UnityEngine;
using AquelaFrameWork.Core;
using AquelaFrameWork.Core.Asset;

namespace com.globo.sitio.mobilegames.QuebraCuca.Characters
{ 
    public class CucaHitedParticleEmiter : AFObject 
    {
        private GameObject m_particleEmiter;

        public void Initialzie()
        {
           m_particleEmiter.gameObject.transform.parent = gameObject.transform;
        }

        public override void AFUpdate(double time)
        {

            if( Input.GetKeyDown("up"))
                m_particleEmiter.particleSystem.Play(true);

            base.AFUpdate(time);
        }

    }
}
