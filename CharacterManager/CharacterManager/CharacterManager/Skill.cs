using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager
{
    class Skill : AbilityObserver, ProficiencyObserver
    {
        int value;
        int aBonus; //Bonus from corresponding ability score.
        int pBonus; //Bonus from character proficiency.
        bool isProficient; //Determines whether pBonus should be added.

        //Called when a character's ability scores are updated.
        public void UpdateA(int bonus)
        {
            aBonus = bonus;
            value = aBonus;
            if (isProficient) { value += pBonus; }
        }

        //Called when a character's proficiency bonus is updated.
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
