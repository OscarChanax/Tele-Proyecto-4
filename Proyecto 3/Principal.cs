using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Net.Http;
using System.Drawing.Imaging;

namespace Proyecto_3
{
    public partial class Principal : Form
    {

        private string esp8266IpAddress; // Mueve la variable al nivel de clase

        public Principal()
        {
            InitializeComponent();
            //Form
            this.Text = string.Empty;
           
            estadoPanel.BackColor = Color.Red;


        }

        private async Task ControlLED(bool encender)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = encender ? $"http://{esp8266IpAddress}/led/on" : $"http://{esp8266IpAddress}/led/off";
                await client.GetAsync(url);
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Restablece la dirección IP a un valor nulo o vacío para indicar que no hay conexión
            esp8266IpAddress = null;

            // Muestra un mensaje de desconexión exitosa
            estadoPanel.BackColor = Color.Red;
            MessageBox.Show("Desconexión exitosa.", "Desconexión", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void comboBoxPuerto_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void buttonConectar_Click(object sender, EventArgs e)
        {
            // Obtén la dirección IP ingresada por el usuario
            esp8266IpAddress = textBoxIP.Text;

            // Verifica si la dirección IP es válida (puedes agregar una validación adicional si lo deseas)
            if (IsValidIPAddress(esp8266IpAddress))
            {
                // Muestra un mensaje de conexión exitosa
                estadoPanel.BackColor = Color.Green;
                MessageBox.Show("Conectado a la dirección IP: " + esp8266IpAddress, "Conexión Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Muestra un mensaje de error si la dirección IP no es válida
                estadoPanel.BackColor = Color.Red;
                MessageBox.Show("Dirección IP no válida. Ingresa una dirección IP válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Función para verificar si una cadena es una dirección IP válida
        private bool IsValidIPAddress(string ipAddress)
        {
            System.Net.IPAddress address;
            return System.Net.IPAddress.TryParse(ipAddress, out address);
        }

        private void Principal_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            await ControlLED(true);
        }

      
        

        private async void button2_Click(object sender, EventArgs e)
        {
            //Apaga
            await ControlLED(false);
        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Cierra ventana
        }

        private async void buttonStop_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"http://{esp8266IpAddress}/stop";
                await client.GetAsync(url);
            }
        }
    }
}
