using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Audio;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace ConsoleApp2
{
    public class Escenario
    {
        public Dictionary<String, Objeto> listaDeObjetos { get; set; }
        public Punto centroDeMasa;
        public Matrix4 matrizDeEscalacion { get; set; }
        public Matrix4 matrizDeTraslacion { get; set; }
        public Matrix4 matrizDeRotacionEjeX { get; set; }
        public Matrix4 matrizDeRotacionEjeY { get; set; }
        public Matrix4 matrizDeRotacionEjeZ { get; set; }


        public Matrix4 matrizIdentidad;


        public Escenario(Dictionary<String,Objeto>lista) {
            this.listaDeObjetos = lista;
            this.centroDeMasa = new Punto(0,0,0);
            this.matrizIdentidad = Matrix4.Identity;
            this.matrizDeEscalacion = Matrix4.Identity;
            this.matrizDeRotacionEjeX = Matrix4.Identity;
            this.matrizDeRotacionEjeY = Matrix4.Identity;
            this.matrizDeRotacionEjeZ = Matrix4.Identity;
            this.matrizDeTraslacion = Matrix4.Identity;

        }


      


        public Escenario(Punto inicioDelObjeto) { 
            this.listaDeObjetos=new Dictionary<String,Objeto>();
            this.centroDeMasa = inicioDelObjeto;//new Punto(0, 0, 0);
            this.matrizIdentidad = Matrix4.Identity;
            this.matrizDeEscalacion = Matrix4.Identity;
            this.matrizDeRotacionEjeX = Matrix4.Identity;
            this.matrizDeRotacionEjeY = Matrix4.Identity;
            this.matrizDeRotacionEjeZ = Matrix4.Identity;
            this.matrizDeTraslacion = Matrix4.CreateTranslation(new Vector3(inicioDelObjeto.getX(), inicioDelObjeto.getY(), inicioDelObjeto.getZ()));

        }

        public Escenario()
        {
            this.listaDeObjetos = new Dictionary<String, Objeto>();
            this.centroDeMasa = new Punto(0, 0, 0);
            this.matrizIdentidad = Matrix4.Identity;
            this.matrizDeEscalacion = Matrix4.Identity;
            this.matrizDeRotacionEjeX = Matrix4.Identity;
            this.matrizDeRotacionEjeY = Matrix4.Identity;
            this.matrizDeRotacionEjeZ = Matrix4.Identity;
            this.matrizDeTraslacion = Matrix4.Identity;

        }



        /* public Escenario(Punto centro) {
             this.centroDeMasa = centro;
             this.listaDeObjetos = new Dictionary<string, Objeto>();

         }*/

        public void setIdentidad(Matrix4 ma) {
            this.matrizIdentidad = ma;
        }


        public Matrix4 getIdentdidad() {
            return this.matrizIdentidad;
        }

        


        public void trasladar(Punto direccionTraslacion) {
            this.matrizDeTraslacion = Matrix4.CreateTranslation(direccionTraslacion.getX(), direccionTraslacion.getY(), direccionTraslacion.getZ()) * this.matrizIdentidad;
        }


        public void rotar(float anguloRotacion, Punto ejeRotacion) {
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

        public void escalar(Punto coordenadasAEscalar) {
            this.matrizDeEscalacion = Matrix4.CreateScale(new Vector3(coordenadasAEscalar.getX(), coordenadasAEscalar.getY(), coordenadasAEscalar.getZ())) * this.matrizIdentidad;
            
        }

        public void setCentroDeMasa(Punto centro)
        {
            this.centroDeMasa = centro;
          
        }

        public Punto getCentroDeMasa() {
            return this.centroDeMasa;
        }

        public void addObjeto(String nombre,Objeto obj) {
            this.listaDeObjetos.Add(nombre,obj);
        }


        public void dibujar() {
            Matrix4 tranformacionCompleta = this.matrizDeTraslacion*this.matrizDeRotacionEjeX * this.matrizDeRotacionEjeY * this.matrizDeRotacionEjeZ * this.matrizDeEscalacion;
            foreach (Objeto objstt in this.listaDeObjetos.Values) {
                objstt.dibujar(tranformacionCompleta);
                
            }
           
        }

        

       

        public Punto sumaCentros(Punto punto1,Punto punto2)
        {
            return new Punto(punto1.getX()+punto2.getX(),punto1.getY()+punto2.getY(),punto1.getZ()+punto2.getZ());  
        }

    }
}
