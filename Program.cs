using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace HundirFlota
{
    internal class Program
    {
        /*
         * Programa principal para manejar el juego "Hundir la Flota".
         */
        private static readonly ILog Log = Logs.GetLogger();
        static void Main(string[] args)
        {
            Console.SetWindowSize(17, 15); // Se modifica el ancho y alto de la consola para hacerla más vistosa.
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth; // Se configura el buffer de la consola a las dimensiones configuradas.
            Console.CursorVisible = false; // Se esconde el cursor de la consola.
            string langSelect = MenuIdiomas.LangMenu();
            lang.LoadStrings(langSelect);
            Log.Info(lang.GetString("log1"));
            bool salir = false; // Bool para controlar el bucle y terminar el programa.
            while (!salir)
            {
                Console.Clear(); // Menú principal
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(lang.GetString("title1"));
                Console.WriteLine(lang.GetString("title2"));
                Console.WriteLine(lang.GetString("title3"));
                Console.ResetColor();
                Console.WriteLine(lang.GetString("title4"));
                Console.WriteLine(lang.GetString("title5"));
                ConsoleKeyInfo teclaInicio = Console.ReadKey(true); // Se configura las teclas para controlar el menú.
                if (teclaInicio.Key == ConsoleKey.Escape) { salir = true;
                    Log.Info(lang.GetString("log2")); break; }
                if (teclaInicio.Key == ConsoleKey.Enter)
                {
                    Log.Info(lang.GetString("log3"));
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
                    Log.Info(lang.GetString("log4"));
                    Tablero tableroOrdenador = new Tablero(); // Inicializa un nuevo tablero para el ordenador.
                    Log.Info(lang.GetString("log5"));
                    while (!victoria || salir)
                    {
                        turno++;
                        bool valido = false;
                        while (!valido && !victoria)
                        {
                            Console.Clear();
                            jugador = 0;
                            tableroOrdenador.tableroOrdenador(); // Muestra el tablero del ordenador al jugador.
                            Console.WriteLine(lang.GetString("game1"));
                            try
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(lang.GetString("game2"));
                                fila = Convert.ToInt32(Console.ReadLine());
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(lang.GetString("game3"));
                                columna = Convert.ToInt32(Console.ReadLine());
                                Console.ResetColor();
                                if (fila >= 8 || fila < 0 || columna >= 8 || columna < 0)
                                {
                                    Log.Warn(lang.GetString("log6"));
                                    Console.WriteLine(lang.GetString("game4"));
                                    Thread.Sleep(2500);
                                }
                                else if (ataquesJugador[fila, columna] == 1)
                                {
                                    Log.Warn(lang.GetString("log7"));
                                    Console.WriteLine(lang.GetString("game5"));
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
                                        Log.Info(lang.GetString("log8"));
                                        barcosOrdenador--;
                                        Console.WriteLine(lang.GetString("game6"));
                                        Thread.Sleep(2500);
                                        if (barcosOrdenador == 0)
                                        {
                                            victoria = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        Log.Info(lang.GetString("log9"));
                                        Console.WriteLine(lang.GetString("game7"));
                                        Thread.Sleep(2500);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Error(lang.GetString("log10"));
                                Console.WriteLine(lang.GetString("game4"));
                                Thread.Sleep(2500);
                            }
                        }
                        valido = false;
                        while (!valido && !victoria)
                        {
                            Console.Clear();
                            jugador = 1;
                            tableroJugador.tableroJugador(); // Muestra el tablero del jugador al ordenador.
                            Console.WriteLine(lang.GetString("game8"));
                            fila = r.Next(0, 8);
                            columna = r.Next(0, 8);
                            if (ataquesOrdenador[fila, columna] != 1)
                            {
                                ataquesOrdenador[fila, columna] = 1;
                                Console.ForegroundColor = ConsoleColor.Red;
                                string filaMensaje = lang.GetString("game9");
                                Console.WriteLine(string.Format(filaMensaje, fila));
                                Console.ForegroundColor = ConsoleColor.Green;
                                string columnaMensaje = lang.GetString("game10");
                                Console.WriteLine(string.Format(columnaMensaje, columna));
                                Console.ResetColor();
                                Thread.Sleep(2500);
                                valido = true;
                                bool ataque = tableroJugador.ataque(fila, columna);
                                Console.Clear();
                                tableroJugador.tableroJugador();
                                if (ataque)
                                {
                                    Log.Info(lang.GetString("log11"));
                                    barcosJugador--;
                                    Console.WriteLine(lang.GetString("game6"));
                                    Thread.Sleep(2500);
                                    if (barcosJugador == 0)
                                    {
                                        victoria = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    Log.Info(lang.GetString("log12"));
                                    Console.WriteLine(lang.GetString("game7"));
                                    Thread.Sleep(2500);
                                }
                            }
                        }
                    }
                    if (jugador == 0)
                    {
                        Log.Info(lang.GetString("log13"));
                        Console.Clear(); // Menú de la pantalla de victoria.
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(lang.GetString("win1"));
                        Console.WriteLine(lang.GetString("win2"));
                        Console.WriteLine(lang.GetString("win3"));
                        Console.ResetColor();
                        string menu = lang.GetString("menu1");
                        Console.WriteLine(string.Format(menu, turno));
                        Console.WriteLine(lang.GetString("menu2"));
                        Console.WriteLine(lang.GetString("title5"));
                        ConsoleKeyInfo teclaFinal = Console.ReadKey(true); // Se configura las teclas para controlar el menú.
                        if (teclaFinal.Key == ConsoleKey.Enter) { Log.Info(lang.GetString("log14")); continue; }
                        if (teclaFinal.Key == ConsoleKey.Escape) { salir = true;
                            Log.Info(lang.GetString("log2")); break; }
                    }
                    else
                    {
                        Log.Info(lang.GetString("log15"));
                        Console.Clear(); // Menú de la pantalla de derrota.
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(lang.GetString("lose1"));
                        Console.WriteLine(lang.GetString("lose2"));
                        Console.WriteLine(lang.GetString("lose3"));
                        Console.ResetColor();
                        string menu = lang.GetString("menu1");
                        Console.WriteLine(string.Format(menu, turno));
                        Console.WriteLine(lang.GetString("menu2"));
                        Console.WriteLine(lang.GetString("title5"));
                        ConsoleKeyInfo teclaFinal = Console.ReadKey(true); // Se configura las teclas para controlar el menú.
                        if (teclaFinal.Key == ConsoleKey.Enter) { Log.Info(lang.GetString("log14")); continue; }
                        if (teclaFinal.Key == ConsoleKey.Escape) { salir = true;
                            Log.Info(lang.GetString("log2")); break; }
                    }
                }
            }
        }
    }
}
