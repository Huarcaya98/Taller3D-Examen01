using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen01_Taller3D
{
    public class Item
    {
        public string nombre;
        public int valor;

        protected Item(string nombre, int valor)
        {
            this.nombre = nombre;
            this.valor = valor;
        }
      

        
    }
}
