using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChoPGP4Win
{
    public sealed class PGPEncryptFileOptions : INotifyPropertyChanged
    {
        private string inputFilePath;
        public string InputFilePath
        {
            get { return inputFilePath; }
            set
            {
                if (inputFilePath != value)
                {
                    inputFilePath = System.IO.Path.GetFullPath(value);
                    NotifyPropertyChanged();
                }
            }
        }
        private string outputFilePath;
        public string OutputFilePath
        {
            get { return outputFilePath; }
            set
            {
                if (outputFilePath != value)
                {
                    outputFilePath = System.IO.Path.GetFullPath(value); ;
                    NotifyPropertyChanged();
                }
            }
        }
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
        private bool _armor;
        public bool Armor
        {
            get { return _armor; }
            set
            {
                if (_armor != value)
                {
                    _armor = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool _integrityCheck;
        public bool IntegrityCheck
        {
            get { return _integrityCheck; }
            set
            {
                if (_integrityCheck != value)
                {
                    _integrityCheck = value;
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

        public PGPEncryptFileOptions()
        {
#if DEBUG
            InputFilePath = @"SampleData.txt";
            OutputFilePath = @"SampleData.pgp";
            PublicKeyFilePath = @"Sample_pub.asc";
            PrivateKeyFilePath = @"Sample_pri.asc";
            PassPhrase = @"Test123";
#endif
            Armor = ChoAppSettings.GetValue<bool>("Armor", true, true);
            IntegrityCheck = ChoAppSettings.GetValue<bool>("IntegrityCheck", true, true);
        }
    }
}
