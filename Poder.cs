using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen01_Taller3D
{
    public class Poder
    {
        public string nombre;
        public int danio;
        public int costoMana;

        public Poder(string nombre, int danio, int costoMana)
        {
            this.nombre = nombre;
            this.danio = danio;
            this.costoMana = costoMana;
        }

        public void Usar(Jugador jugador, Personaje objetivo)
        {
            if(jugador.mana >= costoMana)
            {
                jugador.mana -= costoMana;
                objetivo.vida -= danio;
                Console.WriteLine($"{jugador.nombre} usó {nombre} en {objetivo.nombre} y causó {danio} de daño");

                if(objetivo.vida <=0)
                {
                    Console.WriteLine($"{objetivo.nombre} ha muerto");
                }
            }
            else
            {
                Console.WriteLine($"{jugador.nombre} no tiene sufuciente mana para usar {nombre}");
            }
        }
        

    }
}
