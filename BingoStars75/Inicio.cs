using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BingoStars75
{
    public partial class Inicio : Form
    {
        /// <summary>
        /// Matriz para guardar los números aleatorios del cartón
        /// </summary>
        private int[,] matriz = new int[5,6];
        private bool botonHizoClick = false;
        
        public Inicio()
        {
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Manegar el evento del click del boton para salir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSALIR_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Manegar el evento del click del boton para generar el carton, los numeros del carton son aleatorios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerarCarton_Click(object sender, EventArgs e)
        {
            // Se limpia la matriz cada vez que se genera una nueva
            dataGridView1.Columns.Clear();

            // Creacion de la tabla que contendra el carton/matriz
            for (int i = 0; i < 5; i++)
            {
                DataGridViewColumn columna1 = new DataGridViewColumn(new DataGridViewTextBoxCell());
                //columna1.Name = (i + 1).ToString();
                columna1.Width = 75;
                dataGridView1.Columns.Add(columna1);
            }
            dataGridView1.Rows.Add(5);

            // Añadiendo la palabra BINGO en la parte superior del carton
            dataGridView1[0, 0].Value = "B";
            dataGridView1[1, 0].Value = "I";
            dataGridView1[2, 0].Value = "N";
            dataGridView1[3, 0].Value = "G";
            dataGridView1[4, 0].Value = "O";

            Random random = new Random();
            int[,] matrizRandom = new int[5,6];
            int numeroRandom;
            List<int> numerosGenerados = new List<int>();

            // Generando la matriz/carton aleatoria
            for (int filas = 0; filas < 5; filas++)
            {
                for (int columnas = 1; columnas < 6; columnas++)
                {
                    switch (filas)
                    {
                        // Numeros en la columna 0
                        // Columna que tiene la B
                        // Los numeros están entre el 1 y 15
                        case 0:

                            do
                            {
                                // El metodo Next toma un valor menos n - 1
                                // En su valor superior del intervalo
                                numeroRandom = random.Next(1, 16);
                            } while (numerosGenerados.Contains(numeroRandom));

                            matrizRandom[filas, columnas] = numeroRandom;
                            numerosGenerados.Add(numeroRandom);
                            
                            break;

                        case 1:

                            do
                            {
                                numeroRandom = random.Next(16, 31);
                            } while (numerosGenerados.Contains(numeroRandom));

                            matrizRandom[filas, columnas] = numeroRandom;
                            numerosGenerados.Add(numeroRandom);

                            break;

                        case 2:

                            do
                            {
                                numeroRandom = random.Next(31, 46);
                            } while (numerosGenerados.Contains(numeroRandom));

                            matrizRandom[filas, columnas] = numeroRandom;
                            numerosGenerados.Add(numeroRandom);

                            break;

                        case 3:

                            do
                            {
                                numeroRandom = random.Next(46, 61);
                            } while (numerosGenerados.Contains(numeroRandom));

                            matrizRandom[filas, columnas] = numeroRandom;
                            numerosGenerados.Add(numeroRandom);

                            break;

                        case 4:

                            do
                            {
                                numeroRandom = random.Next(61, 76);
                            } while (numerosGenerados.Contains(numeroRandom));

                            matrizRandom[filas, columnas] = numeroRandom;
                            numerosGenerados.Add(numeroRandom);

                            break;
                    }
                }
            }

            // Colocando los valores en la tabla
            // Los valores tambien se guardan en una matriz que es utilizada para
            // pasar los valores la siguiente pantalla
            for (int filas = 0; filas < 5; filas++)
            {
                for(int columnas = 1; columnas < 6; columnas++)
                {
                    dataGridView1[filas, columnas].Value = matrizRandom[filas, columnas];
                    matriz[filas, columnas] = matrizRandom[filas, columnas];
                }
            }

            dataGridView1[2, 3].Value = "Free"; // Valor por defecto en la casilla central de un carton
            botonHizoClick = true;
        }

        /// <summary>
        /// Manegar evento del boton para continuar a la siguiente pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (botonHizoClick)
            {
                using (Juego pantallaJuego = new Juego(matriz))
                    pantallaJuego.ShowDialog();
                label1.Text = "                     ¡A jugar!";
            }
            else
            {
                label1.Text = "Debe generar un cartón para jugar";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
