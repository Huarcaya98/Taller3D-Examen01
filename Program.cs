﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen01_Taller3D
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Juego juego = new Juego();
            juego.configurarJuego();
            juego.Iniciar();

        }
    }
}
