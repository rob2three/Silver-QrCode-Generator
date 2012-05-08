﻿/*
 *  Silver QrCode Generator - Windows WPF application to generate QrCode.
 *  Copyright (c) 2012 Canxing(Jason) He
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QrCodeGenerator.TabPage
{
    /// <summary>
    /// Interaction logic for SMSPage.xaml
    /// </summary>
    public partial class SMSPage : UserControl
    {
        public SMSPage()
        {
            InitializeComponent();
        }

        internal bool isSMSValid(out string smsStr)
        {
            bool isValid = true;
            smsStr = string.Empty;
            if (!UIValidation.RegexValidate(wtbSMSPhone, UIValidation.PhoneReg, "Invalide phone format", true))
                isValid = false;
            if (!UIValidation.ValidateRequiredTextBox(tbSMSMessage))
                isValid = false;
            if (isValid)
                smsStr = SMSGenerate();
            return isValid;
        }

        private string SMSGenerate()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SMSTO:");
            builder.Append(StringMethod.MeCardPhoneRevamp(wtbSMSPhone.Text));
            builder.Append(":");
            builder.Append(tbSMSMessage.Text.TrimStart(' ').TrimEnd(' '));
            return builder.ToString();
        }

        internal void Clear()
        {
            wtbSMSPhone.Text = string.Empty;
            UIValidation.SetUpValidControl(wtbSMSPhone);
            tbSMSMessage.Text = string.Empty;
            UIValidation.SetUpValidControl(tbSMSMessage);
        }
    }
}