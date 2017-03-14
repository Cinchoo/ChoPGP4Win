using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChoPGP4Win
{
    public sealed class PGPGenerateKeyOptions : INotifyPropertyChanged
    {
        private string _publicKeyFilePath;
        public string PublicKeyFilePath
        {
            get { return _publicKeyFilePath; }
            set
            {
                if (_publicKeyFilePath != value)
                {
                    _publicKeyFilePath = System.IO.Path.GetFullPath(value); ;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _privateKeyFilePath;
        public string PrivateKeyFilePath
        {
            get { return _privateKeyFilePath; }
            set
            {
                if (_privateKeyFilePath != value)
                {
                    _privateKeyFilePath = System.IO.Path.GetFullPath(value); ;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _identity;
        public string Identity
        {
            get { return _identity; }
            set
            {
                if (_identity != value)
                {
                    _identity = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _passPhrase;
        public string PassPhrase
        {
            get { return _passPhrase; }
            set
            {
                if (_passPhrase != value)
                {
                    _passPhrase = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _strength;
        public int Strength
        {
            get { return _strength; }
            set
            {
                if (_strength != value && value > 0)
                {
                    _strength = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _certainty;
        public int Certainty
        {
            get { return _certainty; }
            set
            {
                if (_certainty != value && value > 0)
                {
                    _certainty = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public PGPGenerateKeyOptions()
        {
#if DEBUG
            PublicKeyFilePath = @"Sample_pub1.asc";
            PrivateKeyFilePath = @"Sample_pri1.asc";
            Identity = @"Mark@gmail.com";
            PassPhrase = @"Test123";
#endif
            Strength = ChoAppSettings.GetValue<int>("Strength", 1024, true);
            Certainty = ChoAppSettings.GetValue<int>("Certainty", 8, true);
        }
    }
}
