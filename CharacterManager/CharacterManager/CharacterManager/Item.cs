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
        public string getType()
        {
            return type;
        }
        public abstract string toSaveString();
    }

    class Weapon : Item, ProficiencyObserver, AbilityObserver
    {
        protected int profBonus;
        protected int abilityBonus;
        public int numDmgDice;
        public int dmgDice;
        protected int dmgBonus;
        protected int atkBonus;
        public int range;

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

        public Weapon(string n, string i, int we, string ab, string dt, int ndd, int dd, int r, bool ip)
        {
            name = n;
            type = "Weapon";
            info = i;
            weight = we;
            ability = ab;
            damageType = dt;
            numDmgDice = ndd;
            dmgDice = dd;
            range = r;
            isProficient = ip;
        }

        public void changeAbility()
        {
            if (ability == "Strength")
            {
                ability = "Dexterity";
            }
            else { ability = "Strength"; }
        }

        public string getAbility()
        {
            return ability;
        }

        public string getAttackRoll()
        {
            if(atkBonus >= 0)
            {
                return "1d20 + " + atkBonus;
            }
            else
            {
                return "1d20 - " + Math.Abs(atkBonus);
            }
        }

        public string getDamageRoll()
        {
            return numDmgDice + "d" + dmgDice + " + " + dmgBonus;
        }

        public string getDamageBonus()
        {
            if(dmgBonus >= 0)
            {
                return "+ " + dmgBonus;
            }
            else
            {
                return "- " + Math.Abs(dmgBonus);
            }
        }

        public bool getProficiency()
        {
            return isProficient;
        }

        public void updateProficiency(bool isProf)
        {
            isProficient = isProf;
            UpdateP(profBonus);
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

        public override string toSaveString()
        {
            string saveString = "";
            saveString += type + "\n";
            saveString += name + "\n";
            saveString += info + "\n";
            saveString += weight + "\n";
            saveString += ability + "\n";
            saveString += damageType + "\n";
            saveString += numDmgDice + "d" + dmgDice + "\n";
            saveString += range + "\n";
            saveString += isProficient + "\n";

            return saveString;
        }
    }

    class Armor : Item
    {
        public int armorClass;
        public bool equipped;

        public Armor()
        {
            name = "New Armor";
            type = "Armor";
            info = "Info / Description";
            weight = 0;
            armorClass = 0;
            equipped = false;
        }

        public Armor(string n, string i, int w, int ac, bool eq)
        {
            name = n;
            type = "Armor";
            info = i;
            weight = w;
            armorClass = ac;
            equipped = eq;
        }

        public override string ToString()
        {
            return name;
        }

        public override string toSaveString()
        {
            string saveString = "";
            saveString += type + "\n";
            saveString += name + "\n";
            saveString += info + "\n";
            saveString += weight + "\n";
            saveString += armorClass + "\n";
            saveString += equipped + "\n";

            return saveString;
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

        public Misc(string n, string i, int w)
        {
            name = n;
            type = "Misc";
            info = i;
            weight = w;
        }

        public override string ToString()
        {
            return name;
        }

        public override string toSaveString()
        {
            string saveString = "";
            saveString += type + "\n";
            saveString += name + "\n";
            saveString += info + "\n";
            saveString += weight + "\n";

            return saveString;
        }
    }
}
