using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BingoStars75
{
    public partial class Juego : Form
    {
        private int[,] matriz = new int[5,6];

        public Juego(int[,] contenidoMatriz)
        {
            InitializeComponent();
            for (int filas = 0; filas < 5; filas++)
            {
                for (int columnas = 1; columnas < 6; columnas++)
                {
                    matriz[filas, columnas] = contenidoMatriz[filas, columnas];
                }
            }

            dataGridView2.Columns.Clear();            

            for (int i = 0; i < 5; i++)
            {
                DataGridViewColumn columna1 = new DataGridViewColumn(new DataGridViewTextBoxCell());
                //columna1.Name = (i + 1).ToString();
                columna1.Width = 75;
                dataGridView2.Columns.Add(columna1);
            }
            dataGridView2.Rows.Add(5);

            dataGridView2[0, 0].Value = "B";
            dataGridView2[1, 0].Value = "I";
            dataGridView2[2, 0].Value = "N";
            dataGridView2[3, 0].Value = "G";
            dataGridView2[4, 0].Value = "O";

            for (int filas = 0; filas < 5; filas++)
            {
                for (int columnas = 1; columnas < 6; columnas++)
                {
                    dataGridView2[filas, columnas].Value = matriz[filas, columnas];
                }
            }
        }

        

        private void Juego_Load(object sender, EventArgs e)
        {

        }

        private void btnATRAS_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
