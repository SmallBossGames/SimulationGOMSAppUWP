using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimualtionGOMSApp_UWP.GOMS
{
    public readonly struct OuterNode
    {
        public OuterNode(bool isEndNode, Token[] gomsTokens)
        {
            IsEndNode = isEndNode;
            GomsTokens = gomsTokens;
        }

        public bool IsEndNode { get; }
        public Token[] GomsTokens { get; }

        public double GetTime(in Parameters parameters)
        {
            var sum = 0.0;
            foreach (var item in GomsTokens)
            {
                switch (item)
                {
                    case Token.keyboard:
                        sum += parameters.Keyboard;
                        break;
                    case Token.positioning:
                        sum += parameters.Positioning;
                        break;
                    case Token.handMoving:
                        sum += parameters.HandMoving;
                        break;
                    case Token.menthal:
                        sum += parameters.Menthal;
                        break;
                    default:
                        break;
                }
            }
            return sum;
        }
    }

    public enum Token : byte
    {
        keyboard, positioning, handMoving, menthal
    }
}
