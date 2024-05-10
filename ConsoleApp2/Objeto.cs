using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Objeto
    {

        public Dictionary<String, Parte> listaDePartes { get; set; }
        public Punto centroDeMasa{ get; set; }

        
       
        public Matrix4 matrizDeEscalacion { get; set; }
        public Matrix4 matrizDeTraslacion { get; set; }
        public Matrix4 matrizDeRotacionEjeX { get; set; }
        public Matrix4 matrizDeRotacionEjeY { get; set; }
        public Matrix4 matrizDeRotacionEjeZ { get; set; }
       
        public Matrix4 matrizGenerarlDelObjeto { get; set; }

        public Matrix4 matrizIdentidad;
        public Objeto(Punto centro)
        {

            this.centroDeMasa = centro;
            this.listaDePartes=new Dictionary<String, Parte>();
            this.matrizIdentidad = Matrix4.Identity;
            this.matrizDeEscalacion = Matrix4.Identity;
            this.matrizDeRotacionEjeX = Matrix4.Identity;
            this.matrizDeRotacionEjeY = Matrix4.Identity;
            this.matrizDeRotacionEjeZ = Matrix4.Identity;
            this.matrizGenerarlDelObjeto = Matrix4.Identity;
            this.matrizDeTraslacion = Matrix4.CreateTranslation(new Vector3(centro.getX(), centro.getY(), centro.getZ()));

        }


        public Objeto(Dictionary<String,Parte>listaDePartes, Punto centro)
        {
      
            this.listaDePartes = listaDePartes;
            this.centroDeMasa = centro;
            this.matrizIdentidad = Matrix4.Identity;
            this.matrizDeEscalacion = Matrix4.Identity;
            this.matrizDeRotacionEjeX = Matrix4.Identity;
            this.matrizDeRotacionEjeY = Matrix4.Identity;
            this.matrizDeRotacionEjeZ = Matrix4.Identity;
            this.matrizGenerarlDelObjeto = Matrix4.Identity;
            this.matrizDeTraslacion = Matrix4.CreateTranslation(new Vector3(centro.getX(), centro.getY(), centro.getZ()));

        }

   


        public Parte getParte(string nombre)
        {
            if (this.listaDePartes.ContainsKey(nombre))
            {
                return this.listaDePartes[nombre];
            }
            return null;
        }
        public Matrix4 getMatrizTransformacion() {
            return this.matrizDeTraslacion;
        }

        public void setMatrizTransformacion(Matrix4 ma) {
            this.matrizDeTraslacion = ma;
        }


        public void trasladar(Punto puntos) {
            this.matrizDeTraslacion = Matrix4.CreateTranslation(puntos.getX(), puntos.getY(), puntos.getZ()) * this.matrizIdentidad;
        }



        public void rotar(float anguloRotacion,Punto ejeRotacion) {
            Matrix4 aux = Matrix4.CreateFromAxisAngle(new Vector3(ejeRotacion.getX(),ejeRotacion.getY(),ejeRotacion.getZ()), MathHelper.DegreesToRadians(anguloRotacion));
            switch (ejeRotacion) {
                case Punto p when p.getX() != 0:
                    this.matrizDeRotacionEjeX = Matrix4.CreateFromAxisAngle(new Vector3(ejeRotacion.getX(),ejeRotacion.getY(),ejeRotacion.getZ()),anguloRotacion);
                    break;
                case Punto p when p.getY() != 0:
                    this.matrizDeRotacionEjeY = Matrix4.CreateFromAxisAngle(new Vector3(ejeRotacion.getX(), ejeRotacion.getY(), ejeRotacion.getZ()), anguloRotacion);
                    break;
                case Punto p when p.getZ() != 0:
                    this.matrizDeRotacionEjeZ = Matrix4.CreateFromAxisAngle(new Vector3(ejeRotacion.getX(), ejeRotacion.getY(), ejeRotacion.getZ()), anguloRotacion);
                    break;
            
            }
        }



        public void escalar(Punto puntos) {

            this.matrizDeEscalacion = Matrix4.CreateScale(new Vector3(puntos.getX(), puntos.getY(), puntos.getZ()))*this.matrizIdentidad;
        }
        public Punto getCentroDeMasa() { 
            return this.centroDeMasa;
        }

        public void setCentroDeMasa(Punto centro) {
            this.centroDeMasa = centro;
        }


       /* public void Rotar(Vector3 eje, float angulo)
        {
            foreach (Parte parte in listaDePartes.Values)
            {
                parte.Rotar(eje, angulo);
            }
        }*/



        public void addParte(String nombre,Parte parte)
        {
            this.listaDePartes.Add(nombre, parte);
        }

        public void dibujar(Matrix4 matrizEscenario) {
            this.matrizGenerarlDelObjeto = matrizDeRotacionEjeX * matrizDeRotacionEjeY * matrizDeRotacionEjeZ * matrizDeEscalacion * matrizDeTraslacion;
            foreach (Parte parte in this.listaDePartes.Values){
                parte.dibujar(matrizEscenario * matrizGenerarlDelObjeto);
                //Console.WriteLine("Estas es la matriz Final :: " + this.matrizGenerarlDelObjeto * matrizEscenario);
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
            Console.WriteLine(this.matrizGenerarlDelObjeto*matrizEscenario);
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("---------------------------------------------");
        }


        public Punto sumaCentros(Punto punto1, Punto punto2)
        {
            return new Punto(punto1.getX() + punto2.getX(), punto1.getY() + punto2.getY(), punto1.getZ() + punto2.getZ());
        }






    }
}
