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

        public override string ToString()
        {
            return name;
        }
    }
}
