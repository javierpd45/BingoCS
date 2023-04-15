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
        private List<int> numerosGenerados = new List<int>();

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
                DataGridViewColumn columna2 = new DataGridViewColumn(new DataGridViewTextBoxCell());
                columna2.Width = 75;
                dataGridView2.Columns.Add(columna2);
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

        private void btnSiguienteNumero_Click(object sender, EventArgs e)
        {            
            Random  numero = new Random();
            string letra = "";
            int aleatorio;            

            do
            {
                aleatorio = numero.Next(1, 76);
                if (numerosGenerados.Count() > 73) numerosGenerados.Clear();
            } while (numerosGenerados.Contains(aleatorio));
            
            numerosGenerados.Add(aleatorio);

            if (aleatorio > 0 && aleatorio < 16) letra = "B";
            if (aleatorio > 15 && aleatorio < 31) letra = "I";
            if (aleatorio > 30 && aleatorio < 46) letra = "N";
            if (aleatorio > 45 && aleatorio < 61) letra = "G";
            if (aleatorio > 60 && aleatorio < 76) letra = "O";

            textBox1.Clear();
            textBox1.Text = letra + aleatorio.ToString();
            
            for (int filas = 0; filas < 5; filas++) 
            {
                for (int columnas = 1;columnas < 6;columnas++) 
                {
                    if (matriz[filas, columnas] == aleatorio)
                    {
                        dataGridView2[filas, columnas].Value = "--> X <--";
                    }    
                }
            }
        }
    }
}
