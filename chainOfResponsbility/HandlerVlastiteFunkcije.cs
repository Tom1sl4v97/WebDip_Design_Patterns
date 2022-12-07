using System.Text.RegularExpressions;

namespace ttomiek_zadaca_1.chainOfResponsbility
{
    public abstract class HandlerVlastiteFunkcije
    {
        protected HandlerVlastiteFunkcije successor;

        public void SetSuccessor(HandlerVlastiteFunkcije successor)
        {
            this.successor = successor;
        }
        
        public abstract void ProcesRequest(Match matcherKomande);
    }
}
