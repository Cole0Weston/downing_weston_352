using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager
{
    public abstract class Item
    {
        public string name;
        protected string type;
        public string info;
        public int weight;

        public abstract override string ToString();
    }

    class Weapon : Item, ProficiencyObserver, AbilityObserver
    {
        protected int profBonus;
        protected int abilityBonus;
        protected int numDmgDice;
        protected int dmgDice;
        protected int dmgBonus;
        protected int atkBonus;
        protected int range;

        protected string ability; //The associated score - Dexterity or Strength.
        public string damageType;

        protected bool isProficient;

        public Weapon()
        {
            name = "New Weapon";
            type = "Weapon";
            info = "Info / Description";
            weight = 0;
            ability = "Strength";
            damageType = "Damage Type";
            numDmgDice = 1;
            dmgDice = 4;
            dmgBonus = 0;
            atkBonus = 0;
            range = 0;
            isProficient = false;
        }

        public void changeAbility()
        {
            if (ability == "Strength")
            {
                ability = "Dexterity";
            }
            else { ability = "Strength"; }
        }

        public string getAttackRoll()
        {
            return "1d20 + " + atkBonus;
        }

        public string getDamageRoll()
        {
            return numDmgDice + "d" + dmgDice + " + " + dmgBonus;
        }

        public void updateProficiency(bool isProf)
        {
            isProficient = isProf;
        }

        public void UpdateP(int prof)
        {
            profBonus = prof;
            atkBonus = abilityBonus;
            if (isProficient)
            {
                atkBonus += profBonus;
            }
        }

        public void UpdateA(int bonus)
        {
            abilityBonus = bonus;
            atkBonus = abilityBonus;
            dmgBonus = abilityBonus;
            if (isProficient)
            {
                atkBonus += profBonus;
            }
        }

        public override string ToString()
        {
            return name;
        }
    }

    class Armor : Item
    {
        public int armorClass;
        protected bool equipped;

        public Armor()
        {
            name = "New Armor";
            type = "Armor";
            info = "Info / Description";
            weight = 0;
            armorClass = 0;
            equipped = false;
        }

        public void setEquipped(bool isEquipped)
        {
            equipped = isEquipped;
        }

        public override string ToString()
        {
            return name;
        }
    }

    class Misc : Item
    {
        public Misc()
        {
            name = "New Item";
            type = "Misc";
            info = "Info / Description";
            weight = 0;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
