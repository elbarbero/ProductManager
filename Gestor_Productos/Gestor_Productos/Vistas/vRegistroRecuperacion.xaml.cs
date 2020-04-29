using DevExpress.Xpf.Core;
using DevExpress.Xpo;
using Gestor_Productos.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Gestor_Productos.Vistas
{
    /// <summary>
    /// Lógica de interacción para vRegistroRecuperacion.xaml
    /// </summary>
    public partial class vRegistroRecuperacion : DevExpress.Xpf.Core.ThemedWindow
    {
        public vRegistroRecuperacion(string pulsado)
        {
            InitializeComponent();
            ViewModelvRegRec viewModel = new ViewModelvRegRec(this, pulsado);
        }

        public class ViewModelvRegRec : INotifyPropertyChanged
        {
            #region variables globales
            private vRegistroRecuperacion ventana;
            public UnitOfWork uow;
            private string _nomVentana;
            public string nomVentana { get { return _nomVentana; } set { _nomVentana = value; OnPropertyChanged("nomVentana"); } }

            public event PropertyChangedEventHandler PropertyChanged;
            #endregion

            #region Constructor
            protected void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
            //Constructor para visualizar albaranes
            public ViewModelvRegRec(vRegistroRecuperacion ventana, string pulsado)
            {
                this.ventana = ventana;
                uow = ConexionBBDD.getNewUnitOfWork();
                if (pulsado == "registro")
                {
                    nomVentana = "Resgristo nuevo usuario";
                }
                else
                {
                    nomVentana = "Recuperación de contraseña";
                }
            }
            #endregion

            #region Eventos

            public void btnAceptarCancel_Click(object sender, RoutedEventArgs e)
            {
                if((sender as SimpleButton).Name == "btnAceptar")
                {
                    //hacer el registro
                }
                else
                {
                    //boton cancelar
                }
            }

            #endregion
        }
    }
}
