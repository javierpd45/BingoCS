using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Media;
using System.IO;

namespace BingoStars75
{
    public partial class Juego : Form
    {
        /// <summary>Matriz para guardar los números aleatorios del cartón </summary>
        private int[,] matriz = new int[5,6];
        /// <summary>Lista de los numeros aleatorios ya generados</summary>
        private List<int> numerosGenerados = new List<int>();
        /// <summary>Sintetizador de la voz que dicta los numeros</summary>
        private SpeechSynthesizer vozDiscurso = new SpeechSynthesizer();
        /// <summary>Reproductor de sonido</summary>
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

        /// <summary>
        /// Manegar el evento del click del boton para salir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnATRAS_Click(object sender, EventArgs e)
        {
            ReproducirSonidoClick();
            this.Close();
        }

        /// <summary>
        /// Manegar el evento del click del boton para presentar el siguiente numero en el carton del Bingo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSiguienteNumero_Click(object sender, EventArgs e)
        {            
            ReproducirSonidoClick();

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
                    // Verificar si se cumplen las condiciones de ganar
            bool bingo = false;

            // Verificar filas
            for (int fila = 0; fila < 5; fila++)
            {
                if (fila == 2) // Excluir la fila 3
                {
                    // Verificar solo las celdas en las columnas 1, 2, 4 y 5
                    if (dataGridView2[fila, 1].Value.ToString() == "--> X <--" &&
                        dataGridView2[fila, 2].Value.ToString() == "--> X <--" &&
                        dataGridView2[fila, 4].Value.ToString() == "--> X <--" &&
                        dataGridView2[fila, 5].Value.ToString() == "--> X <--")
                    {
                        bingo = true;
                        break;
                    }
                }
                else
                {
                    // Verificar todas las celdas en las columnas 1 a 5
                    if (dataGridView2[fila, 1].Value.ToString() == "--> X <--" &&
                        dataGridView2[fila, 2].Value.ToString() == "--> X <--" &&
                        dataGridView2[fila, 3].Value.ToString() == "--> X <--" &&
                        dataGridView2[fila, 4].Value.ToString() == "--> X <--" &&
                        dataGridView2[fila, 5].Value.ToString() == "--> X <--")
                    {
                        bingo = true;
                        break;
                    }
                }
            }


            // Verificar columnas
            if (!bingo)
            {
                for (int columna = 0; columna < 5; columna++)
                {
                    // Comenzar desde la segunda fila (índice 1) en lugar de la primera (índice 0)
                    for (int fila = 1; fila < 5; fila++)
                    {
                        // Saltar la fila con el índice 3 en la columna con el índice 2
                        if (columna == 2 && fila == 3)
                        {
                            continue;
                        }

                        if (dataGridView2[columna, fila].Value.ToString() != "--> X <--")
                        {
                            // Si una celda no contiene la combinación específica, salir del bucle
                            break;
                        }

                        // Si se llega a la última fila y todas las celdas contienen la combinación, establecer "bingo" como verdadera
                        if (fila == 4)
                        {
                            bingo = true;
                        }
                    }
                }
            }   
                

            // Verificar diagonal principal
            if (!bingo)
            {
                if (dataGridView2[0, 1].Value.ToString() == "--> X <--" &&
                    dataGridView2[1, 2].Value.ToString() == "--> X <--" &&
                    dataGridView2[3, 4].Value.ToString() == "--> X <--" &&
                    dataGridView2[4, 5].Value.ToString() == "--> X <--")
                {
                    bingo = true;
                }
            }

            // Verificar diagonal secundaria
            if (!bingo)
            {
                if (dataGridView2[0, 5].Value.ToString() == "--> X <--" &&
                    dataGridView2[1, 4].Value.ToString() == "--> X <--" &&
                    dataGridView2[3, 2].Value.ToString() == "--> X <--" &&
                    dataGridView2[4, 1].Value.ToString() == "--> X <--")
                {
                    bingo = true;
                }
            }

            if (bingo)
            {
                vozDiscurso.SelectVoiceByHints(VoiceGender.Male);
                vozDiscurso.Speak("Bingo");
                label1.Text = "¡Ganaste el premio!";
                sonidoDeVictoria.Play();
            }
            else
            {
                MessageBox.Show("Aún no has completado el Bingo. Sigue jugando.", "Bingo Incompleto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Reproduce un sonido de click, por lo general es usado en botones
        /// </summary>
        private void ReproducirSonidoClick()
        {
            SystemSounds.Beep.Play();
        }
    }   
}
