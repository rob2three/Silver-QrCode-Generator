using System.ComponentModel;
using QrCodeGenerator.Model;

namespace QrCodeGenerator.ViewModel
{
    internal sealed class WifiViewModel : QrViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        #region Member

        private readonly WifiContent _wifiContent;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructor

        public WifiViewModel()
        {
            _wifiContent = new WifiContent();
        }

        #endregion

        #region Properties

        public AuthenticationType Authentication
        {
            get { return _wifiContent.Authentication; }
            set
            {
                if (_wifiContent.Authentication == value)
                    return;
                _wifiContent.Authentication = value;
                PropertyChanged.Raise(() => Authentication);
            }
        }

        public string Ssid
        {
            get { return _wifiContent.Ssid; }
            set
            {
                if (_wifiContent.Ssid == value)
                    return;
                _wifiContent.Ssid = value;
                PropertyChanged.Raise(() => Ssid);
            }
        }

        public string Psk
        {
            get { return _wifiContent.Psk; }
            set
            {
                if (_wifiContent.Psk == value)
                    return;
                _wifiContent.Psk = value;
                PropertyChanged.Raise(() => Psk);
            }
        }

        public bool HiddenSsid
        {
            get { return _wifiContent.HiddenSsid; }
            set
            {
                if (_wifiContent.HiddenSsid == value)
                    return;
                _wifiContent.HiddenSsid = value;
                PropertyChanged.Raise(() => HiddenSsid);
            }
        }

        #endregion

        #region Methods

        internal override string GetContent()
        {
            return _wifiContent.GetContent;
        }
        internal override void Clear()
        {
            Ssid = string.Empty;
            HiddenSsid = false;
            Authentication=AuthenticationType.WPA;
            Psk = string.Empty;
        }

        #endregion

        #region IDataErrorInfo

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;

                error = (_wifiContent as IDataErrorInfo)[propertyName];

                return error;
            }
        }

        public string Error
        {
            get { return (_wifiContent as IDataErrorInfo).Error; } 
        }

        #endregion
    }
}