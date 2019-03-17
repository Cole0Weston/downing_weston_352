using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager
{
    class Character
    {
        private Dictionary<String, Ability> abilities;
        private Dictionary<String, Skill> skills;

        private List<ProficiencyObserver> pObservers;
        private int proficiencyBonus;

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
    }

    class Ability
    {
        private List<AbilityObserver> observers;
        private int score;
        private int bonus;

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
    }
}
