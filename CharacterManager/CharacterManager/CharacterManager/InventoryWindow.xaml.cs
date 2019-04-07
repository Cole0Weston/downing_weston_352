using System;
using System.Collections;
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
    /// Interaction logic for InventoryWindow.xaml
    /// </summary>
    public partial class InventoryWindow : Window
    {
        private ArrayList characterList;
        private Character curCharacter;

        public InventoryWindow(ArrayList charList)
        {
            characterList = charList;
            curCharacter = new Character();
            InitializeComponent();
            curCharacter = (Character)characterList[0];
        }
    }
}
