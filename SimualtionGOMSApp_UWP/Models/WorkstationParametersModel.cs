using SimualtionGOMSApp_UWP.GOMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimualtionGOMSApp_UWP.Models
{
    public class WorkstationParametersModel : NotificatedModel
    {
        private string rootNodeName;
        private double expectedValue;
        private double lossRatio;

        public string RootNodeName { get => rootNodeName; set => SetMember(ref rootNodeName, value); }
        public double ExpectedValue { get => expectedValue; set => SetMember(ref expectedValue, value); }
        public double LossRatio { get => lossRatio; set => SetMember(ref lossRatio, value); }
    }
}
