using System.ComponentModel;

namespace Detextive.ViewModel
{
    public class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotificarCambio(string sNameAtributo)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(sNameAtributo));
            }
        }
    }
}