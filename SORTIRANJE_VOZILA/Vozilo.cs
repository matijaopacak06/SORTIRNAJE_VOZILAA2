using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORTIRANJE_VOZILA
{
    [Serializable]
    internal class Vozilo
    {
        private string Marka;
        private string Model;
        private int GodinaProizvodnje;
        private int Kilometraza;

        public string Marka1 { get => Marka; set => Marka = value; }
        public string Model1 { get => Model; set => Model = value; }
        public int GodinaProizvodnje1 { get => GodinaProizvodnje; set => GodinaProizvodnje = value; }
        public int Kilometraza1 { get => Kilometraza; set => Kilometraza = value; }
    }
}
