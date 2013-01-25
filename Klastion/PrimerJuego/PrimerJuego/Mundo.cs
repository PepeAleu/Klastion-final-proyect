using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrimerJuego
{
    class Mundo
    {
        float gravedad;
        public Mundo(float gravedad)
        {
            this.gravedad = gravedad;
        }

        public float Gravedad
        {
            get
            {
                return gravedad;
            }
            set
            {
                gravedad = value;
            }
        }
    }
}
