using System;
using System.Collections.Generic;

namespace AquelaFrameWork.Sound
{
    public class AFSoundGroup
    {
        protected string m_name = "";

        public Dictionary<string, AFSound> m_audios;

        public string GetName()
        {
            return m_name;
        }

        public AFSoundGroup( string name )
        {
            m_name = name;
            m_audios = new Dictionary<string, AFSound>();
        }


        public void Add( AFSound sound )
        {
            m_audios[ sound.GetName() ] = sound;
        }

        public AFSound GetSound( string name )
        {
            return m_audios[name];
        }

        public AFSound GetSound( bool isRandom = false )
        {
            string key;

            if( isRandom )
            {
                List<string> keys = new List<string>(m_audios.Keys);
                int size = m_audios.Count;
                Random rand = new Random();
                key = keys[rand.Next(size)];
            }
            else
            {
                key = m_audios.Keys.GetEnumerator().Current;
            }

            return m_audios[key];
        }

    }
}
