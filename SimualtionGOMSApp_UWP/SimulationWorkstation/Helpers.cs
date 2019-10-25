using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimualtionGOMSApp_UWP.SimulationWorkstation
{
    public static class Helpers
    {
        public static double NextExponential(this Random random, double math)
        {
            if (random == null)
                throw new NullReferenceException();

            return -math * Math.Log(random.NextDouble());
        }
    }
}
