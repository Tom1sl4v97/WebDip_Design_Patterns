namespace ttomiek_zadaca_1.klase
{
    public class Greske
    {
        public int id { get; set; }
        private string _opisGreske = "";
        public string opisGreske 
        {
            get
            {
                return _opisGreske;
            }
            set
            {
                if (value == null || value == "")
                    throw new Exception("Greska mora imati opis");
                _opisGreske = value;
            }
        }
        public Greske(int id, string opisGreske)
        {
            this.id = id;
            this.opisGreske = opisGreske;
        }
    }
}
