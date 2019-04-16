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
    /// Interaction logic for SkillWindow.xaml
    /// </summary>
    public partial class SkillWindow : Window
    {
        ArrayList characterList;
        Character curCharacter;
        Dictionary<string, TextBlock> skillValues;
        Dictionary<string, CheckBox> skillBoxes;

        public SkillWindow(ArrayList charList)
        {
            characterList = charList;
            curCharacter = (Character)characterList[0];
            skillValues = new Dictionary<string, TextBlock>();
            skillBoxes = new Dictionary<string, CheckBox>();
            InitializeComponent();
            InitializeSkillControls();
            foreach(KeyValuePair<string, TextBlock> skillpair in skillValues)
            {
                string skillName = skillpair.Key;
                TextBlock skillText = skillpair.Value;
                skillText.Text = getPlayerSkillValue(skillName);
            }
            foreach(KeyValuePair<string, CheckBox> skillpair in skillBoxes)
            {
                string skillName = skillpair.Key;
                CheckBox skillBox = skillpair.Value;
                skillBox.IsChecked = curCharacter.getSkillProficiency(skillName);
            }
            FeatBox.Text = curCharacter.featInfo;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Window w = new MainWindow(characterList);
            w.Show();
            this.Close();
        }

        private void ProficiencyBox_Checked(object sender, RoutedEventArgs e)
        {
            string skill = ((CheckBox)sender).Tag.ToString();
            bool isProf = (bool)((CheckBox)sender).IsChecked;
            curCharacter.setSkillProficiency(skill, isProf);
            skillValues[skill].Text = getPlayerSkillValue(skill);
        }

        private void InitializeSkillControls()
        {
            string[] skillNames = { "Athletics", "Acrobatics", "Sleight of Hand", "Stealth", "Arcana", "History", "Investigation",
                                    "Nature", "Religion", "Animal Handling", "Insight", "Medicine", "Perception", "Survival",
                                    "Deception", "Intimidation", "Performance", "Persuasion" };

            TextBlock[] textBlocks = { AthleticsBonus, AcrobaticsBonus, SleightOfHandBonus, StealthBonus, ArcanaBonus, HistoryBonus,
                                       InvestigationBonus, NatureBonus, ReligionBonus, AnimalHandlingBonus, InsightBonus, MedicineBonus,
                                       PerceptionBonus, SurvivalBonus, DeceptionBonus, IntimidationBonus, PerformanceBonus, PersuasionBonus };

            CheckBox[] checkBoxes = { AthleticsBox, AcrobaticsBox, SleightOfHandBox, StealthBox, ArcanaBox, HistoryBox,
                                       InvestigationBox, NatureBox, ReligionBox, AnimalHandlingBox, InsightBox, MedicineBox,
                                       PerceptionBox, SurvivalBox, DeceptionBox, IntimidationBox, PerformanceBox, PersuasionBox };

            for (int i = 0; i < skillNames.Length; i++)
            {
                skillValues.Add(skillNames[i], textBlocks[i]);
                skillBoxes.Add(skillNames[i], checkBoxes[i]);
            }
        }

        private string getPlayerSkillValue(string skill)
        {
            int value = curCharacter.getSkillValue(skill);
            return (value >= 0 ? "+" : "") + value; 
        }

        private void FeatBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text != "Feats and Languages")
            {
                curCharacter.featInfo = ((TextBox)sender).Text;
            }
        }
    }
}
