using Cinchoo.PGP;
using Microsoft.Win32;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChoPGP4Win
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PGPOptions PGPOptions
        {
            get;
            set;
        }

        public PGPEncryptFileOptions PGPEncryptFileOptions
        {
            get;
            set;
        }

        public PGPDecryptFileOptions PGPDecryptFileOptions
        {
            get;
            set;
        }

        public PGPGenerateKeyOptions PGPGenerateKeyOptions
        {
            get;
            set;
        }
        public MainWindow()
        {
            PGPOptions = new PGPOptions();
            PGPEncryptFileOptions = new PGPEncryptFileOptions();
            PGPDecryptFileOptions = new PGPDecryptFileOptions();
            PGPGenerateKeyOptions = new PGPGenerateKeyOptions();
            DataContext = this;
            InitializeComponent();
            Title = "{0} (v{1})".FormatString(Title, Assembly.GetEntryAssembly().GetName().Version);
        }

        #region Encrypt Methods

        private void btnEncryptInputFilePath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (!String.IsNullOrWhiteSpace(PGPEncryptFileOptions.InputFilePath))
                dlg.InitialDirectory = System.IO.Path.GetDirectoryName(PGPEncryptFileOptions.InputFilePath);

            dlg.Filter = "All files|*.*";

            var result = dlg.ShowDialog();

            if (result == true)
            {
                PGPEncryptFileOptions.InputFilePath = dlg.FileName;
            }
        }

        private void btnEncryptOutputFilePath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (!String.IsNullOrWhiteSpace(PGPEncryptFileOptions.OutputFilePath))
                dlg.InitialDirectory = System.IO.Path.GetDirectoryName(PGPEncryptFileOptions.OutputFilePath);

            dlg.Filter = "All files|*.*";

            var result = dlg.ShowDialog();

            if (result == true)
            {
                PGPEncryptFileOptions.OutputFilePath = dlg.FileName;
            }
        }

        private void btnEncryptPublicKeyFilePath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (!String.IsNullOrWhiteSpace(PGPEncryptFileOptions.PublicKeyFilePath))
                dlg.InitialDirectory = System.IO.Path.GetDirectoryName(PGPEncryptFileOptions.PublicKeyFilePath);

            dlg.Filter = "ASC files|*.asc|All files|*.*";

            var result = dlg.ShowDialog();

            if (result == true)
            {
                PGPEncryptFileOptions.PublicKeyFilePath = dlg.FileName;
            }
        }

        private void btnEncryptPrivateKeyFilePath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (!String.IsNullOrWhiteSpace(PGPEncryptFileOptions.PrivateKeyFilePath))
                dlg.InitialDirectory = System.IO.Path.GetDirectoryName(PGPEncryptFileOptions.PrivateKeyFilePath);

            dlg.Filter = "ASC files|*.asc|All files|*.*";

            var result = dlg.ShowDialog();

            if (result == true)
            {
                PGPEncryptFileOptions.PrivateKeyFilePath = dlg.FileName;
            }
        }

        private async void btnEncryptAndSign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChoPGPEncryptDecrypt pgp = PGPOptions.NewPGPEncryptDecrypt();
                await pgp.EncryptFileAndSignAsync(PGPEncryptFileOptions.InputFilePath, PGPEncryptFileOptions.OutputFilePath, PGPEncryptFileOptions.PublicKeyFilePath, PGPEncryptFileOptions.PrivateKeyFilePath,
                    PGPEncryptFileOptions.PassPhrase, PGPEncryptFileOptions.Armor, PGPEncryptFileOptions.IntegrityCheck);
                MessageBox.Show("PGP Encryption successful.", Title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred during PGP encryption. " + ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChoPGPEncryptDecrypt pgp = PGPOptions.NewPGPEncryptDecrypt();
                await pgp.EncryptFileAsync(PGPEncryptFileOptions.InputFilePath, PGPEncryptFileOptions.OutputFilePath, PGPEncryptFileOptions.PublicKeyFilePath, PGPEncryptFileOptions.Armor, PGPEncryptFileOptions.IntegrityCheck);
                MessageBox.Show("PGP Encryption successful.", Title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred during PGP encryption. " + ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion Encrypt Methods

        #region Decrypt Methods

        private void btnDecryptInputFilePath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (!String.IsNullOrWhiteSpace(PGPDecryptFileOptions.InputFilePath))
                dlg.InitialDirectory = System.IO.Path.GetDirectoryName(PGPDecryptFileOptions.InputFilePath);

            dlg.Filter = "All files|*.*";

            var result = dlg.ShowDialog();

            if (result == true)
            {
                PGPDecryptFileOptions.InputFilePath = dlg.FileName;
            }
        }

        private void btnDecryptOutputFilePath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (!String.IsNullOrWhiteSpace(PGPDecryptFileOptions.OutputFilePath))
                dlg.InitialDirectory = System.IO.Path.GetDirectoryName(PGPDecryptFileOptions.OutputFilePath);

            dlg.Filter = "All files|*.*";

            var result = dlg.ShowDialog();

            if (result == true)
            {
                PGPDecryptFileOptions.OutputFilePath = dlg.FileName;
            }
        }

        private void btnDecryptPrivateKeyFilePath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (!String.IsNullOrWhiteSpace(PGPDecryptFileOptions.PrivateKeyFilePath))
                dlg.InitialDirectory = System.IO.Path.GetDirectoryName(PGPDecryptFileOptions.PrivateKeyFilePath);

            dlg.Filter = "ASC files|*.asc|All files|*.*";

            var result = dlg.ShowDialog();

            if (result == true)
            {
                PGPDecryptFileOptions.PrivateKeyFilePath = dlg.FileName;
            }
        }

        private async void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChoPGPEncryptDecrypt pgp = PGPOptions.NewPGPEncryptDecrypt();
                await pgp.DecryptFileAsync(PGPDecryptFileOptions.InputFilePath, PGPDecryptFileOptions.OutputFilePath, PGPDecryptFileOptions.PrivateKeyFilePath, PGPDecryptFileOptions.PassPhrase);
                MessageBox.Show("PGP decryption successful.", Title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred during PGP decryption. " + ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion Decrypt Methods

        #region Generate Key Methods

        private void btnPublicKeyFilePath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (!String.IsNullOrWhiteSpace(PGPGenerateKeyOptions.PublicKeyFilePath))
                dlg.InitialDirectory = System.IO.Path.GetDirectoryName(PGPGenerateKeyOptions.PublicKeyFilePath);

            dlg.Filter = "ASC files|*.asc|All files|*.*";

            var result = dlg.ShowDialog();

            if (result == true)
            {
                PGPGenerateKeyOptions.PublicKeyFilePath = dlg.FileName;
            }
        }

        private void btnPrivateKeyFilePath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (!String.IsNullOrWhiteSpace(PGPGenerateKeyOptions.PrivateKeyFilePath))
                dlg.InitialDirectory = System.IO.Path.GetDirectoryName(PGPGenerateKeyOptions.PrivateKeyFilePath);

            dlg.Filter = "ASC files|*.asc|All files|*.*";

            var result = dlg.ShowDialog();

            if (result == true)
            {
                PGPGenerateKeyOptions.PrivateKeyFilePath = dlg.FileName;
            }
        }

        private async void btnGenerateKey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChoPGPEncryptDecrypt pgp = PGPOptions.NewPGPEncryptDecrypt();
                await pgp.GenerateKeyAsync(PGPGenerateKeyOptions.PublicKeyFilePath, PGPGenerateKeyOptions.PrivateKeyFilePath, PGPGenerateKeyOptions.Identity, PGPGenerateKeyOptions.PassPhrase,
                    PGPGenerateKeyOptions.Strength, PGPGenerateKeyOptions.Certainty);
                MessageBox.Show("PGP key generation successful.", Title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred during PGP key generation. " + ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion Generate Key Methods

        #region Other Methods

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
        private void TextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        #endregion Other Methods
    }
}
