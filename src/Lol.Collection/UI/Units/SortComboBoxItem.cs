﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Lol.Collection.UI.Units
{
    public class SortComboBoxItem : ComboBoxItem
    {
        #region DefaultStyleKey

        static SortComboBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SortComboBoxItem), new FrameworkPropertyMetadata(typeof(SortComboBoxItem)));
        }
        #endregion
    }
}