using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager
{
    public abstract class Spell
    {
        protected string type;
        public string name;
        public string level;
        public string castTime;
        public string duration;
        public string range;
        public string school;
        public string components;
        public string info;
        public string save;

        public abstract override string ToString();
        public abstract string toSaveString();

        public string getType()
        {
            return type;
        }
    }

    class DamageSpell : Spell
    {
        public string damage;
        public string damageType;

        public DamageSpell()
        {
            name = "New Damage Spell";
            type = "Damage";
            level = "1";
            castTime = "";
            duration = "";
            range = "";
            school = "";
            components = "";
            info = "";
            save = "";
            damage = "";
            damageType = "";
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
            saveString += level + "\n";
            saveString += castTime + "\n";
            saveString += duration + "\n";
            saveString += range + "\n";
            saveString += school + "\n";
            saveString += components + "\n";
            saveString += info + "\n";
            saveString += save + "\n";
            saveString += damage + "\n";
            saveString += damageType + "\n";
            return saveString;
        }

        public DamageSpell(string n, string l, string ct, string d, string r, string s, string c, string i, string sv, string dmg, string dmgt)
        {
            name = n;
            type = "Damage";
            level = l;
            castTime = ct;
            duration = d;
            range = r;
            school = s;
            components = c;
            info = i;
            save = sv;
            damage = dmg;
            damageType = dmgt;
        }
    }

    class UtilitySpell : Spell
    {
        public UtilitySpell()
        {
            name = "New Utility Spell";
            type = "Utility";
            level = "1";
            castTime = "";
            duration = "";
            range = "";
            school = "";
            components = "";
            info = "";
            save = "";
        }

        public UtilitySpell(string n, string l, string ct, string d, string r, string s, string c, string i, string sv)
        {
            name = n;
            type = "Utility";
            level = l;
            castTime = ct;
            duration = d;
            range = r;
            school = s;
            components = c;
            info = i;
            save = sv;
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
            saveString += level + "\n";
            saveString += castTime + "\n";
            saveString += duration + "\n";
            saveString += range + "\n";
            saveString += school + "\n";
            saveString += components + "\n";
            saveString += info + "\n";
            saveString += save + "\n";
            return saveString;
        }
    }
}
