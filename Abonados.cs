using System;

namespace Abonados_del_betis;

public class Abonados
{
    private int NumeroSocio;
        private string NombreSocio;
        private string ApellidoSocio;
        private int EdadSocio;
        private char GradaSocio;
        private float CostoSocio;
        private bool PagoSocio;
        private string RutaImagenSocio;
        private int Tam;
        private byte[] ImagenSocio;

        public int tam
        {
            get { return Tam; }
        }

        public byte[] imagenSocio
        {
            get
            {
                return ImagenSocio;
            }
            set
            {
                ImagenSocio = value;
                tamaño();
            }
        }

        public int numeroSocio
        {
            get
            {
                return NumeroSocio;
            }

            set
            {
                NumeroSocio = value;
            }
        }

        public string nombreSocio
        {
            get
            {
                return NombreSocio;
            }
            set
            {
                NombreSocio = value;
            }
        }

        public string apellidoSocio
        {
            get
            {
                return ApellidoSocio;
            }
            set
            {
                ApellidoSocio = value;
            }
        }

        public int edadSocio
        {
            get
            {
                return EdadSocio;
            }
            set
            {
                EdadSocio = value;
            }
        }

        public char gradaSocio
        {
            get
            {
                return GradaSocio;
            }
            set
            {
                GradaSocio = value;
            }
        }

        public float costoSocio
        {
            get
            {
                return CostoSocio;
            }
            set
            {
                CostoSocio = value;
            }
        }

        public bool pagoSocio
        {
            get
            {
                return PagoSocio;
            }
            set
            {
                PagoSocio = value;
            }
        }

        public string rutaImagenSocio
        {
             get
             {
                 return RutaImagenSocio;
             }
             set
             {
                     RutaImagenSocio = value;
             }
        }
        public void tamaño()
        {
            Tam = imagenSocio.Length;
        }
}