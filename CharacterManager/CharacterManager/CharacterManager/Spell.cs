using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager
{
    public abstract class Spell
    {
        public int level;
        public string spellClass;
        public string castTime;
        public string duration;
        public string range;
        public string school;
        public string components;
        public string info;
        public string save;
    }

    class DamageSpell : Spell
    {
        public string damage;
        public string damageType;
        public string attack;
    }

    class UtilitySpell : Spell
    {

    }
}
