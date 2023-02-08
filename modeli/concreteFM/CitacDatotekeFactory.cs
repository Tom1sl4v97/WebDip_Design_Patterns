using ttomiek_zadaca_1.@interface;

namespace ttomiek_zadaca_1.ConcreteFM
{
    public abstract class CitacDatotekeFactory
    {
        protected abstract CitanjeDatotekeInterface MakeCitac();

        public CitanjeDatotekeInterface CreateCitac()
        {
            return this.MakeCitac();
        }
    }
}
