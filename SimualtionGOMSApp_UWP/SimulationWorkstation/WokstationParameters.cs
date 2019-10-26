using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimualtionGOMSApp_UWP.GOMS;

namespace SimualtionGOMSApp_UWP.SimulationWorkstation
{
    public readonly struct WokstationParameters: IEquatable<WokstationParameters>
    {
        public WokstationParameters(Node rootNode, double expectedValue, double lossRatio)
        {
            RootNode = rootNode ?? throw new ArgumentNullException(nameof(rootNode));
            ExpectedValue = expectedValue;
            LossRatio = lossRatio;
        }

        public Node RootNode { get; }
        public double ExpectedValue { get; }
        public double LossRatio { get; }

        public bool Equals(WokstationParameters other)
        {
            return
                RootNode.Equals(other.RootNode) ||
                ExpectedValue.Equals(other.ExpectedValue) ||
                LossRatio.Equals(other.LossRatio);
        }

        public override bool Equals(object obj)
        {
            return obj is WokstationParameters && Equals((WokstationParameters)obj);
        }

        public override int GetHashCode()
        {
            return RootNode.GetHashCode() + ExpectedValue.GetHashCode() + LossRatio.GetHashCode();
        }

        public static bool operator ==(WokstationParameters left, WokstationParameters right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(WokstationParameters left, WokstationParameters right)
        {
            return !(left == right);
        }
    }
}
