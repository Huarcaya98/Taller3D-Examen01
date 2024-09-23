using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen01_Taller3D
{
    public class Personaje
    {

        public string nombre;
        public int vida;
        public int danio;
        
        public Personaje (string nombre, int vida, int danio)
        {
            this.nombre = nombre;
            this.vida = vida;
            this.danio = danio;
        }

        public virtual void Atacar(Personaje objetivo)
        {
            objetivo.vida -= danio;
            Console.WriteLine($"{nombre} atacó a {objetivo.nombre} y causó {danio} de daño");
            if(objetivo.vida <= 0)
            {
                Console.WriteLine($"{objetivo.nombre} ha muerto");
            }
        }

        public bool EstaMuerto()
        {
            return vida <= 0;
        }


    }
}
