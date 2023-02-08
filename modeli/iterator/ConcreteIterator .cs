namespace ttomiek_zadaca_2.iterator
{
    public class ConcreteIterator : Iterator
    {
        ConcreteAggregate aggregate;
        int current = 0;

        public ConcreteIterator(ConcreteAggregate aggregate)
        {
            this.aggregate = aggregate;
        }
        
        public override object DohvatiPrvoga()
        {
            return aggregate[0];
        }
        
        public override object Sljedeci()
        {
            object ret = null;
            if (current < aggregate.Count - 1)
            {
                ret = aggregate[++current];
            }
            return ret;
        }
        
        public override object TrenutniElement()
        {
            return aggregate[current];
        }
        
        public override bool Zavrseno()
        {
            return current >= aggregate.Count;
        }
    }
}
