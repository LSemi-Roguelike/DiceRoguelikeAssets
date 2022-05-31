namespace LSemiRoguelike
{
    [System.Serializable]
    public struct Ability
    {
        //attack
        public float attackIncrese;
        public float attackMulti;

        //damaged
        public float damageReduce;
        public float damageMulti;

        Condition attackCon;
        Condition resistCon;

        public Ability(float attackIncrese, float attackMulti, float damageReduce, float damageMulti) : this(attackIncrese, attackMulti, damageReduce, damageMulti, new Condition(), new Condition()) { }
        public Ability(float attackIncrese, float attackMulti, float damageReduce, float damageMulti, Condition attackCon, Condition resistCon)
        {
            this.attackIncrese = attackIncrese; 
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
                "Attack Increase: " + attackIncrese + "\tAttack Multi: " + attackMulti +
                "\nDamage Reduce: " + damageReduce +"\tDamage Multi " + damageMulti
                ;
        }
    }
}