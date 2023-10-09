﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundirFlota
{
    internal class Barco
    {
        /*
         * Programa que genera los barcos y los controla.
         */
        private int fila;
        private int columna;
        public Barco(int fila, int columna)
        {
            this.fila = fila;
            this.columna = columna;
        }
        public int getFila() { return fila; }
        public int getColumna() { return columna; }
        internal static Barco GenerarBarco() // Genera un barco en una posición aleatoria.
        {
            Random r = new Random();
            int fila = r.Next(0, 8);
            int columna = r.Next(0, 8);
            return new Barco(fila, columna);
        }
        public override bool Equals(object obj) // Controla si hay un barco ya ocupando una casilla o no.
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Barco other = (Barco)obj;
            return this.fila == other.fila && this.columna == other.columna;
        }
    }
}
