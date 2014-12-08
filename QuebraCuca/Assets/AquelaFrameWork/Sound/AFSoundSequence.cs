using System;
using System.Collections.Generic;

using UnityEngine;

namespace AquelaFrameWork.Sound
{
    public class AFSoundSequence : MonoBehaviour
    {
        protected List<AFSound> m_soundSequence;
        protected AFSound m_currentSound;
        protected int m_nextSoundIndex = 0;

        protected bool m_isPlaying = false;

        public AFSoundSequence( object[] objects )
        {

        }

        public AFSoundSequence( AFSound[] objects )
        {

        }
        public AFSoundSequence(AudioClip[] objects)
        {

        }

        public void Add( AFSound audio )
        {
            //TODO: Add audio to a list and verify if it is valid
        }

        public void Play()
        {

        }

        public void Stop()
        {

        }

        public void Pause()
        {

        }




    }
}
