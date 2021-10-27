using System;
using System.Collections.Generic;
namespace TheGame001
{
    public class Player  : Monster
    {

 
        private int currentMaxHp;
        //private int xp;
        private int level;

        private Weapon usedWeapon;
        private Armor wornArmor;

 
        public override int HitChance 
        {
            get => hitChance + usedWeapon.ToHitBonus;
            set
            {
 
                hitChance = value;
            }
        }

        public override int HitDamage  
        {
            get => hitDamage + usedWeapon.DamageBonus;
            set
            {
 
                hitChance = value;
            }
        }

        public int Level
        {
            get => level;
            set
            {
                level = value;
            }
        }

      public Armor WornArmor
        {
            get => wornArmor;
            set
            {
                wornArmor = value;
            }
        }
        public Weapon UsedWeapon
        {
            get => usedWeapon;
            set
            {
                usedWeapon = value;
            }
        }


        public void Display()
        {
 
            Console.WriteLine("Namn: "+name);
            Console.WriteLine("HP: "+hp+"/"+currentMaxHp);
            Console.WriteLine("XP: "+xp);
            Console.WriteLine("Träffchans: "+hitChance+"% ("+HitChance+"%)");
            Console.WriteLine("Skada: "+hitDamage+" p. ("+HitDamage+" p.)");
            Console.Write("Utrustning: " );
            foreach (var item in inventory)
            {
                Console.Write(item.Name+" ");
            }
            Console.WriteLine("");
            Console.WriteLine("Guld: "+gold);
            Console.WriteLine("Använder Vapen: "+UsedWeapon.Name);
            Console.WriteLine("Bär Rustning: " + WornArmor.Name);

            Console.ReadKey();
        }
        public void LevelUp ()
        {
            while (xp / (level * 1000) > 1)
            {
                level++;
                int hitsTaken = currentMaxHp - HP;
                hp = currentMaxHp;
                hp = hp * 6 / 5;
                currentMaxHp = HP;
                hp -= hitsTaken;
                hitDamage = HitDamage * 8 / 7;
                hitChance = HitChance + (100 - hitChance)/5;
                Console.WriteLine(Name + " LEVLADE UPP TILL LEVEL " + level + ", HP är nu " + HP + ", träffchans är nu " + HitChance + "\n"
                    + Name + " gör nu " + HitDamage + " hp skada");
            }
                Console.ReadKey();
            
        }

        public void Consume(Consumable consumable)
        {
            // nånting här

        }

        public Player (string myName, char sign) : base (   myName, sign)
        {
            Random rnd = new Random();
 
            Weapon Fist = (Weapon)Miscellaneous.ExistingThings["Knytnäve"];
            Armor Shirt = (Armor)Miscellaneous.ExistingThings["Skjorta"];
            Armor BirthDaySuit = (Armor)Miscellaneous.ExistingThings["Bara Mässingen"];
 
            this.position = new int[] { 4, 4 };
            this.hp=rnd.Next(80,120);
            this.xp=0;
            this.currentMaxHp = HP;
            this.hitChance= rnd.Next(10, 35); 
            this.hitDamage = rnd.Next(5, 25); ;
            this.damageReduction=0;
            this.level=1;
            
            this.inventory = new List<Item> { Miscellaneous.ExistingThings["Get"], Miscellaneous.ExistingThings["Svärd"], Miscellaneous.ExistingThings["CocaCola"] };
            this.gold=55;
            this.usedWeapon = Fist;
            this.wornArmor = Shirt;
        }

    }
}
