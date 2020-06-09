using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    public abstract class PdxObject : INotifyPropertyChanged
    {
        private bool _IsModData;
        public bool IsModData
        {
            get
            {
                return _IsModData;
            }
            set
            {
                _IsModData = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsModData"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
