using System;
using System.Collections.Generic;


namespace TheGame001
{
    public static class Miscellaneous
    {

        public static List<Item> ThingsThatExist = new List<Item>// ändra till dictionary(string,Item)
        {
           new Item {Name ="Get", SuggestedPrice=1, Description="En värdelös get. Bäää!" },
           new Weapon {Name ="Knytnäve", SuggestedPrice=0, Description="Kilroy_was_here", DamageBonus=1, ToHitBonus=1, Sellable=false},

           new Weapon {Name ="Dolk", SuggestedPrice=20, Description="En dolk. +5 skada. Hugg med den spetsiga änden", DamageBonus=5, ToHitBonus=0, Sellable=true},
           new Weapon {Name ="Svärd", SuggestedPrice=150, Description="Ett svärd. +20 skada", DamageBonus=20, ToHitBonus=0, Sellable=true},
           new Weapon {Name ="Yxa", SuggestedPrice=100, Description="En yxa. +40 skada, -20 träffchans", DamageBonus=40, ToHitBonus=-20, Sellable=true},
           new Armor {Name ="Skjorta", SuggestedPrice=1, Description="En skjorta - the shirt off your back.",  ToHitBonus=0, DamageReductionBonus=0},
           new Armor {Name ="Läderrustning", SuggestedPrice=20, Description="Läderställ med Hells-Angels-logga. +5 skadereduktion ",  ToHitBonus=0, DamageReductionBonus=5},
           new Armor {Name ="Ringbrynja", SuggestedPrice=75, Description="Ringbrynja. +20 skadereduktion",  ToHitBonus=0, DamageReductionBonus=20},
           new Armor {Name ="Kyrass", SuggestedPrice=1, Description="Ett harnesk av 4mm stålplåt S255JR. -20 träffchans, +40 skadereduktion",  ToHitBonus=-20, DamageReductionBonus=0},
           new Equipable {Name ="Styrkeamulett", SuggestedPrice=50, Description="Ett förtrollat simborgarmärke. +5 skada.",  ToHitBonus=0, DamageReductionBonus=0, HpBonus=0, Damagebonus=5},
           new Equipable {Name ="Hälsoamulett", SuggestedPrice=50, Description="Gammalt \"kärnkraft- nej tack\" - märke med stark hippie-magi. +5 hp.",  ToHitBonus=0, DamageReductionBonus=0, HpBonus=5, Damagebonus=0},
           new Consumable {Name ="CocaCola", SuggestedPrice=50, Description="CocaCola - It's the real thing! +20 hp i 20 rundor",  ToHitBonus=0, DamageReductionBonus=0, HpBonus=20, Damagebonus=0, Duration=20},
           new Consumable {Name ="PepsiCola", SuggestedPrice=50, Description="PepsiCola - The Choice of a New Generation! +20 skada i 20 rundor",  ToHitBonus=0, DamageReductionBonus=0, HpBonus=5, Damagebonus=20, Duration=20}
        };

        public static Dictionary<string, Item> ExistingThings = new Dictionary<string, Item>()
        {
            { "Get", new Item { Name = "Get", SuggestedPrice = 1, Description = "En värdelös get. Bäää!" }},
            { "Knytnäve", new Weapon {Name ="Knytnäve", SuggestedPrice=0, Description="Kilroy was here", DamageBonus=1, ToHitBonus=1, Sellable=false} },
            { "Dolk",new Weapon {Name ="Dolk", SuggestedPrice=20, Description="En dolk. +5 skada. Hugg med den spetsiga änden", DamageBonus=5, ToHitBonus=0, Sellable=true}},
            { "Svärd",new Weapon {Name ="Svärd", SuggestedPrice=150, Description="Ett svärd. +20 skada", DamageBonus=20, ToHitBonus=0, Sellable=true}},
            { "Yxa",new Weapon {Name ="Yxa", SuggestedPrice=100, Description="En yxa. +40 skada, -20 träffchans", DamageBonus=40, ToHitBonus=-20, Sellable=true}},
            { "Bara Mässingen",new Armor {Name ="Bara Mässingen", SuggestedPrice=1, Description="Kilroy Was here",  ToHitBonus=0, DamageReductionBonus=0}},         //ge armor SellablePropsen och sätt till false
            { "Skjorta",new Armor {Name ="Skjorta", SuggestedPrice=1, Description="En skjorta - the shirt off your back.",  ToHitBonus=0, DamageReductionBonus=1}},
            { "Läderrustning",new Armor {Name ="Läderrustning", SuggestedPrice=20, Description="Läderställ med Hells-Angels-logga. +5 skadereduktion ",  ToHitBonus=0, DamageReductionBonus=5}},
            { "Ringbrynja",new Armor {Name ="Ringbrynja", SuggestedPrice=75, Description="Ringbrynja. +20 skadereduktion",  ToHitBonus=0, DamageReductionBonus=20}},
            { "Kyrass",new Armor {Name ="Kyrass", SuggestedPrice=1, Description="Ett harnesk av 4mm stålplåt S255JR. -20 träffchans, +40 skadereduktion",  ToHitBonus=-20, DamageReductionBonus=0}},
            { "Styrkeamulett",new Equipable {Name ="Styrkeamulett", SuggestedPrice=50, Description="Ett förtrollat simborgarmärke. +5 skada.",  ToHitBonus=0, DamageReductionBonus=0, HpBonus=0, Damagebonus=5}},
            { "Hälsoamulett",new Equipable {Name ="Hälsoamulett", SuggestedPrice=50, Description="Gammalt \"kärnkraft- nej tack\" - märke med stark hippie-magi. +5 hp.",  ToHitBonus=0, DamageReductionBonus=0, HpBonus=5, Damagebonus=0}},
            { "CocaCola",new Consumable {Name ="CocaCola", SuggestedPrice=50, Description="CocaCola - It's the real thing! +20 hp i 20 rundor",  ToHitBonus=0, DamageReductionBonus=0, HpBonus=20, Damagebonus=0, Duration=20}},
            { "PepsiCola",new Consumable {Name ="PepsiCola", SuggestedPrice=50, Description="PepsiCola - The Choice of a New Generation! +20 skada i 20 rundor",  ToHitBonus=0, DamageReductionBonus=0, HpBonus=5, Damagebonus=20, Duration=20}}
        };

 
        //public static List<Monster> GenericMonsterTemplates = new List<Monster>
        //{
        //    new Monster("minitaur", 'm') {XP=250,HP=rnd.Next(30,80)},
        //};
    }

}





