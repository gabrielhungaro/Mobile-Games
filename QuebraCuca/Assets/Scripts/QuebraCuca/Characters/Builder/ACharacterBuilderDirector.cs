using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.globo.sitio.mobilegames.QuebraCuca.Characters
{
    public abstract class ACharacterBuilderDirector
    {
        public ACharacterBuilder m_builder;

        public ACharacterBuilderDirector( ACharacterBuilder builder )
        {
            m_builder = builder;
        }

        virtual public Character GetCharacter()
        {
            throw new InvalidOperationException("This Method must be overridden");
            return null;
        }
    }
}
