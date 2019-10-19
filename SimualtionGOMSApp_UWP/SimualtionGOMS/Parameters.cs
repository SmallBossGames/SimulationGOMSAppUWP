using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimualtionGOMSApp_UWP.SimualtionGOMS
{
    public readonly struct Parameters
    {
        public Parameters(double keyboard, double positioning, double handMoving, double menthal)
        {
            Keyboard = keyboard;
            Positioning = positioning;
            HandMoving = handMoving;
            Menthal = menthal;
        }

        public double Keyboard { get; }
        public double Positioning { get; }
        public double HandMoving { get; }
        public double Menthal { get; }
    }
}
