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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace CharacterManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Character[] characterList;
        private Character curCharacter;

        public MainWindow(Character[] charList)
        {
            characterList = charList;
            curCharacter = characterList[0];
            InitializeComponent();
        }

        private void Click_IncrementAbility(object sender, RoutedEventArgs e)
        {
            string ability = ((Button)sender).Tag.ToString();
            int val = curCharacter.getAbilityScore(ability);
            if (val <= 30)
            {
                curCharacter.setAbilityScore(ability.ToString(), val + 1);
            }
            UpdateAbilityScores();
            UpdateAbilityBonuses();
        }

        private void Click_DecrementAbility(object sender, RoutedEventArgs e)
        {
            string ability = ((Button)sender).Tag.ToString();
            int val = curCharacter.getAbilityScore(ability);
            if (val > 0)
            {
                curCharacter.setAbilityScore(ability.ToString(), val - 1);
            }
            UpdateAbilityScores();
            UpdateAbilityBonuses();
        }

        private void UpdateAbilityBonuses()
        {
            StrBonus.Content = curCharacter.getAbilityBonus("Strength");
            DexBonus.Content = curCharacter.getAbilityBonus("Dexterity");
            ConBonus.Content = curCharacter.getAbilityBonus("Constitution");
            IntBonus.Content = curCharacter.getAbilityBonus("Intelligence");
            ChrBonus.Content = curCharacter.getAbilityBonus("Charisma");
            WisBonus.Content = curCharacter.getAbilityBonus("Wisdom");
            PerceptionBox.Text = Convert.ToString(curCharacter.getPerception());
        }

        private void UpdateAbilityScores()
        {
            StrValue.Content = curCharacter.getAbilityScore("Strength");
            DexValue.Content = curCharacter.getAbilityScore("Dexterity");
            ConValue.Content = curCharacter.getAbilityScore("Constitution");
            IntValue.Content = curCharacter.getAbilityScore("Intelligence");
            ChrValue.Content = curCharacter.getAbilityScore("Charisma");
            WisValue.Content = curCharacter.getAbilityScore("Wisdom");
        }

        private void NameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            curCharacter.name = ((TextBox)sender).Text;
        }

        private void ClassText_TextChanged(object sender, TextChangedEventArgs e)
        {
            curCharacter.charClass = ((TextBox)sender).Text;
        }

        private void RaceText_TextChanged(object sender, TextChangedEventArgs e)
        {
            curCharacter.charClass = ((TextBox)sender).Text;
        }

        private void AlignmentText_TextChanged(object sender, TextChangedEventArgs e)
        {
            curCharacter.charClass = ((TextBox)sender).Text;
        }

        private void LevelText_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (int.TryParse(text, out int value))
            {
                if(value > 0 && value < 100)
                {
                    curCharacter.level = value;
                }
            }
            ((TextBox)sender).Text = curCharacter.level.ToString();
            curCharacter.updateProficiencyBonus();

            //Null check is needed here because it will attempt to update the box before it is initialized.
            if(ProficiencyBox != null)
            {
                ProficiencyBox.Text = "+" + Convert.ToString(curCharacter.getProficiencyBonus());
            }
        }

        private void MaxHPBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (int.TryParse(text, out int value))
            {
                if (value > 0 && value < 1000)
                {
                    curCharacter.maxHP = value;
                }
            }
            ((TextBox)sender).Text = curCharacter.maxHP.ToString();
        }

        private void CurrentHPBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (int.TryParse(text, out int value))
            {
                curCharacter.currentHP = value;
            }
            ((TextBox)sender).Text = curCharacter.currentHP.ToString();
        }

        private void SpeedBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (int.TryParse(text, out int value))
            {
                curCharacter.speed = value;
            }
            ((TextBox)sender).Text = curCharacter.speed.ToString();
        }

        private void ArmorBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (int.TryParse(text, out int value))
            {
                curCharacter.armorClass = value;
            }
            ((TextBox)sender).Text = curCharacter.armorClass.ToString();
        }
    }
}
