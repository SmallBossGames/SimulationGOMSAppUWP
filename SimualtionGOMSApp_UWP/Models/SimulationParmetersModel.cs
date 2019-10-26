using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimualtionGOMSApp_UWP.Models
{
    public class SimulationParmetersModel : NotificatedModel
    {
        private double menthal;
        private double handMoving;
        private double keyboard;
        private double positioning;

        private double minError;
        private double maxError;
        private double stepError;

        public double Keyboard
        {
            get => keyboard;
            set => SetMember(ref keyboard, value);
        }

        public double Positioning
        {
            get => positioning;
            set => SetMember(ref positioning, value);
        }

        public double HandMoving
        {
            get => handMoving;
            set => SetMember(ref handMoving, value);
        }

        public double Menthal
        {
            get => menthal;
            set => SetMember(ref menthal, value);
        }

        public double MinError
        {
            get => minError;
            set => SetMember(ref minError, value);
        }

        public double MaxError
        {
            get => maxError;
            set => SetMember(ref maxError, value);
        }

        public double StepError
        {
            get => stepError;
            set => SetMember(ref stepError, value);
        }
    }
}
