﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Telegram.Api.Helpers;
using Telegram.Api.Services;
using Telegram.Api.Services.Cache;
using Telegram.Api.TL;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Unigram.Controls.Messages
{
    public sealed partial class UserMessageControl : MessageControlBase
    {
        public UserMessageControl()
        {
            InitializeComponent();

            DataContextChanged += (s, args) =>
            {
                if (ViewModel != null) Bindings.Update();
                //if (ViewModel != null) OnMessageChanged(HeaderLabel);
            };
        }

        #region Adaptive part

        private Visibility UpdateFirst(bool isFirst)
        {
            OnMessageChanged(HeaderLabel);
            return isFirst ? Visibility.Visible : Visibility.Collapsed;
        }

        private void OnMediaChanged(object sender, EventArgs e)
        {
            OnMediaChanged();
        }

        private void OnMediaChanged()
        {
            var message = DataContext as TLMessage;
            if (message == null || message.Media == null || message.Media is TLMessageMediaEmpty)
            {
                MediaControl.Margin = new Thickness(0);
                StatusToDefault();
                Grid.SetRow(StatusControl, 2);
                Grid.SetRow(MessageControl, 2);
            }
            else if (message != null && message.Media != null)
            {
                if (IsFullMedia(message.Media))
                {
                    var left = -8;
                    var top = -4;
                    var right = -12;
                    var bottom = -6;

                    if (message.HasReplyToMsgId)
                    {
                        top = 0;
                    }

                    // Captioned photo/video/gif
                    if (IsInlineMedia(message.Media))
                    {
                        StatusToDefault();
                        bottom = 4;
                    }
                    else
                    {
                        StatusToFullMedia();
                    }

                    var caption = false;
                    if (message.Media is ITLMediaCaption)
                    {
                        caption = !string.IsNullOrWhiteSpace(((ITLMediaCaption)message.Media).Caption);
                    }

                    MediaControl.Margin = new Thickness(left, top, right, bottom);
                    Grid.SetRow(StatusControl, caption ? 4 : 3);
                    Grid.SetRow(MessageControl, caption ? 4 : 2);
                }
                else if (IsInlineMedia(message.Media))
                {
                    var caption = false;
                    if (message.Media is ITLMediaCaption)
                    {
                        caption = !string.IsNullOrWhiteSpace(((ITLMediaCaption)message.Media).Caption);
                    }

                    MediaControl.Margin = new Thickness(0, 4, 0, 2);
                    StatusToDefault();
                    Grid.SetRow(StatusControl, caption ? 4 : 3);
                    Grid.SetRow(MessageControl, caption ? 4 : 2);
                }
                else
                {
                    MediaControl.Margin = new Thickness(0);
                    StatusToDefault();
                    Grid.SetRow(StatusControl, 4);
                    Grid.SetRow(MessageControl, 2);
                }
            }
        }

        private void StatusToFullMedia()
        {
            if (StatusControl.Padding.Left != 6)
            {
                StatusControl.Padding = new Thickness(6, 2, 6, 4);
                StatusControl.Background = StatusDarkBackgroundBrush;
                StatusLabel.Foreground = StatusDarkForegroundBrush;
                StatusGlyph.Foreground = StatusDarkForegroundBrush;

                //LayoutRoot.BorderThickness = new Thickness(0);
            }
        }

        private void StatusToDefault()
        {
            if (StatusControl.Padding.Left != 0)
            {
                StatusControl.Padding = new Thickness(0, 0, 6, 0);
                StatusControl.Background = null;
                StatusLabel.Foreground = StatusLightLabelForegroundBrush;
                StatusGlyph.Foreground = StatusLightGlyphForegroundBrush;

                //LayoutRoot.BorderThickness = new Thickness(0, 0, 0, 2);
            }
        }

        #endregion
    }
}