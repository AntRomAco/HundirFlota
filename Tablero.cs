using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundirFlota
{
    internal class Tablero
    {
        /*
         * Programa para generar y controlar el tablero de juego.
         */
        private static readonly ILog Log = Logs.GetLogger();

        private static int filas = 8;
        private static int columnas = 8;
        private static int barcos = 5;
        private Casilla[,] casillas = new Casilla[filas, columnas];
        private Barco[] flota = new Barco[barcos];
        public Tablero() // Genera un tablero con sus barcos.
        {
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    casillas[i, j] = new Casilla(i,j);
                    casillas[i, j].setEstado(casillas[i, j].getAgua());
                }
            }
            Log.Info(lang.GetString("log18"));
            for (int i = 0; i < barcos; i++)
            {
                bool repetir = true;
                while (repetir) {
                    Barco nuevoBarco = Barco.GenerarBarco();
                    int posicionFila = nuevoBarco.getFila();
                    int posicionColumna = nuevoBarco.getColumna();
                    bool existe = false;
                    for (int j = 0; j < i; j++)
                    {
                        if (flota[j].Equals(nuevoBarco))
                        {
                            Log.Warn(lang.GetString("log19"));
                            existe = true;
                            break;
                        }
                    }
                    if (!existe)
                    {
                        Log.Info(lang.GetString("log20"));
                        flota[i] = nuevoBarco;
                        casillas[posicionFila, posicionColumna].setEstado(casillas[posicionFila, posicionColumna].getBarco());
                        repetir = false;
                    }
                }
            }
        }
        public void tableroJugador() // Visualiza el tablero del jugador, con barcos visibles.
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(lang.GetString("num"));
            Console.ResetColor();
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    Console.Write(casillas[i, j].getEstado() + " ");
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(i);
                Console.ResetColor();
            }
        }
        public void tableroOrdenador() // Visualiza el tablero del ordenador, sin barcos visibles.
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(lang.GetString("num"));
            Console.ResetColor();
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    if (casillas[i, j].getEstado() == casillas[i, j].getBarco())
                    {
                        Console.Write(casillas[i, j].getAgua() + " ");
                    } else { Console.Write(casillas[i, j].getEstado() + " "); }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(i);
                Console.ResetColor();
            }
        }
        public bool ataque(int fila, int columna) // Controla los ataques que se realizan al tablero.
        {
            if (casillas[fila, columna].getEstado() == casillas[fila, columna].getBarco())
            {
                casillas[fila, columna].setEstado(casillas[fila, columna].getTocado());
                Log.Info(lang.GetString("log21"));
                return true;
            } else
            {
                casillas[fila, columna].setEstado(casillas[fila, columna].getAtacado());
                Log.Info(lang.GetString("log22"));
                return false;
            }
        }
    }
}
