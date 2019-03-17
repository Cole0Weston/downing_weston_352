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
using System.Windows.Shapes;

namespace CharacterManager
{
    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : Window
    {
        Character[] characterList;

        public StartupWindow()
        {
            characterList = new Character[10];
            InitializeComponent();
        }

        private void CreateNew_Click(object sender, RoutedEventArgs e)
        {
            characterList[0] = new Character();
            Window newWindow = new MainWindow(characterList);
            newWindow.Show();
            this.Close();
        }

        private void LoadExisting_Click(object sender, RoutedEventArgs e)
        {
            Window newWindow = new MainWindow(characterList);
            newWindow.Show();
            this.Close();
        }
    }
}
