using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimualtionGOMSApp_UWP.Models
{
    public abstract class NotificatedModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void SetMember<T>(ref T member, T value, [CallerMemberName] string memberName = "")
        {
            member = value;
            OnPropertyChanged(memberName);
        }
    }
}
