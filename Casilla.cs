using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundirFlota
{
    internal class Casilla
    {
        /*
         * Programa para generar y controlar las casillas del tablero de juego.
         */
        private int fila;
        private int columna;
        private string estado;
        private string agua = "-";
        private string barco = "B";
        private string atacado = "*";
        private string tocado = "X";
        public Casilla(int fila, int columna)
        {
            this.fila = fila;
            this.columna = columna;
            estado = agua;
        }
        public int getFila() { return fila; }
        public void setFila(int fila) { this.fila = fila; }
        public int getColumna() { return columna; }
        public void setColumna(int columna) { this.columna = columna; }
        public string getEstado() { return estado; }
        public void setEstado(string estado) { this.estado = estado; }
        public string getAgua() { return agua; }
        public string getBarco() { return barco; }
        public string getAtacado() { return atacado; }
        public string getTocado() { return tocado; }
    }
}
