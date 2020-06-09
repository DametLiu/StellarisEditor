using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    public class PdxModTag : PdxObject
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        private String _Loclization_English;
        public String Loclization_English
        {
            get
            {
                return _Loclization_English;
            }
            set
            {
                _Loclization_English = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Loclization_English"));
            }
        }

        private String _Locliaztion_Chinese;
        public String Locliaztion_Chinese
        {
            get
            {
                return _Locliaztion_Chinese;
            }
            set
            {
                _Locliaztion_Chinese = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Locliaztion_Chinese"));
            }
        }

        private Boolean _IsChecked;
        public Boolean IsChecked {
            get
            {
                return _IsChecked;
            }
            set
            {
                _IsChecked = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsChecked"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedTags"));
            }
        }
        
        public PdxModTag(String english, String chinese, Boolean isChecked)
        {
            Loclization_English = english;
            Locliaztion_Chinese = chinese;
            IsChecked = isChecked;
        }

        public override string ToString()
        {
            return $"\"{Loclization_English}\"";
        }
    }
}
