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
using Microsoft.Win32;
using System.IO;
using System.Collections;

namespace CharacterManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ArrayList characterList;
        private Character curCharacter;

        //MainWindow constructor - opens the main window with the first character in characterList displayed.
        public MainWindow(ArrayList charList)
        {
            //This is a bit of a silly workaround - the fields will all update before our character's info is pulled.
            //This prevents our character's name from being set to "Name", etc.
            characterList = charList;
            curCharacter = new Character();
            InitializeComponent();
            curCharacter = (Character)characterList[0];
            UpdateFields();
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

        //Called each time an ability score is changed.
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

        //Called each time an ability score is changed.
        private void UpdateAbilityScores()
        {
            StrValue.Content = curCharacter.getAbilityScore("Strength");
            DexValue.Content = curCharacter.getAbilityScore("Dexterity");
            ConValue.Content = curCharacter.getAbilityScore("Constitution");
            IntValue.Content = curCharacter.getAbilityScore("Intelligence");
            ChrValue.Content = curCharacter.getAbilityScore("Charisma");
            WisValue.Content = curCharacter.getAbilityScore("Wisdom");
        }

        //No processing necessary for text fields - contents are arbitrary and determined by the user.
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
            curCharacter.race = ((TextBox)sender).Text;
        }

        private void AlignmentText_TextChanged(object sender, TextChangedEventArgs e)
        {
            curCharacter.alignment = ((TextBox)sender).Text;
        }
        //End arbitrary text fields.

        //Level text is constrained from 1-99 and the contents of LevelText must be guaranteed to be 1-99.
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

        //MaxHP, CurrentHP, Speed, and Armor are arbitrary values, to be tracked by the user.
        //Content of MaxHPBox, CurrentHPBox, SpeedBox, and ArmorBox are guaranteed to be integers.
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Character files (*.char)|*.char";
            if (saveFileDialog.ShowDialog() == true)
            {
                curCharacter.saveCharacter(saveFileDialog.FileName);
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Character files (*.char)|*.char";
            if (openFileDialog.ShowDialog() == true)
            {
                curCharacter = new Character(openFileDialog.FileName);
            }

            UpdateFields();
        }

        private void UpdateFields()
        {
            UpdateAbilityScores();
            UpdateAbilityBonuses();
            NameText.Text = curCharacter.name;
            ClassText.Text = curCharacter.charClass;
            RaceText.Text = curCharacter.race;
            AlignmentText.Text = curCharacter.alignment;
            LevelText.Text = curCharacter.level.ToString();
            PerceptionBox.Text = curCharacter.getPerception().ToString();
            SpeedBox.Text = curCharacter.speed.ToString();
            ArmorBox.Text = curCharacter.armorClass.ToString();
            CurrentHPBox.Text = curCharacter.currentHP.ToString();
            MaxHPBox.Text = curCharacter.maxHP.ToString();
        }

        private void Chage_Icon_Click(object sender, RoutedEventArgs e)
        {
            var imagefilePath = string.Empty;
            string path = Directory.GetCurrentDirectory();
            // This is a possible solution to our defaulting directory problem. Will have to test on other machines.
            path = path.Substring(0, path.Length - 9) + "PlayerIcons";           
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = path;
            openFileDialog.Filter = "Character Avatars (*.png)|*.png";
            if (!Directory.Exists(path))
            {
                MessageBox.Show("Oops! PlayerIcons Folder Wasn't Found.");
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            }
            if (openFileDialog.ShowDialog() == true)
            {
                imagefilePath = openFileDialog.FileName;
                characterAvatar.Source = new BitmapImage(new Uri(imagefilePath));
            }
            

        }
    }
}
