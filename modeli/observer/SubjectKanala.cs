using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ttomiek_zadaca_1.klase;

namespace ttomiek_zadaca_1.observer
{
    public abstract class SubjectKanala
    {
        private int _idKanala;
        private int _trenutniKapacitet;
        
        private List<Observer> _observers = new List<Observer>();

        public SubjectKanala(int idKanala, int kapacitet)
        {
            this._idKanala = idKanala;
            this._trenutniKapacitet = kapacitet;
        }

        public void Attach(Observer observer)
        {
            _observers.Add(observer);
        }

        public void Detach(Observer observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (Observer o in _observers)
            {
                o.Update(this);
            }
        }
        
        public int TrenutniKapacitet
        {
            get { return _trenutniKapacitet; }
            set
            {
                if (_trenutniKapacitet != value){
                    _trenutniKapacitet = value;
                    Notify();
                }
            }
        }

        public int IdKanala 
        { 
            get { return _idKanala; } 
        }
    }
}
