using StellarisEditor.ScriptEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    public class PdxTechnologyTier : PdxObject
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        private String _Key;
        public String Key
        {
            get
            {
                return _Key;
            }
            set
            {
                _Key = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Key"));
            }
        }

        public String _PreviouslyUnlocked;
        public String PreviouslyUnlocked
        {
            get
            {
                return _PreviouslyUnlocked;
            }
            set
            {
                _PreviouslyUnlocked = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PreviouslyUnlocked"));
            }
        }


        private String _FileName;
        public String FileName
        {
            get
            {
                return _FileName;
            }
            set
            {
                _FileName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FileName"));
            }
        }

        public static PdxTechnologyTier Parse(ObjectStatement statement)
        {
            return new PdxTechnologyTier() { Key = statement.Key, PreviouslyUnlocked = statement.Statements.Count == 0 ? "" : (statement.Statements.First() as AssignmentStatement).Value.Content };
        }

        public override bool Equals(object obj) => obj is PdxTechnologyTier tier && Key == tier.Key && PreviouslyUnlocked == tier.PreviouslyUnlocked;

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => $"{Key} = {{ {(String.IsNullOrWhiteSpace(PreviouslyUnlocked) ? "" : "previously_unlocked = " + PreviouslyUnlocked)} }}";

        public PdxTechnologyTier Clone() => new PdxTechnologyTier() { Key = Key, PreviouslyUnlocked = PreviouslyUnlocked, IsModData = IsModData };
    }
}
