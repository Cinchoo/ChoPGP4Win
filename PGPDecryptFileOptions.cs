using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChoPGP4Win
{
    public sealed class PGPDecryptFileOptions : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public PGPDecryptFileOptions()
        {
#if DEBUG
            InputFilePath = @"SampleData.pgp";
            OutputFilePath = @"SampleData.out";
            PrivateKeyFilePath = @"Sample_pri.asc";
            PassPhrase = @"Test123";
#endif
        }
    }
}
