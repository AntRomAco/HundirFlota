using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundirFlota
{
    internal class Program
    {
        /*
         * Programa principal para manejar el juego "Hundir la Flota".
         */
        static void Main(string[] args)
        {
            Console.SetWindowSize(17, 15); // Se modifica el ancho y alto de la consola para hacerla más vistosa.
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth; // Se configura el buffer de la consola a las dimensiones configuradas.
            Console.CursorVisible = false; // Se esconde el cursor de la consola.
            bool salir = false; // Bool para controlar el bucle y terminar el programa.
            while (!salir)
            {
                Console.Clear(); // Menú principal
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" ╔══════════════╗");
                Console.WriteLine(" ║  BATTLESHIP  ║");
                Console.WriteLine(" ╚══════════════╝");
                Console.ResetColor();
                Console.WriteLine("\nEnter - Empezar");
                Console.WriteLine("ESC - Salir");
                ConsoleKeyInfo teclaInicio = Console.ReadKey(true); // Se configura las teclas para controlar el menú.
                if (teclaInicio.Key == ConsoleKey.Escape) { salir = true; break; }
                if (teclaInicio.Key == ConsoleKey.Enter)
                {
                    int turno = 0; // Turnos que pasan.
                    int jugador = 0; // 0 = Jugador, 1 = Ordenador
                    bool victoria = false; // Bool controla el bucle del juego para cuando hay victoria.
                    int barcosJugador = 5; // Controla cuantos barcos tiene el jugador aún flotando.
                    int barcosOrdenador = 5; // Controla cuantos barcos tiene el ordenador aún flotando.
                    int[,] ataquesJugador = new int[8, 8]; // Almacena los ataques que el jugador ya ha realizado.
                    int[,] ataquesOrdenador = new int[8, 8]; // Almacena los ataques que el ordenador ya ha realizado.
                    int fila;
                    int columna;
                    Random r = new Random();
                    Tablero tableroJugador = new Tablero(); // Inicializa un nuevo tablero para el jugador.
                    Tablero tableroOrdenador = new Tablero(); // Inicializa un nuevo tablero para el ordenador.
                    while (!victoria || salir)
                    {
                        turno++;
                        bool valido = false;
                        while (!valido && !victoria)
                        {
                            Console.Clear();
                            jugador = 0;
                            tableroOrdenador.tableroOrdenador(); // Muestra el tablero del ordenador al jugador.
                            Console.WriteLine("Jugador");
                            try
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("Fila: ");
                                fila = Convert.ToInt32(Console.ReadLine());
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Columna: ");
                                columna = Convert.ToInt32(Console.ReadLine());
                                Console.ResetColor();
                                if (fila >= 8 || fila < 0 || columna >= 8 || columna < 0)
                                {
                                    Console.WriteLine("Inválido!");
                                    Thread.Sleep(2500);
                                }
                                else if (ataquesJugador[fila, columna] == 1)
                                {
                                    Console.WriteLine("Ya atacada!");
                                    Thread.Sleep(2500);
                                }
                                else
                                {
                                    Console.Clear();
                                    ataquesJugador[fila, columna] = 1;
                                    valido = true;
                                    bool ataque = tableroOrdenador.ataque(fila, columna);
                                    tableroOrdenador.tableroOrdenador();
                                    if (ataque)
                                    {
                                        barcosOrdenador--;
                                        Console.WriteLine("Diana!");
                                        Thread.Sleep(2500);
                                        if (barcosOrdenador == 0)
                                        {
                                            victoria = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Fallo");
                                        Thread.Sleep(2500);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Inválido!");
                                Thread.Sleep(2500);
                            }
                        }
                        valido = false;
                        while (!valido && !victoria)
                        {
                            Console.Clear();
                            jugador = 1;
                            tableroJugador.tableroJugador(); // Muestra el tablero del jugador al ordenador.
                            Console.WriteLine("Ordenador");
                            fila = r.Next(0, 8);
                            columna = r.Next(0, 8);
                            if (ataquesOrdenador[fila, columna] != 1)
                            {
                                ataquesOrdenador[fila, columna] = 1;
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Fila: " + fila);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Columna: " + columna);
                                Console.ResetColor();
                                Thread.Sleep(2500);
                                valido = true;
                                bool ataque = tableroJugador.ataque(fila, columna);
                                Console.Clear();
                                tableroJugador.tableroJugador();
                                if (ataque)
                                {
                                    barcosJugador--;
                                    Console.WriteLine("Diana!");
                                    Thread.Sleep(2500);
                                    if (barcosJugador == 0)
                                    {
                                        victoria = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Fallo");
                                    Thread.Sleep(2500);
                                }
                            }
                        }
                    }
                    if (jugador == 0)
                    {
                        Console.Clear(); // Menú de la pantalla de victoria.
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("  ╔════════════╗");
                        Console.WriteLine("  ║  VICTORIA  ║");
                        Console.WriteLine("  ╚════════════╝");
                        Console.ResetColor();
                        Console.WriteLine("\nTurnos: " + turno);
                        Console.WriteLine("\nEnter - Título");
                        Console.WriteLine("ESC - Salir");
                        ConsoleKeyInfo teclaFinal = Console.ReadKey(true); // Se configura las teclas para controlar el menú.
                        if (teclaFinal.Key == ConsoleKey.Enter) { continue; }
                        if (teclaFinal.Key == ConsoleKey.Escape) { salir = true; break; }
                    }
                    else
                    {
                        Console.Clear(); // Menú de la pantalla de derrota.
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("   ╔═══════════╗");
                        Console.WriteLine("   ║  DERROTA  ║");
                        Console.WriteLine("   ╚═══════════╝");
                        Console.ResetColor();
                        Console.WriteLine("\nTurnos: " + turno);
                        Console.WriteLine("\nEnter - Título");
                        Console.WriteLine("ESC - Salir");
                        ConsoleKeyInfo teclaFinal = Console.ReadKey(true);
                        if (teclaFinal.Key == ConsoleKey.Enter) { continue; } // Se configura las teclas para controlar el menú.
                        if (teclaFinal.Key == ConsoleKey.Escape) { salir = true; break; }
                    }
                }
            }
        }
    }
}
