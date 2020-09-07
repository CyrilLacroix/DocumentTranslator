﻿// // ----------------------------------------------------------------------
// // <copyright file="MainWindowViewModel.cs" company="Microsoft Corporation">
// // Copyright (c) Microsoft Corporation.
// // All rights reserved.
// // THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// // KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// // IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// // PARTICULAR PURPOSE.
// // </copyright>
// // ----------------------------------------------------------------------
// // <summary>AccountViewModel.cs</summary>
// // ----------------------------------------------------------------------

namespace TranslationAssistant.DocumentTranslationInterface.ViewModel
{
    using System;
    using System.Windows.Input;

    using Microsoft.Practices.Prism.Commands;

    using TranslationAssistant.Business;
    using TranslationAssistant.Business.Model;
    using TranslationAssistant.DocumentTranslationInterface.Common;
    using FirstFloor.ModernUI.Presentation;
    using FirstFloor.ModernUI.Windows;
    using System.Windows.Navigation;

    /// <summary>
    ///     The Main window view model.
    /// </summary>
    //public class MainWindowViewModel : Notifyer
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        ///     The status text.
        /// </summary>
        private string statusText;

        private LinkGroupCollection groups = new LinkGroupCollection();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountViewModel" /> class.
        /// </summary>
        public MainWindowViewModel()
        {
            Properties.DocumentTranslator.Default.Upgrade();
            Properties.DocumentTranslator.Default.Reload();
            try
            {
                TranslationServices.Core.TranslationServiceFacade.Initialize();
            }
            catch (TranslationServices.Core.CredentialsMissingException ex)
            {
                StatusText = String.Format("{0}\n{1}", Properties.Resources.Error_PleaseSubscribe, ex.Message);
                ShowStatus();
                return;
            }
            StatusText = string.Empty;
            ShowStatus();
        }

        #endregion

        #region Public Properties

 
        /// <summary>
        ///     Gets or sets the status text.
        /// </summary>
        public string StatusText
        {
            get
            {
                return this.statusText;
            }

            set
            {
                this.statusText = value;
                //this.NotifyPropertyChanged("StatusText");
            }
        }

        public LinkGroupCollection Groups {
            get { return this.groups; }
        }

        #endregion

        #region Methods

        private async void ShowStatus()
        {

            if (await TranslationServices.Core.TranslationServiceFacade.IsTranslationServiceReadyAsync())
            {
                this.StatusText = Properties.Resources.Common_Ready;
            }
            else
            {
                this.StatusText = Properties.Resources.Error_PleaseSubscribe;
            }
        }

        #endregion
    }
}