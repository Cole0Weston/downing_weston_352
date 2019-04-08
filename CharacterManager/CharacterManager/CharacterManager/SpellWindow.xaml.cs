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
    /// Interaction logic for SpellWindow.xaml
    /// </summary>
    public partial class SpellWindow : Window
    {
        private ArrayList characterList;
        private Character curCharacter;
        private Spell curSpell;

        public SpellWindow(ArrayList charList)
        {
            characterList = charList;
            curCharacter = new Character();
            InitializeComponent();
            curCharacter = (Character)characterList[0];
            SpellList.ItemsSource = curCharacter.spells;
            DamageGrid.Visibility = Visibility.Hidden;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Window newWindow = new MainWindow(characterList);
            newWindow.Show();
            this.Close();
        }

        private void NewUtilSpell_Click(object sender, RoutedEventArgs e)
        {
            curCharacter.addSpell("Utility");
        }

        private void NewDmgSpell_Click(object sender, RoutedEventArgs e)
        {
            curCharacter.addSpell("Damage");
        }

        private void SpellList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DamageGrid.Visibility = Visibility.Hidden;
            curSpell = (Spell)SpellList.SelectedItem;
            //Do nothing if we've just deleted a spell or somehow selected no spell.
            if (curSpell == null){return;}

            SpellName.Text = curSpell.name;
            SpellType.Content = curSpell.getType();
            SpellInfo.Text = curSpell.info;
            SpellLevel.Text = curSpell.level;
            SpellCastTime.Text = curSpell.castTime;
            SpellDuration.Text = curSpell.duration;
            SpellRange.Text = curSpell.range;
            SpellSchool.Text = curSpell.school;
            SpellComponents.Text = curSpell.components;
            SpellSave.Text = curSpell.save;
            if(curSpell.getType() == "Damage")
            {
                DamageGrid.Visibility = Visibility.Visible;
                SpellDamage.Text = ((DamageSpell)curSpell).damage;
                SpellDamageType.Text = ((DamageSpell)curSpell).damageType;
            }
        }

        private void SpellName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (curSpell != null)
            {
                curSpell.name = text;
                SpellList.Items.Refresh();
            }
        }

        private void SpellLevel_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (curSpell != null && text != "")
            {
                curSpell.level = text;
            }
        }

        private void SpellCastTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (curSpell != null && text != "")
            {
                curSpell.castTime = text;
            }
        }

        private void SpellRange_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (curSpell != null && text != "")
            {
                curSpell.range = text;
            }
        }

        private void SpellDuration_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (curSpell != null && text != "")
            {
                curSpell.duration = text;
            }
        }

        private void SpellSchool_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (curSpell != null && text != "")
            {
                curSpell.school = text;
            }
        }

        private void SpellComponents_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (curSpell != null && text != "")
            {
                curSpell.components = text;
            }
        }

        private void SpellSave_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (curSpell != null && text != "")
            {
                curSpell.save = text;
            }
        }

        private void SpellDamage_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (curSpell != null && text != "")
            {
                ((DamageSpell)curSpell).damage = text;
            }
        }

        private void SpellDamageType_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (curSpell != null && text != "")
            {
                ((DamageSpell)curSpell).damageType = text;
            }
        }

        private void SpellInfo_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (curSpell != null && text != "")
            {
                curSpell.info = text;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //This seems to work properly if the curSpell = null. It deletes nothing.
            curCharacter.spells.Remove(curSpell);
            SpellList.Items.Refresh();
        }
    }
}
