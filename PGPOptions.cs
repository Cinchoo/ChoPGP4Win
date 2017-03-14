using Cinchoo.PGP;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChoPGP4Win
{
    public class PGPOptions : INotifyPropertyChanged
    { 
        private CompressionAlgorithmTag _compressionAlgorithm;
        public CompressionAlgorithmTag CompressionAlgorithm
        {
            get { return _compressionAlgorithm; }
            set
            {
                if (_compressionAlgorithm != value)
                {
                    _compressionAlgorithm = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private SymmetricKeyAlgorithmTag _symmetricKeyAlgorithm;
        public SymmetricKeyAlgorithmTag SymmetricKeyAlgorithm
        {
            get { return _symmetricKeyAlgorithm; }
            set
            {
                if (_symmetricKeyAlgorithm != value)
                {
                    _symmetricKeyAlgorithm = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private PgpSignatureTag _pgpSignature;
        public PgpSignatureTag PgpSignature
        {
            get { return _pgpSignature; }
            set
            {
                if (_pgpSignature != value)
                {
                    _pgpSignature = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private PublicKeyAlgorithmTag _publicKeyAlgorithm;
        public PublicKeyAlgorithmTag PublicKeyAlgorithm
        {
            get { return _publicKeyAlgorithm; }
            set
            {
                if (_publicKeyAlgorithm != value)
                {
                    _publicKeyAlgorithm = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public PGPOptions()
        {
            CompressionAlgorithm = ChoAppSettings.GetValue<CompressionAlgorithmTag>("CompressionAlgorithm", CompressionAlgorithmTag.Uncompressed, true);
            SymmetricKeyAlgorithm = ChoAppSettings.GetValue<SymmetricKeyAlgorithmTag>("SymmetricKeyAlgorithm", SymmetricKeyAlgorithmTag.TripleDes, true);
            PublicKeyAlgorithm = ChoAppSettings.GetValue<PublicKeyAlgorithmTag>("PublicKeyAlgorithm", PublicKeyAlgorithmTag.RsaGeneral, true);
            PgpSignature = ChoAppSettings.GetValue<PgpSignatureTag>("PgpSignature", PgpSignatureTag.DefaultCertification, true);
        }

        private void NotifyPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public ChoPGPEncryptDecrypt NewPGPEncryptDecrypt()
        {
            ChoPGPEncryptDecrypt pgp = new ChoPGPEncryptDecrypt();
            pgp.CompressionAlgorithm = CompressionAlgorithm;
            pgp.SymmetricKeyAlgorithm = SymmetricKeyAlgorithm;
            pgp.PublicKeyAlgorithm = PublicKeyAlgorithm;
            pgp.PgpSignatureType = (int)PgpSignature;

            return pgp;
        }
    }
    public enum PgpSignatureTag
    {
        BinaryDocument = 0,
        CanonicalTextDocument = 1,
        CasualCertification = 18,
        CertificationRevocation = 48,
        DefaultCertification = 16,
        DirectKey = 31,
        KeyRevocation = 32,
        NoCertification = 17,
        PositiveCertification = 19,
        PrimaryKeyBinding = 25,
        StandAlone = 2,
        SubkeyBinding = 24,
        SubkeyRevocation = 40,
        Timestamp = 64
    };

}
