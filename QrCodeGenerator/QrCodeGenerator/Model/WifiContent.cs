/*
http://daniel-baumann.ch/other/qr-codes/wifi/
*/

using System;
using System.ComponentModel;
using System.Diagnostics;

namespace QrCodeGenerator.Model
{
    enum AuthenticationType
    {
        WPA = 0,
        WEP,
        nopass
    }

    internal sealed class WifiContent : QrContent, IDataErrorInfo
    {
        #region Properties

        /// <summary>
        /// Authentication type (optional; equals nopass if omitted): either WEP, WPA, or nopass
        /// </summary>
        internal AuthenticationType Authentication { get; set; }

        /// <summary>
        /// Name of the network
        /// </summary>
        internal string Ssid { get; set; }

        /// <summary>
        /// Get/sets the Pre-Shared Key
        /// </summary>
        internal string Psk { get; set; }

        /// <summary>
        /// True, if SSID is hidden
        /// </summary>
        internal bool HiddenSsid { get; set; }

        #endregion

        internal override string GetContent
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Ssid))
                    return string.Empty;

                if (string.IsNullOrWhiteSpace(Psk))
                    return string.Empty;

                return string.Format("WIFI:T:{0};S:{1};P:{2};H:{3};", Authentication, Ssid, Psk, HiddenSsid);
            }
        }

        internal override bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                    if (GetValidationError(property) != null)
                        return false;
                return true;
            }
        }

        private static readonly string[] ValidatedProperties =
        {
            "Ssid",
            "Psk"
        };

        private string GetValidationError(string propertyName)
        {
            //Check if property is in validation list.
            //If not, then it's not require to be validated.
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
            {
                return null;
            }

            string error = null;

            switch (propertyName)
            {
                case "Ssid":
                    error = StringValidation.ValidateRequired(propertyName, Ssid);
                    break;
                case "Psk":
                    error = StringValidation.ValidateRequired(propertyName, Psk);
                    break;

                default:
                    Debug.Fail("Unexpected property being validated on vCardContent: " + propertyName);
                    break;
            }

            return error;
        }

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error { get { return null; } }

        string IDataErrorInfo.this[string PropertyName]
        {
            get
            {
                return this.GetValidationError(PropertyName);
            }
        }

        #endregion
    }
}