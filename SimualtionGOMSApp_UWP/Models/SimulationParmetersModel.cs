using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimualtionGOMSApp_UWP.Models
{
    public class SimulationParmetersModel : INotifyPropertyChanged
    {
        private double menthal;
        private double handMoving;
        private double keyboard;
        private double positioning;

        private double minError;
        private double maxError;
        private double stepError;

        public event PropertyChangedEventHandler PropertyChanged;

        public double Keyboard
        {
            get => keyboard;
            set
            {
                keyboard = value;
                OnPropertyChanged();
            }
        }

        public double Positioning
        {
            get => positioning;
            set
            {
                positioning = value;
                OnPropertyChanged();
            }
        }

        public double HandMoving
        {
            get => handMoving;
            set
            {
                handMoving = value;
                OnPropertyChanged();
            }
        }

        public double Menthal
        {
            get => menthal;
            set
            {
                menthal = value;
                OnPropertyChanged();
            }
        }

        public double MinError
        {
            get => minError;
            set
            {
                minError = value;
                OnPropertyChanged();
            }
        }

        public double MaxError
        {
            get => maxError;
            set
            {
                maxError = value;
                OnPropertyChanged();
            }
        }

        public double StepError
        {
            get => stepError;
            set
            {
                stepError = value;
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
