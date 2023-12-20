using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;

namespace Abonados_del_betis;

public partial class MainWindow : Window
{
    List<Abonados> Socio; //declaro una lista que guardara los registros.
    int posicion = 0; //declaro un entero para recorrer la lista.
    public MainWindow()
    {
        InitializeComponent();
        Socio = new List<Abonados>();
        leerFichero(); //llamo al metodo para leer.
        comprobarbotones(); //llamo a un metodo para activar/desactivar los botones anterior y siguiente
        this.Closed += (sender, args) => gardarEnFichero(); //al cerrar la app se llama el metodo para gurdar los datos
    }
    
    //button Anterior
    private void Anterior(object? sender, RoutedEventArgs e)
    {
        if (posicion > 0) //siempre que la posicion sea mayor que 0 actualiza los valores a mostrar.
        {
            txtNum.Text = Socio[posicion - 1].numeroSocio.ToString();
            txtNombre.Text = Socio[posicion - 1].nombreSocio;
            txtApellido.Text = Socio[posicion - 1].apellidoSocio;
            txtEdad.Text = Socio[posicion - 1].edadSocio.ToString();
            txtGrada.Text = Socio[posicion - 1].gradaSocio.ToString();
            txtPago.Text = Socio[posicion - 1].costoSocio.ToString();
            ckRealizoElPago.IsChecked = Socio[posicion - 1].pagoSocio;
            txtImagen.Text = Socio[posicion - 1].rutaImagenSocio.ToString();
            posicion--;
            cargarImagen(); //llamo a un metodo para cargar la imagen de este registro.
            comprobarbotones(); //llamo a un metodo para activar/desactivar los botones anterior y siguiente.
        }
        lblNumeroTotalSocio.Text = Socio.Count.ToString(); //Actualizo el contador al numero de registros que haya.
    }
    //button Crear
    private void crear(object? sender, RoutedEventArgs e)
    {
        TxtBlancos(); //llamo al metodo para dejar todos los elemtos vacios.
        lblNumeroTotalSocio.Text = Socio.Count.ToString(); //Actualizo el contador al numero de registros que haya.
    }
    //button Eliminar
    private void Eliminar(object? sender, RoutedEventArgs e)
    {
        Socio.Remove(Socio[posicion]); //Elimino el que se esta mostrando actualmente.
        lblNumeroTotalSocio.Text = Socio.Count.ToString(); //Actualizo el contador al numero de registros que haya.
        if (Socio.Count > 0 && posicion > 0) //Si mi lista tiene mas de 0 registros y no esta en la primera posicion.
        {
            posicion--; //Muevo la posicion uno atras.
            MostrarActual(); //LLamo al metodo para actualizar los valores al actual.
            butEliminar.IsEnabled = true; //Activo el boton.
        }
        else
        {
            if (posicion == 0 && Socio.Count > 0) //sai esta en la primear posicion y no es el unico en mi lista.
            {
                MostrarActual(); //Muestro el actual.
                butEliminar.IsEnabled = true; //Activo el boton.
            }
            else //Por el contrario si es el ultimo registro exixtente.
            {
                butModificar.IsEnabled = false; //Desactivo el boton modificar (ya que no hay mas registros).
                butEliminar.IsEnabled = false; //Desactivo el boton eliminar.
                TxtBlancos(); //llamo al metodo para dejar todos los elemtos vacios.
            }
        }

        comprobarbotones(); //llamo a un metodo para activar/desactivar los botones anterior y siguiente.
    }
    //button Siguiente

    private void Siguiente(object? sender, RoutedEventArgs e)
    {
        MostrarSiguienteElemento(); //Llamo al metodo para mostrar el siguiente elemento de mi lista.
        lblNumeroTotalSocio.Text = Socio.Count.ToString(); //Actualizo el contador al numero de registros que haya.
    }
    
    
    public void MostrarSiguienteElemento()
    {
        if (posicion < Socio.Count - 1) //siempre que la posicion sea menor al tama�o actualiza los valores a mostrar.
        {
            txtNum.Text = Socio[posicion + 1].numeroSocio.ToString();
            txtNombre.Text = Socio[posicion + 1].nombreSocio;
            txtApellido.Text = Socio[posicion + 1].apellidoSocio;
            txtEdad.Text = Socio[posicion + 1].edadSocio.ToString();
            txtGrada.Text = Socio[posicion + 1].gradaSocio.ToString();
            txtPago.Text = Socio[posicion + 1].costoSocio.ToString();
            ckRealizoElPago.IsChecked = Socio[posicion + 1].pagoSocio;
            txtImagen.Text = Socio[posicion + 1].rutaImagenSocio.ToString();
            posicion++;
            cargarImagen(); //llamo a un metodo para cargar la imagen de este registro.
            comprobarbotones(); //llamo a un metodo para activar/desactivar los botones anterior y siguiente.
        }
    }
    
    public void TxtBlancos() //actualizo todos los objetos del mi formilario para pornerlo en blanco.
    {
        txtNum.Text = "";
        txtNombre.Text = "";
        txtApellido.Text = "";
        txtEdad.Text = "";
        txtGrada.Text = "";
        txtPago.Text = "";
        txtImagen.Text = "";
        imagen.Source = null; //lo mismo hay que modificar
        ckRealizoElPago.IsChecked = false;
    }
    
    public void MostrarActual() //muestro el elemento en el que se encuentre mi Lista.
    {
        txtNum.Text = Socio[posicion].numeroSocio.ToString();
        txtNombre.Text = Socio[posicion].nombreSocio;
        txtApellido.Text = Socio[posicion].apellidoSocio;
        txtEdad.Text = Socio[posicion].edadSocio.ToString();
        txtGrada.Text = Socio[posicion].gradaSocio.ToString();
        txtPago.Text = Socio[posicion].costoSocio.ToString();
        ckRealizoElPago.IsChecked = Socio[posicion].pagoSocio;
        txtImagen.Text = Socio[posicion].rutaImagenSocio.ToString();
        cargarImagen(); //llamo a un metodo para cargar la imagen de este registro.
        comprobarbotones(); //llamo a un metodo para activar/desactivar los botones anterior y siguiente.
    }

    private void ButModificar_OnClick(object? sender, RoutedEventArgs e)
    {
        Socio[posicion].numeroSocio = int.Parse(txtNum.Text);
        Socio[posicion].nombreSocio = txtNombre.Text;
        Socio[posicion].apellidoSocio = txtApellido.Text;
        Socio[posicion].edadSocio = int.Parse(txtEdad.Text);
        Socio[posicion].gradaSocio = char.Parse(txtGrada.Text);
        Socio[posicion].costoSocio = float.Parse(txtPago.Text);
        Socio[posicion].pagoSocio = (bool)ckRealizoElPago.IsChecked;
        Socio[posicion].rutaImagenSocio = txtImagen.Text;
        //Socio[posicion].imagenSocio = imageToByteArray(txtImagen.Text);
        Bitmap foto = new Bitmap(txtImagen.Text);
        Socio[posicion].imagenSocio = imageToByteArray(foto);
        cargarImagen(); //llamo a un metodo para cargar la imagen de este registro.
        MostrarActual(); //llamo a un metodo para activar/desactivar los botones anterior y siguiente.
    }

    private void ButConfirmar_OnClick(object? sender, RoutedEventArgs e)
    {
        butModificar.IsEnabled = true; //Activo el boton de modificar (ya que a�ado un nuevo reguistro)
        butEliminar.IsEnabled = true;  //Activo el boton de eliminar (ya que a�ado un nuevo reguistro)
        Abonados abonados = new Abonados(); //creo y le doy los valores necesarios que se leen de los TextBox
        abonados.numeroSocio = int.Parse(txtNum.Text);
        abonados.nombreSocio = txtNombre.Text;
        abonados.apellidoSocio = txtApellido.Text;
        abonados.edadSocio = int.Parse(txtEdad.Text);
        abonados.gradaSocio = char.Parse(txtGrada.Text);
        abonados.costoSocio = float.Parse(txtPago.Text);
        abonados.pagoSocio = (bool) ckRealizoElPago.IsChecked;
        abonados.rutaImagenSocio = txtImagen.Text;
        Bitmap foto = new Bitmap(txtImagen.Text);
        abonados.imagenSocio = imageToByteArray(foto);

        Socio.Add(abonados); //A�ado mi nuevo registro.
        MostrarActual(); //Muestro el actual.
        lblNumeroTotalSocio.Text = Socio.Count.ToString(); //Actualizo el contador al numero de registros que haya.
    }

    private void ButCancelar_OnClick(object? sender, RoutedEventArgs e)
    {
        MostrarActual(); //Muestro el actual.
        lblNumeroTotalSocio.Text = Socio.Count.ToString();//Actualizo el contador al numero de registros que haya.
    }

    private void ButCargar_OnClick(object? sender, RoutedEventArgs e)
    {
        leerFichero(); //llamo al metodo para leer el fichero.
    }
    
    public byte[] imageToByteArray(Bitmap imagen) //Se convierte una imagen a un Array de Byte.
    {
        using (MemoryStream stream = new MemoryStream())
        {
            imagen.Save(stream);
            //Socio[posicion].imagenSocio = stream.ToArray();
            return stream.ToArray();
        }
    }
    
    public Bitmap byteArrayToImage() //Se convierte una Array de Byte a una imagen.
    {
        using (MemoryStream stream = new MemoryStream(Socio[posicion].imagenSocio))
        {
            return new Bitmap(stream);
        }

    }
    
    public void cargarImagen()
    {
        try
        {
            imagen.Source = (byteArrayToImage()); //Convierto el Array de Byte y se muestra.
        }
        catch (Exception ex) { }
    }
    
    public void gardarEnFichero()
    {
        BinaryWriter fichero; //Se crea un fichero para escribir.
        try
        {
            fichero = new BinaryWriter(File.Open("databank.data", FileMode.Create)); //Abrimos el fichero si exite, sino lo crea. Si ya existe lo sobre escribe.
            fichero.Write(Socio.Count); //Guardo el numero de resgitros de mi lista.

            for (int i = 0; i < Socio.Count; i++) //Escribo en el fichero
            {
                fichero.Write(Socio[i].numeroSocio);
                fichero.Write(Socio[i].nombreSocio);
                fichero.Write(Socio[i].apellidoSocio);
                fichero.Write(Socio[i].edadSocio);
                fichero.Write(Socio[i].gradaSocio);
                fichero.Write(Socio[i].costoSocio);
                fichero.Write(Socio[i].pagoSocio);
                fichero.Write(Socio[i].rutaImagenSocio);
                fichero.Write(Socio[i].tam); //guarda el tama�o de byte que tiene la imagen.
                fichero.Write(Socio[i].imagenSocio);
            }
           // MessageBox.Show("Se han guardado los registros correctamente");
            fichero.Close(); //Cerramos el fichero.
        }
        catch (Exception ex)
        {
           // MessageBox.Show("No se puedo guardar"); //Un mensaje en caso de error.
        }
    }
    
    public void leerFichero()
        {
            int NumeroSocioAux; //Declaro una variables auxiliares para leer el fichero.
            String NombreSocioAux;
            String ApellidoSocioAux;
            int EdadSocioAux;
            char GradaSocioAux;
            float CostoSocioAux;
            Boolean PagoSocioAux;
            String RutaImagenSocioAux;
            int TamAux;
            byte[] ImagenSocioAux;

            try
            {
                BinaryReader Fichero2; //Se crea un fichero para leer.
                Fichero2 = new BinaryReader(File.OpenRead("databank.data")); //Se abre el fichero para leer su contenido.
                int count = Fichero2.ReadInt32(); //leeo el numero total de registros guardados.
                lblNumeroTotalSocio.Text = count.ToString(); //Actualizo el contador al numero de registros que haya.
                for (int i = 0; i < count; i++) //Leemos los valores del fichero. Una vez termina creamos un abonado con las variables auxiliares.
                {
                    NumeroSocioAux = Fichero2.ReadInt32();
                    NombreSocioAux = Fichero2.ReadString();
                    ApellidoSocioAux = Fichero2.ReadString();
                    EdadSocioAux = Fichero2.ReadInt32();
                    GradaSocioAux = Fichero2.ReadChar();
                    CostoSocioAux = Fichero2.ReadSingle();
                    PagoSocioAux = Fichero2.ReadBoolean();
                    RutaImagenSocioAux = Fichero2.ReadString();
                    TamAux = Fichero2.ReadInt32();
                    ImagenSocioAux = Fichero2.ReadBytes(TamAux); //Leemos los bytes que me indica el tama�o.

                    Abonados abonados = new Abonados();
                    abonados.numeroSocio = NumeroSocioAux;
                    abonados.nombreSocio = NombreSocioAux;
                    abonados.apellidoSocio = ApellidoSocioAux;
                    abonados.edadSocio = EdadSocioAux;
                    abonados.gradaSocio = GradaSocioAux;
                    abonados.costoSocio = CostoSocioAux;
                    abonados.pagoSocio = PagoSocioAux;
                    abonados.rutaImagenSocio = RutaImagenSocioAux;
                    abonados.imagenSocio = ImagenSocioAux;

                    Socio.Add(abonados); //A�ado mi nuevo registro.
                    MostrarActual();  //Muestro el actual.
                }
               //MessageBox.Show("Se han a�adido los registros correctamente"); //Un mensaje para indicar que todo salio bien
                Fichero2.Close(); //Cierro el Fichero.
            }
            catch (Exception ex)
            {
                //MessageBox.Show("No hay valores que leer en el fichero \no no se pudo abrir el fichero"); //Un mensaje en caso de error.
            }
        }

    private void ButGuardar_OnClick(object? sender, RoutedEventArgs e)
    {
        gardarEnFichero(); //llamo al metodo para guardar el fichero.
    }
    
    private void comprobarbotones()
    {
        if (posicion == 0) //Si es la primera posicion desactivo el boton atras.
        {
            butAnterior.IsEnabled = false;
        }
        else //Si no se activa.
        {
            butAnterior.IsEnabled = true;
        }
        if (posicion + 1 == Socio.Count) //Si la posicion +1 es igual al total de registros desactivo el boton siguiente.
        {
            butSiguiente.IsEnabled = false;
        }
        else //Si no se activa el boton
        {
            butSiguiente.IsEnabled = true;
        }
        if (Socio.Count == 0) //si solo hay un reguistro o ninguno, ambos botones se desactivan.
        {
            butAnterior.IsEnabled = false;
            butSiguiente.IsEnabled = false;
        }
    }
}