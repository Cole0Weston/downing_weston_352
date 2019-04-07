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
        private Item curItem;

        public InventoryWindow(ArrayList charList)
        {
            characterList = charList;
            curCharacter = new Character();
            InitializeComponent();
            curCharacter = (Character)characterList[0];
            InventoryList.ItemsSource = curCharacter.items;
            WeaponGrid.Visibility = Visibility.Hidden;
        }

        private void AddWeaponButton_Click(object sender, RoutedEventArgs e)
        {
            curCharacter.addItem("Weapon");
        }

        private void AddArmorButton_Click(object sender, RoutedEventArgs e)
        {
            curCharacter.addItem("Armor");
        }

        private void AddMiscButton_Click(object sender, RoutedEventArgs e)
        {
            curCharacter.addItem("Misc");
        }

        private void InventoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WeaponGrid.Visibility = Visibility.Hidden;
            curItem = (Item)InventoryList.SelectedItem;
            ItemName.Text = curItem.name;
            ItemType.Content = curItem.getType();
            ItemWeight.Text = curItem.weight.ToString();
            ItemInfo.Text = curItem.info;
            if(curItem.getType() == "Weapon")
            {
                WeaponGrid.Visibility = Visibility.Visible;
                ItemAttack.Content = ((Weapon)curItem).getAttackRoll();
                WeaponAbility.Content = "Ability: " + ((Weapon)curItem).getAbility();
                Proficient.IsChecked = ((Weapon)curItem).getProficiency();
                ItemDamageType.Text = ((Weapon)curItem).damageType;
                DamageNumDice.Text = ((Weapon)curItem).numDmgDice.ToString();
                DamageDice.Text = ((Weapon)curItem).dmgDice.ToString();
                DamageBonus.Content = ((Weapon)curItem).getDamageBonus();
                ItemRange.Text = ((Weapon)curItem).range.ToString();
            }
        }

        private void ItemInfo_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (curItem != null && text != "Info / Description")
            {
                curItem.info = ((TextBox)sender).Text;
            }
        }

        private void ItemName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (curItem != null && text != "")
            {
                curItem.name = text;
                InventoryList.Items.Refresh();
            }
        }

        private void ItemWeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (curItem != null)
            {
                string text = ((TextBox)sender).Text;
                if (int.TryParse(text, out int value))
                {
                    curItem.weight = value;
                }
                ((TextBox)sender).Text = curItem.weight.ToString();
            }
        }

        private void AbilityToggle_Click(object sender, RoutedEventArgs e)
        {
            ((Weapon)curItem).changeAbility();
            ((Weapon)curItem).UpdateA(curCharacter.getAbilityBonus(((Weapon)curItem).getAbility()));
            WeaponAbility.Content = "Ability: " + ((Weapon)curItem).getAbility();
            ItemAttack.Content = ((Weapon)curItem).getAttackRoll();
            DamageBonus.Content = ((Weapon)curItem).getDamageBonus();
        }

        private void Proficient_CheckedChanged(object sender, RoutedEventArgs e)
        {
            ((Weapon)curItem).updateProficiency((bool)Proficient.IsChecked);
            ItemAttack.Content = ((Weapon)curItem).getAttackRoll();
        }

        private void ItemDamageType_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (curItem != null && text != "")
            {
                ((Weapon)curItem).damageType = text;
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Window newWindow = new MainWindow(characterList);
            newWindow.Show();
            this.Close();
        }

        private void DamageNumDice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (curItem != null)
            {
                string text = ((TextBox)sender).Text;
                if (int.TryParse(text, out int value))
                {
                    ((Weapon)curItem).numDmgDice = value;
                }
                ((TextBox)sender).Text = ((Weapon)curItem).numDmgDice.ToString();
            }
        }

        private void DamageDice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (curItem != null)
            {
                string text = ((TextBox)sender).Text;
                if (int.TryParse(text, out int value))
                {
                    ((Weapon)curItem).dmgDice = value;
                }
                ((TextBox)sender).Text = ((Weapon)curItem).dmgDice.ToString();
            }
        }

        private void ItemRange_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (curItem != null)
            {
                string text = ((TextBox)sender).Text;
                if (int.TryParse(text, out int value))
                {
                    ((Weapon)curItem).range = value;
                }
                ((TextBox)sender).Text = ((Weapon)curItem).range.ToString();
            }
        }
    }
}
