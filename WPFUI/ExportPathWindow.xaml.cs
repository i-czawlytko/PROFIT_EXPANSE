﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFUI
{
    /// <summary>
    /// Логика взаимодействия для ExportPathWindow.xaml
    /// </summary>
    public partial class ExportPathWindow : Window
    {
        ExportPathWindowViewModel VM;
        public ExportPathWindow()
        {
            InitializeComponent();
            VM = new ExportPathWindowViewModel();
            this.DataContext = VM;
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public string ExportPath
        {
            get { return VM.ExportPath; }
        }
    }
}
