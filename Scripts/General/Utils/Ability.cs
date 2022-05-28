namespace LSemiRoguelike
{
    [System.Serializable]
    public struct Ability
    {
        //attack
        public float attackAdd;
        public float attackMulti;

        //damaged
        public float damageReduce;
        public float damageMulti;

        Condition attackCon;
        Condition resistCon;

        public Ability(float attackAdd, float attackMulti, float damageReduce, float damageMulti) : this(attackAdd, attackMulti, damageReduce, damageMulti, new Condition(), new Condition()) { }
        public Ability(float attackAdd, float attackMulti, float damageReduce, float damageMulti, Condition attackCon, Condition resistCon)
        {
            this.attackAdd = attackAdd; 
            this.attackMulti = attackMulti;
            this.damageReduce = damageReduce;
            this.damageMulti = damageMulti;
            this.attackCon = attackCon;
            this.resistCon = resistCon;
        }

        public static Ability one => new Ability(1, 1, 1, 1);

        public override string ToString()
        {
            return
                "Attack Add: " + attackAdd +"\tAttack Multi: " + attackMulti +
                "\nDamage Reduce: " + damageReduce +"\tDamage Multi " + damageMulti
                ;
        }
    }
}