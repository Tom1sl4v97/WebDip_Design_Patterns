namespace ttomiek_zadaca_2.iterator
{
    public class ConcreteAggregate : Aggregate
    {
        List<object> popisElemenata = new List<object>();
        public override Iterator KreirajIterator()
        {
            return new ConcreteIterator(this);
        }
        public int Count
        {
            get { return popisElemenata.Count; }
        }
        public object this[int index]
        {
            get { return popisElemenata[index]; }
            set { popisElemenata.Insert(index, value); }
        }
    }
}
