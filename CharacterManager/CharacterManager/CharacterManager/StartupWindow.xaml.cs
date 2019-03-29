using System;
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
using System.Collections;
using Microsoft.Win32;

namespace CharacterManager
{
    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : Window
    {
        ArrayList characterList;

        public StartupWindow()
        {
            characterList = new ArrayList();
            InitializeComponent();
        }

        private void CreateNew_Click(object sender, RoutedEventArgs e)
        {
            characterList.Add(new Character());
            Window newWindow = new MainWindow(characterList);
            newWindow.Show();
            this.Close();
        }

        private void LoadExisting_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Character files (*.char)|*.char";
            if (openFileDialog.ShowDialog() == true)
            {
                characterList.Add(new Character(openFileDialog.FileName));
            }
            Window newWindow = new MainWindow(characterList);
            newWindow.Show();
            this.Close();
        }
    }
}
