using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquelaFrameWork.View
{
    /**  
    *  The IAnimatable interface describes objects that are animated depending on the passed time. 
    *   Any object that implements this interface can be added to a juggler.
    */
    interface IAnimatable
    {
        void AdvanceTime(double time);
    }
}
