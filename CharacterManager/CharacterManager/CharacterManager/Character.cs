using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager
{
    public class Character
    {
        //Abilities are indexed by name: abilities["Strength"]
        private Dictionary<String, Ability> abilities;
        //Skills are also indexed by name: skills["Athletics"]
        private Dictionary<String, Skill> skills;

        private List<ProficiencyObserver> pObservers;
        private int proficiencyBonus;
        private int perception;

        public int level;
        public int speed;
        public int armorClass;
        public int currentHP;
        public int maxHP;

        public string name;
        public string charClass;
        public string race;

        public Character()
        {
            abilities = new Dictionary<string, Ability>();
            skills = new Dictionary<string, Skill>();
            pObservers = new List<ProficiencyObserver>();
            proficiencyBonus = 2;
            level = 1;
            speed = 30;
            armorClass = 10;
            currentHP = 5;
            maxHP = 10;

            initializeAbilities();
            initializeSkills();
        }

        public void setProficiencyBonus(int value)
        {
            if(value >= 0)
            {
                proficiencyBonus = value;
            }
            foreach(ProficiencyObserver po in pObservers)
            {
                po.UpdateP(proficiencyBonus);
            }
        }

        public void registerObserver(ProficiencyObserver po)
        {
            pObservers.Add(po);
        }

        public void setAbilityScore(string ability, int val)
        {
            abilities[ability].setScore(val);
            if(ability == "Wisdom")
            {
                perception = 10 + getAbilityBonus("Wisdom");
            }
        }

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

        private void initializeAbilities()
        {
            abilities.Add("Strength", new Ability(10));
            abilities.Add("Dexterity", new Ability(10));
            abilities.Add("Constitution", new Ability(10));
            abilities.Add("Intelligence", new Ability(10));
            abilities.Add("Charisma", new Ability(10));
            abilities.Add("Wisdom", new Ability(10));
        }

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

    class Ability
    {
        private List<AbilityObserver> observers;
        private int score;
        private int bonus;

        public Ability(int s)
        {
            observers = new List<AbilityObserver>();
            setScore(s);
        }

        public void setScore(int value)
        {
            //Ability scores cannot be negative values.
            if(value >= 0)
            {
                score = value;
            }
            setBonus(score);
            foreach(AbilityObserver ao in observers)
            {
                ao.UpdateA(bonus);
            }
        }

        private void setBonus(int value)
        {
            //Ability bonuses are equal to (score - 10) divided by 2, rounding down.
            bonus = (int)Math.Floor( (score - 10) / 2.0 );
        }

        public int getScore() { return score; }
        public int getBonus() { return bonus; }

        public void registerObserver(AbilityObserver po)
        {
            observers.Add(po);
        }
    }

    public interface AbilityObserver
    {
        void UpdateA(int bonus);
    }

    public interface ProficiencyObserver
    {
        void UpdateP(int proficiency);
    }

    class Skill : AbilityObserver, ProficiencyObserver
    {
        int value;
        int aBonus;
        int pBonus;
        bool isProficient;

        public void UpdateA(int bonus)
        {
            aBonus = bonus;
            value = aBonus;
            if (isProficient) { value += pBonus; }
        }

        public void UpdateP(int proficiency)
        {
            pBonus = proficiency;
            value = aBonus;
            if (isProficient) { value += pBonus; }
        }

        public void setProficiency(bool isProf)
        {
            isProficient = isProf;
            if (isProficient) { value = aBonus + pBonus; }
            else { value = aBonus; }
        }
    }
}
