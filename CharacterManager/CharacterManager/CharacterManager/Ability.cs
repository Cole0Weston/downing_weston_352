﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager
{
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
            if (value >= 0)
            {
                score = value;
            }
            setBonus(score);
            foreach (AbilityObserver ao in observers)
            {
                ao.UpdateA(bonus);
            }
        }

        private void setBonus(int value)
        {
            //Ability bonuses are equal to (score - 10) divided by 2, rounding down.
            bonus = (int)Math.Floor((score - 10) / 2.0);
        }

        public int getScore() { return score; }
        public int getBonus() { return bonus; }

        public void registerObserver(AbilityObserver po)
        {
            observers.Add(po);
        }
    }
}
