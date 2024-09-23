using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen01_Taller3D
{
    public  class Jugador : Personaje
    {
        public int mana;
        public List<Poder> poderes = new List<Poder>();

        public Jugador(string nombre, int vida, int danio, int mana)
            :base(nombre, vida , danio)
        {
            this.mana = mana;
        }

        public override void Atacar(Personaje objetivo)
        {

            Console.WriteLine($"{nombre} , selecciona un ataque: 1. Ataque simple, 2. Usar poder");
            int eleccion = int.Parse(Console.ReadLine());

            if(eleccion == 2 && poderes.Count>0)
            {

                Console.WriteLine("Selecciona un poder");
                for(int i = 0; i < poderes.Count; i++)
                {
                    Console.WriteLine($"{i + 1}.{poderes[i].nombre} - costo: {poderes[i].costoMana} Mana");
                }

                int eleccionPoder = int.Parse(Console.ReadLine()) - 1;
                if(eleccionPoder >= 0 && eleccionPoder < poderes.Count)
                {
                    poderes[eleccionPoder].Usar(this, objetivo);
                    return;
                }

            }base.Atacar(objetivo);

        }

        public void UsarItem( Item item)
        {

            if(item is ItemVida)
            {
                vida += item.valor;
                Console.WriteLine($"{nombre} usó {item.nombre} y recuperó {item.valor} de vida");
            }
            else if (item is ItemMana)
            {
                mana += item.valor;
                Console.WriteLine($"{nombre} usó {item.nombre} y recuperó {item.valor} de mana ");
            }
            items.Remove(item);
        }

    }
}
