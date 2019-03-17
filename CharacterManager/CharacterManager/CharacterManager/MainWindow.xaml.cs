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
            if (val < 30)
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
    }
}
