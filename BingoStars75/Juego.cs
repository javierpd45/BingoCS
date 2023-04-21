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
using System.Speech.Synthesis;
using System.Media;
using System.IO;

namespace BingoStars75
{
    public partial class Juego : Form
    {
        private int[,] matriz = new int[5,6];
        private List<int> numerosGenerados = new List<int>();
        private SpeechSynthesizer vozDiscurso = new SpeechSynthesizer();
        private SoundPlayer sonidoDeVictoria;

        public Juego(int[,] contenidoMatriz)
        {
            // Se trae la matriz generada en la pantalla de Inicio
            // Se utiliza el constructor para que se genere 1 sola vez
            InitializeComponent();
            for (int filas = 0; filas < 5; filas++)
            {
                for (int columnas = 1; columnas < 6; columnas++)
                {
                    matriz[filas, columnas] = contenidoMatriz[filas, columnas];
                }
            }

            // URL del sonido
            sonidoDeVictoria = new SoundPlayer();
            sonidoDeVictoria.SoundLocation = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Recursos\hesDoneIt.wav");
            //sonidoDeVictoria = new SoundPlayer(soundLocation: );


            // Generando la tabla que contendrá la matriz
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

            // Agregando valores a las filas y columnas
            for (int filas = 0; filas < 5; filas++)
            {
                for (int columnas = 1; columnas < 6; columnas++)
                {
                    dataGridView2[filas, columnas].Value = matriz[filas, columnas];
                }
            }
            dataGridView2[2, 3].Value = "Free";
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

            // Se comparan los numeros con lo de una lista que contiene los numeros repetidos
            do
            {
                aleatorio = numero.Next(1, 76);
                if (numerosGenerados.Count() > 73) numerosGenerados.Clear();
            } while (numerosGenerados.Contains(aleatorio));
            
            numerosGenerados.Add(aleatorio); // Los numeros se agregan a una lista para no repetirse

            // Añadiendo las letras a los numeros
            if (aleatorio > 0 && aleatorio < 16) letra = "B";
            if (aleatorio > 15 && aleatorio < 31) letra = "I";
            if (aleatorio > 30 && aleatorio < 46) letra = "N";
            if (aleatorio > 45 && aleatorio < 61) letra = "G";
            if (aleatorio > 60 && aleatorio < 76) letra = "O";

            // Se limpia la caja de texto luego de cada numero
            textBox1.Clear();
            textBox1.Text = letra + aleatorio.ToString();

            // Voz femenina
            vozDiscurso.SelectVoiceByHints(VoiceGender.Female);

            // Discurso de numeros
            vozDiscurso.Speak(textBox1.Text);
            
            // Cambia el numero en el carton por una --> X <-- si es el numero que sale
            for (int filas = 0; filas < 5; filas++) 
            {
                for (int columnas = 1;columnas < 6;columnas++) 
                {
                    if (matriz[filas, columnas] == aleatorio && matriz[filas, columnas] != matriz[2, 3])
                    {
                        dataGridView2[filas, columnas].Value = "--> X <--";
                    }    
                }
            }
        }

        private void btnBingo_Click(object sender, EventArgs e)
        {
            vozDiscurso.SelectVoiceByHints(VoiceGender.Male);
                        
            vozDiscurso.Speak("Bingo");
            label1.Text = "Ganaste el premio!";
            sonidoDeVictoria.Play();            
        }
    }
}
