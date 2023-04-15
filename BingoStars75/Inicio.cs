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

        public Inicio()
        {
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {

        }

        private void btnSALIR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerarCarton_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();

            for (int i = 0; i < 5; i++)
            {
                DataGridViewColumn columna1 = new DataGridViewColumn(new DataGridViewTextBoxCell());
                columna1.Name = (i + 1).ToString();
                columna1.Width = 75;
                dataGridView1.Columns.Add(columna1);
            }
            dataGridView1.Rows.Add(5);

            dataGridView1[0, 0].Value = "B";
            dataGridView1[1, 0].Value = "I";
            dataGridView1[2, 0].Value = "N";
            dataGridView1[3, 0].Value = "G";
            dataGridView1[4, 0].Value = "O";

            Random random = new Random();
            int[,] matrizRandom = new int[5,6];
            int numeroRandom;
            List<int> numerosGenerados = new List<int>();

            for (int filas = 0; filas < 5; filas++)
            {
                for (int columnas = 1; columnas < 6; columnas++)
                {
                    switch (filas)
                    {
                        case 0:

                            do
                            {
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

            for (int filas = 0; filas < 5; filas++)
            {
                for(int columnas = 1; columnas < 6; columnas++)
                {
                    dataGridView1[filas, columnas].Value = matrizRandom[filas, columnas];
                }
            }
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {

        }        
    }
}
