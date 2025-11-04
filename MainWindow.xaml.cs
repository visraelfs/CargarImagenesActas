using CargarImagenesActas.Clases;
using CargarImagenesActas.Model.ModelViews;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CargarImagenesActas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<ComboBoxOpciones> lOpiones = new List<ComboBoxOpciones>();
            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Fachada",
                valor = "fachada"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Causa",
                valor = "causa"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Lugar Antena Antes",
                valor = "lugarAntenaAntes"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Lugar Antena Despues",
                valor = "lugarAntenaDespues"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Linea Vista Detras Antena",
                valor = "lineaVistaDetrasAntena"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Detalle Base Antena",
                valor = "detalleBaseAntena"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Trayectoria Cableado Exterior",
                valor = "trayectoriaCableadoExterior"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Ruta Cableado Interior",
                valor = "rutaCableadoInterior"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Trayectoria Cableado Inerior",
                valor = "trayectoriaCableadoInerior"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Ruta Salida Cableado Interior",
                valor = "rutaSalidaCableadoInterior"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Ruta Tuberia",
                valor = "rutaTuberia"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Modem Antes Servicio",
                valor = "modemAntesServicio"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Modem Despues Servicio",
                valor = "modemDespuesServicio"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Detalle Leds",
                valor = "detalleLeds"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Detalle Posterior Modem",
                valor = "detallePosteriorModem"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Detalle BUCAntes",
                valor = "detalleBUCAntes"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Detalle BUC Despues",
                valor = "DetalleBUCDespues"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Detalle LNB Antes",
                valor = "detalleLNBAntes"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Detalle LNB Despues",
                valor = "detalleLNBDespues"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Detalle Conexion",
                valor = "detalleConexion"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Adecuaciones Especiales",
                valor = "adecuacionesEspeciales"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Evidencia Conexion Modem",
                valor = "evidenciaConexionModem"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Foto UPS",
                valor = "fotoUPS"
            });

            lOpiones.Add(new ComboBoxOpciones
            {
                texto = "Firma Telefono",
                valor = "firmaTelefono"
            });

            cbNombre.ItemsSource = lOpiones;
            cbNombre.DisplayMemberPath = "texto";
            cbNombre.SelectedValuePath = "valor";
        }

        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private void NumberOnly_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Permitir teclas como Tab, Backspace, Delete, etc.
            if (e.Key == Key.Space) e.Handled = true;
        }

        private void NumberOnly_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var text = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextNumeric(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private bool IsTextNumeric(string text)
        {
            return text.All(char.IsDigit);
        }

        private void SeleccionarImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
            {
                rutaImagen.Text = openFileDialog.FileName;                
            }

        }

        private void GuardarImagen_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(IdActa.Text) || cbNombre.SelectedIndex == -1 || string.IsNullOrWhiteSpace(rutaImagen.Text))
                {
                    MessageBox.Show("Todos los campos sob obligatorios, favor de validar", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                AgregarImagen agregarImagen = new AgregarImagen(chkSS.IsChecked.Value, int.Parse(IdActa.Text), cbNombre.SelectedValue?.ToString(), rutaImagen.Text);
                agregarImagen.guardarImagenActa();

                MessageBox.Show("Se cargo la imagen de forma correcta", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
