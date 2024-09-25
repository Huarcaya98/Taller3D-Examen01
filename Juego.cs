using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Examen01_Taller3D
{
    public class Juego
    {
        private Jugador jugador;
        private List<Enemigo> enemigos = new List<Enemigo>();

        public void configurarJuego()
        {
            
            Console.WriteLine("Crea tu personaje");
            Console.WriteLine("Ingresa el nombre del personaje");
            string nombreJugador = Console.ReadLine();
            Console.WriteLine("Ingresa la cantidad de vida");
            int vidaJugador = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingresa el daño del personaje");
            int danioJugador = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingresa el mana del personaje");
            int manaJugador = int.Parse(Console.ReadLine());

            jugador = new Jugador(nombreJugador, vidaJugador, danioJugador, manaJugador);

            Console.WriteLine("¿Cuántos poderes quieres añadir a tu personaje?");
            int numPoderes = int.Parse(Console.ReadLine());

            for (int i = 0; i < numPoderes; i++)
            {
                Console.WriteLine($"Ingresa el nombre del poder {i + 1}:");
                string nombrePoder = Console.ReadLine();
                Console.WriteLine("Ingresa el daño del poder:");
                int danioPoder = int.Parse(Console.ReadLine());
                Console.WriteLine("Ingresa el costo de mana del poder:");
                int costoManaPoder = int.Parse(Console.ReadLine());

                jugador.poderes.Add(new Poder(nombrePoder, danioPoder, costoManaPoder));
            }

            
            Console.WriteLine("¿Cuántos enemigos quieres crear?");
            int numEnemigos = int.Parse(Console.ReadLine());

            for (int i = 0; i < numEnemigos; i++)
            {
                Console.WriteLine($"Ingresa el nombre del enemigo {i + 1}:");
                string nombreEnemigo = Console.ReadLine();
                Console.WriteLine("Ingresa la cantidad de vida del enemigo:");
                int vidaEnemigo = int.Parse(Console.ReadLine());
                Console.WriteLine("Ingresa el daño del enemigo:");
                int danioEnemigo = int.Parse(Console.ReadLine());

                
                Console.WriteLine($"Ingresa el valor de vida que el enemigo {i + 1} tiene como ítem:");
                int valorVidaItem = int.Parse(Console.ReadLine());
                Console.WriteLine($"Ingresa el valor de mana que el enemigo {i + 1} tiene como ítem:");
                int valorManaItem = int.Parse(Console.ReadLine());

                Enemigo enemigo = new Enemigo(nombreEnemigo, vidaEnemigo, danioEnemigo);
                enemigo.items.Add(new ItemVida("Poción de Vida", valorVidaItem));
                enemigo.items.Add(new ItemMana("Poción de Mana", valorManaItem));

                enemigos.Add(enemigo);
            }
        }

        public void Iniciar()
        {
            int enemigoActual = 0;

            while (!jugador.EstaMuerto() && enemigoActual < enemigos.Count)
            {
                Enemigo enemigo = enemigos[enemigoActual];

                if (!enemigo.EstaMuerto())
                {
                    
                    Console.WriteLine("=== Batalla 1v1 ===");
                    Console.WriteLine($"Jugador: {jugador.nombre} - Vida: {jugador.vida}, Mana: {jugador.mana}");
                    Console.WriteLine($"Enemigo: {enemigo.nombre} - Vida: {Math.Max(enemigo.vida, 0)}");
                    Console.WriteLine("===================");

                    bool turnoTerminado = false;

                    while (!jugador.EstaMuerto() && !enemigo.EstaMuerto() && !turnoTerminado)
                    {
                        
                        Console.WriteLine("Es tu turno.");
                        Console.WriteLine("1. Usar item");
                        Console.WriteLine("2. Atacar enemigo");
                        int eleccion = int.Parse(Console.ReadLine());

                        if (eleccion == 1 && jugador.items.Count > 0)
                        {
                           
                            Console.WriteLine("Selecciona un item:");
                            for (int i = 0; i < jugador.items.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}: {jugador.items[i].nombre}");
                            }

                            int eleccionItem = int.Parse(Console.ReadLine()) - 1;
                            jugador.UsarItem(jugador.items[eleccionItem]);
                            turnoTerminado = true;
                        }
                        else if (eleccion == 2)
                        {
                            Console.WriteLine("Selecciona un ataque:");
                            Console.WriteLine("1. Ataque simple");
                            Console.WriteLine("2. Usar poder");
                            int eleccionAtaque = int.Parse(Console.ReadLine());

                            if (eleccionAtaque == 1)
                            {
                                
                                jugador.Atacar(enemigo);
                                turnoTerminado = true;
                            }
                            else if (eleccionAtaque == 2 && jugador.poderes.Count > 0)
                            {
                                
                                Console.WriteLine("Selecciona un poder:");
                                for (int i = 0; i < jugador.poderes.Count; i++)
                                {
                                    Console.WriteLine($"{i + 1}: {jugador.poderes[i].nombre} (Costo: {jugador.poderes[i].costoMana} Mana)");
                                }

                                int eleccionPoder = int.Parse(Console.ReadLine()) - 1;
                                if (eleccionPoder >= 0 && eleccionPoder < jugador.poderes.Count)
                                {
                                    jugador.poderes[eleccionPoder].Usar(jugador, enemigo);
                                    turnoTerminado = true;
                                }
                            }
                        }

                        
                        Console.WriteLine($"Estado actual: {jugador.nombre} - Vida: {jugador.vida}, Mana: {jugador.mana}");
                        Console.WriteLine($"Estado actual: {enemigo.nombre} - Vida: {Math.Max(enemigo.vida, 0)}");

                        if (enemigo.EstaMuerto())
                        {
                            Console.WriteLine($"{enemigo.nombre} ha sido derrotado.");

                            
                            foreach (var item in enemigo.items)
                            {
                                jugador.items.Add(item);
                                Console.WriteLine($"{jugador.nombre} ha recibido {item.nombre}.");
                            }

                            enemigoActual++; 
                            break;
                        }
                    }

                    
                    if (!jugador.EstaMuerto() && !enemigo.EstaMuerto())
                    {
                        Console.WriteLine($"Es el turno de {enemigo.nombre}.");
                        enemigo.Atacar(jugador);

                        
                        Console.WriteLine($"Estado actual: {jugador.nombre} - Vida: {jugador.vida}, Mana: {jugador.mana}");
                        Console.WriteLine($"Estado actual: {enemigo.nombre} - Vida: {Math.Max(enemigo.vida, 0)}");
                    }

                    if (jugador.EstaMuerto())
                    {
                        Console.WriteLine("Has muerto. Game Over.");
                        return;
                    }
                }
            }

            if (!jugador.EstaMuerto())
            {
                Console.WriteLine("Has derrotado a todos los enemigos. ¡Victoria!");
            }
            Console.ReadKey();
        }
        
    }
}
