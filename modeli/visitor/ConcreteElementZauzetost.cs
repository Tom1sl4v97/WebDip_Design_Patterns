namespace ttomiek_zadaca_1.visitor
{
    public class ConcreteElementZauzetost : Element
    {
        private string _vrstaVeza;
        private DateTime _vrijeme;

        public ConcreteElementZauzetost(string vrstaVeza, DateTime vrijeme)
        {
            _vrstaVeza = vrstaVeza;
            _vrijeme = vrijeme;
        }

        public string vrstaVeza
        {
            get { return _vrstaVeza; }
            set { _vrstaVeza = value; }
        }

        public DateTime vrijeme
        {
            get { return _vrijeme; }
            set { _vrijeme = value; }
        }

        public override void Accept(Visitor visitor)
        {
            visitor.VisitConcreteElementZauzetost(this);
        }
    }
}
