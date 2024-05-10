using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
     public class Parte
    {
        public Dictionary<String, Cara> listaDeCaras { get; set; }
        public Punto centroDeMasa { get; set; }
        public Matrix4 matrizDeEscalacion { get; set; }
        public Matrix4 matrizDeTraslacion { get; set; }
        public Matrix4 matrizDeRotacionEjeX { get; set; }
        public Matrix4 matrizDeRotacionEjeY { get; set; }
        public Matrix4 matrizDeRotacionEjeZ { get; set; }

        public Matrix4 matrizGenerarlDelObjeto { get; set; }

        public Matrix4 matrizIdentidad;


        public Parte(Dictionary<String,Cara>caras,Punto centro) {
            this.listaDeCaras = caras;
            this.matrizIdentidad = Matrix4.Identity;
            this.matrizDeEscalacion = Matrix4.Identity;
            this.matrizDeRotacionEjeX = Matrix4.Identity;
            this.matrizDeRotacionEjeY = Matrix4.Identity;
            this.matrizDeRotacionEjeZ = Matrix4.Identity;
            this.matrizGenerarlDelObjeto = Matrix4.Identity;
            this.matrizDeTraslacion = Matrix4.CreateTranslation(centro.getX(), centro.getY(), centro.getZ());
            this.centroDeMasa= centro;
        }


        
        public Parte(Punto centro) {
            this.centroDeMasa = centro;
            this.listaDeCaras = new Dictionary<string, Cara>();
            this.matrizIdentidad = Matrix4.Identity;
            this.matrizDeEscalacion = Matrix4.Identity;
            this.matrizDeRotacionEjeX = Matrix4.Identity;
            this.matrizDeRotacionEjeY = Matrix4.Identity;
            this.matrizDeRotacionEjeZ = Matrix4.Identity;
            this.matrizGenerarlDelObjeto = Matrix4.Identity;
            this.matrizDeTraslacion = Matrix4.CreateTranslation(centro.getX(), centro.getY(), centro.getZ());

        }

        /* public Parte(Punto centro) {
             this.centroDeMasa = centro;
             this.listaDeCaras= new Dictionary<String, Cara>();

         }*/

        public Parte() {
            this.centroDeMasa = new Punto(0,0,0);
            this.listaDeCaras = new Dictionary<string, Cara>();
            this.matrizIdentidad = Matrix4.Identity;
            this.matrizDeEscalacion = Matrix4.Identity;
            this.matrizDeRotacionEjeX = Matrix4.Identity;
            this.matrizDeRotacionEjeY = Matrix4.Identity;
            this.matrizDeRotacionEjeZ = Matrix4.Identity;
            this.matrizGenerarlDelObjeto = Matrix4.Identity;
            this.matrizDeTraslacion = Matrix4.CreateTranslation(0,0,0);
        }

        public Punto getCentroDeMasa() {
            return this.centroDeMasa;
        }


        public Cara getCara(string nombre) {
            if(this.listaDeCaras.ContainsKey(nombre)){
                return this.listaDeCaras[nombre];
            }
        return null;
        }
        public void setCentroDeMasa(Punto centro) {
            this.centroDeMasa = centro;
            
        }

        public void agregarCara(String nombreCara,Cara cara) {
            this.listaDeCaras.Add(nombreCara,cara);
        }

        public void setIdentidad(Matrix4 ma)
        {
            this.matrizIdentidad = ma;
        }


        public Matrix4 getIdentdidad()
        {
            return this.matrizIdentidad;
        }

       

      




        public void trasladar(Punto direccionTraslacion)
        {
            this.matrizDeTraslacion = Matrix4.CreateTranslation(direccionTraslacion.getX(), direccionTraslacion.getY(), direccionTraslacion.getZ()) * this.matrizIdentidad;
        }


        public void rotar(float anguloRotacion, Punto ejeRotacion)
        {
            Matrix4 aux = Matrix4.CreateFromAxisAngle(new Vector3(ejeRotacion.getX(), ejeRotacion.getY(), ejeRotacion.getZ()), MathHelper.DegreesToRadians(anguloRotacion));
            switch (ejeRotacion)
            {
                case Punto p when p.getX() != 0:
                    this.matrizDeRotacionEjeX = Matrix4.CreateFromAxisAngle(new Vector3(ejeRotacion.getX(), ejeRotacion.getY(), ejeRotacion.getZ()), anguloRotacion);
                    break;
                case Punto p when p.getY() != 0:
                    this.matrizDeRotacionEjeY = Matrix4.CreateFromAxisAngle(new Vector3(ejeRotacion.getX(), ejeRotacion.getY(), ejeRotacion.getZ()), anguloRotacion);
                    break;
                case Punto p when p.getZ() != 0:
                    this.matrizDeRotacionEjeZ = Matrix4.CreateFromAxisAngle(new Vector3(ejeRotacion.getX(), ejeRotacion.getY(), ejeRotacion.getZ()), anguloRotacion);
                    break;

            }
        }

        public void escalar(Punto coordenadasAEscalar)
        {
            this.matrizDeEscalacion = Matrix4.CreateScale(new Vector3(coordenadasAEscalar.getX(), coordenadasAEscalar.getY(), coordenadasAEscalar.getZ())) * this.matrizIdentidad;

        }

        public void dibujar(Matrix4 matrizPadre) {
           this.matrizGenerarlDelObjeto = matrizDeRotacionEjeX * matrizDeRotacionEjeY *matrizDeRotacionEjeZ * matrizDeEscalacion * matrizDeTraslacion;
            foreach (Cara cara in listaDeCaras.Values) {
                cara.dibujar(this.matrizGenerarlDelObjeto*matrizPadre);
                //Console.WriteLine("Estas es la matriz Final :: " + this.matrizGenerarlDelObjeto * matrizPadre);
            }
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine(this.matrizIdentidad);
            Console.WriteLine(this.matrizDeRotacionEjeX);
            Console.WriteLine(this.matrizDeRotacionEjeY);
            Console.WriteLine(this.matrizDeRotacionEjeZ);
            Console.WriteLine(this.matrizDeTraslacion);
            Console.WriteLine(this.matrizDeEscalacion);
            Console.WriteLine(this.matrizGenerarlDelObjeto);
            Console.WriteLine(this.matrizGenerarlDelObjeto * matrizPadre);
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("---------------------------------------------");
        }

        public Punto sumaCentros(Punto punto1, Punto punto2)
        {
            return new Punto(punto1.getX() + punto2.getX(), punto1.getY() + punto2.getY(), punto1.getZ() + punto2.getZ());
        }

    }
}
