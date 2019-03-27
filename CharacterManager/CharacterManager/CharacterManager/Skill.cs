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
