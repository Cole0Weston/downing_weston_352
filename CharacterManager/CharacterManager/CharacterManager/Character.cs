using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;

namespace CharacterManager
{
    //Interface for objects which must track a particular ability score.
    public interface AbilityObserver
    {
        void UpdateA(int bonus);
    }

    //Interface for objects which must track a character's proficiency bonus.
    public interface ProficiencyObserver
    {
        void UpdateP(int proficiency);
    }

    public class Character
    {
        //Abilities are indexed by name: abilities["Strength"]
        private Dictionary<String, Ability> abilities;
        //Skills are also indexed by name: skills["Athletics"]
        private Dictionary<String, Skill> skills;

        public ObservableCollection<Item> items;

        private List<ProficiencyObserver> pObservers;
        private int proficiencyBonus; // ((level - 1) / 4) + 2
        private int perception; // 10 + Wisdom bonus.

        //All public values below are determined arbitrarily by the user.
        public int level;
        public int speed;
        public int armorClass;
        public int currentHP;
        public int maxHP;

        public string name;
        public string charClass;
        public string race;
        public string alignment;

        public string playerAvatar;

        public Character()
        {
            abilities = new Dictionary<string, Ability>();
            skills = new Dictionary<string, Skill>();
            items = new ObservableCollection<Item>();
            pObservers = new List<ProficiencyObserver>();
            proficiencyBonus = 2;
            level = 1;
            speed = 30;
            armorClass = 10;
            currentHP = 5;
            maxHP = 10;
            perception = 10;

            name = "Name";
            charClass = "Class";
            race = "Race";
            alignment = "Alignment";
            playerAvatar = ((Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 9)) + "PlayerIcons\\female\\f_07.PNG");            
            initializeAbilities();
            initializeSkills();
        }

        public void saveCharacter(string file)
        {
            FileStream fs = new FileStream(file, FileMode.Create);
            fs.Close();
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.WriteLine(name);
                writer.WriteLine(charClass);
                writer.WriteLine(race);
                writer.WriteLine(alignment);
                writer.WriteLine(level);
                writer.WriteLine(speed);
                writer.WriteLine(armorClass);
                writer.WriteLine(currentHP);
                writer.WriteLine(maxHP);
                writer.WriteLine(playerAvatar.Substring(playerAvatar.Length - 28));
                foreach (KeyValuePair<string, Ability> a in abilities)
                {
                    writer.WriteLine(a.Key + ":" + a.Value.getScore());
                }
                foreach (KeyValuePair<string, Skill> s in skills)
                {
                    writer.WriteLine(s.Key + ":" + s.Value.getProficiency());
                }
            }
        }

        public Character(string file)
        {
            abilities = new Dictionary<string, Ability>();
            skills = new Dictionary<string, Skill>();
            pObservers = new List<ProficiencyObserver>();
            initializeAbilities();
            initializeSkills();
            
            using (StreamReader reader = new StreamReader(file))
            {
                name = reader.ReadLine();
                charClass = reader.ReadLine();
                race = reader.ReadLine();
                alignment = reader.ReadLine();
                level = Convert.ToInt16(reader.ReadLine());
                speed = Convert.ToInt16(reader.ReadLine());
                armorClass = Convert.ToInt16(reader.ReadLine());
                currentHP = Convert.ToInt16(reader.ReadLine());
                maxHP = Convert.ToInt16(reader.ReadLine());
                playerAvatar = (Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 10)) + reader.ReadLine();

                foreach (KeyValuePair<string, Ability> a in abilities)
                {
                    string[] ability = reader.ReadLine().Split(':');
                    abilities[ability[0]].setScore(Convert.ToInt16(ability[1]));
                }
                foreach (KeyValuePair<string, Skill> s in skills)
                {
                    string[] skill = reader.ReadLine().Split(':');
                    skills[skill[0]].setProficiency(Convert.ToBoolean(skill[1]));
                }
            }

            updatePerception();
        }

        // Should be called each time level is changed (perhaps implement a setLevel method instead.)
        public void updateProficiencyBonus()
        {
            proficiencyBonus = ((level - 1) / 4) + 2;
            foreach(ProficiencyObserver po in pObservers)
            {
                po.UpdateP(proficiencyBonus);
            }
        }

        public void registerObserver(ProficiencyObserver po)
        {
            pObservers.Add(po);
        }

        //Updates an ability score by name.
        public void setAbilityScore(string ability, int val)
        {
            abilities[ability].setScore(val);
            updatePerception();
        }

        public void updatePerception()
        {
            perception = 10 + getAbilityBonus("Wisdom");
        }

        public void addItem(string itemType)
        {
            switch (itemType)
            {
                case "Weapon":
                    items.Add(new Weapon());
                    break;
                case "Armor":
                    items.Add(new Armor());
                    break;
                case "Misc":
                    items.Add(new Misc());
                    break;
            }
        }

        // GETTERS
        public int getAbilityScore(string ability)
        {
            return abilities[ability].getScore();
        }

        public int getAbilityBonus(string ability)
        {
            return abilities[ability].getBonus();
        }

        public int getPerception()
        {
            return perception;
        }

        public int getProficiencyBonus()
        {
            return proficiencyBonus;
        }
        //END GETTERS

        //Initializes all abilities with default value (10).
        private void initializeAbilities()
        {
            abilities.Add("Strength", new Ability(10));
            abilities.Add("Dexterity", new Ability(10));
            abilities.Add("Constitution", new Ability(10));
            abilities.Add("Intelligence", new Ability(10));
            abilities.Add("Charisma", new Ability(10));
            abilities.Add("Wisdom", new Ability(10));
        }

        //Initializes all skills and links them to their corresponding ability score.
        //There is probably a much shorter way to initialize all of these, but this seems semi-sensible.
        private void initializeSkills()
        {
            //STRENGTH SKILLS
            skills.Add("Athletics", new Skill());
            abilities["Strength"].registerObserver(skills["Athletics"]);
            registerObserver(skills["Athletics"]);
            //DEXTERITY SKILLS
            skills.Add("Acrobatics", new Skill());
            skills.Add("Sleight of Hand", new Skill());
            skills.Add("Stealth", new Skill());
            abilities["Dexterity"].registerObserver(skills["Acrobatics"]);
            abilities["Dexterity"].registerObserver(skills["Sleight of Hand"]);
            abilities["Dexterity"].registerObserver(skills["Stealth"]);
            registerObserver(skills["Acrobatics"]);
            registerObserver(skills["Sleight of Hand"]);
            registerObserver(skills["Stealth"]);
            //INTELLIGENCE SKILLS
            skills.Add("Arcana", new Skill());
            skills.Add("History", new Skill());
            skills.Add("Investigation", new Skill());
            skills.Add("Nature", new Skill());
            skills.Add("Religion", new Skill());
            abilities["Intelligence"].registerObserver(skills["Arcana"]);
            abilities["Intelligence"].registerObserver(skills["History"]);
            abilities["Intelligence"].registerObserver(skills["Investigation"]);
            abilities["Intelligence"].registerObserver(skills["Nature"]);
            abilities["Intelligence"].registerObserver(skills["Religion"]);
            registerObserver(skills["Arcana"]);
            registerObserver(skills["History"]);
            registerObserver(skills["Investigation"]);
            registerObserver(skills["Nature"]);
            registerObserver(skills["Religion"]);
            //WISDOM SKILLS
            skills.Add("Animal Handling", new Skill());
            skills.Add("Insight", new Skill());
            skills.Add("Medicine", new Skill());
            skills.Add("Perception", new Skill());
            skills.Add("Survival", new Skill());
            abilities["Wisdom"].registerObserver(skills["Animal Handling"]);
            abilities["Wisdom"].registerObserver(skills["Insight"]);
            abilities["Wisdom"].registerObserver(skills["Medicine"]);
            abilities["Wisdom"].registerObserver(skills["Perception"]);
            abilities["Wisdom"].registerObserver(skills["Survival"]);
            registerObserver(skills["Animal Handling"]);
            registerObserver(skills["Insight"]);
            registerObserver(skills["Medicine"]);
            registerObserver(skills["Perception"]);
            registerObserver(skills["Survival"]);
            //CHARISMA SKILLS
            skills.Add("Deception", new Skill());
            skills.Add("Intimidation", new Skill());
            skills.Add("Performance", new Skill());
            skills.Add("Persuasion", new Skill());
            abilities["Charisma"].registerObserver(skills["Deception"]);
            abilities["Charisma"].registerObserver(skills["Intimidation"]);
            abilities["Charisma"].registerObserver(skills["Performance"]);
            abilities["Charisma"].registerObserver(skills["Persuasion"]);
            registerObserver(skills["Deception"]);
            registerObserver(skills["Intimidation"]);
            registerObserver(skills["Performance"]);
            registerObserver(skills["Persuasion"]);
        }
    }
}
