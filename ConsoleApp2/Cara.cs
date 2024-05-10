using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace ConsoleApp2
{
    public class Cara
    {
        public List<Punto> listaDePuntos { get; set; }
        public String nombre { get; set; }
        public Punto color { get; set; }

        private Punto centroDeMasa;
        public Cara() { 
            listaDePuntos=new List<Punto>();
            this.color = new Punto(0, 0, 0);
            this.nombre = "";
            this.centroDeMasa = new Punto(0, 0, 0);
            
        }

        public void setCentroDeMasa(Punto centro) {
            this.centroDeMasa = centro;
        }


        public Punto getCentroDeMasa() {
            return this.centroDeMasa;
        }


        public void sumarCentros(Punto centroPadre) {
            Punto suma = sumaCentros(centroPadre, this.centroDeMasa);
            this.centroDeMasa = suma;
        }

        public Cara(List<Punto>lista)
        {

            this.listaDePuntos = lista;
           
        }


        public void addPunto(Punto vertice) { 
            this.listaDePuntos.Add(vertice);
        }

        public void Rotar(Matrix4 rotacion)
        {
            // Aplicar la rotación a cada punto de la cara
            for (int i = 0; i < listaDePuntos.Count; i++)
            {
                Vector4 puntoRotado = Vector4.Transform(new Vector4(listaDePuntos[i].getX(), listaDePuntos[i].getY(), listaDePuntos[i].getZ(), 1), rotacion);
                listaDePuntos[i] = new Punto(puntoRotado.X, puntoRotado.Y, puntoRotado.Z);
            }
        }





        private Punto RotarPunto(Punto punto, Vector3 eje, float angulo)
        {
            Matrix4 rotacion = Matrix4.CreateFromAxisAngle(eje, MathHelper.DegreesToRadians(angulo));
            Vector4 puntoRotado = Vector4.Transform(new Vector4(punto.getX(), punto.getY(), punto.getZ(), 1), rotacion);
            return new Punto(puntoRotado.X, puntoRotado.Y, puntoRotado.Z);
        }


        public List<Punto> getCara(){

            return this.listaDePuntos;
        }

        public void dibujar(Matrix4 rotacion) {

            GL.PushMatrix();
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.5f, 0.1f, 1f);
            
            foreach (Punto pun in this.listaDePuntos)
            {
                //GL.Vertex3(pun.getX(), pun.getY(), pun.getZ());
                GL.Vertex3(Vector3.TransformPosition(new Vector3(pun.getX(),pun.getY(),pun.getZ()), rotacion));
              //  Console.WriteLine("Estas es la matriz Final :: "+ rotacion);

            }

            GL.End();
            GL.PopMatrix();



        }



        public Punto sumaCentros(Punto punto1, Punto punto2)
        {
            return new Punto(punto1.getX() + punto2.getX(), punto1.getY() + punto2.getY(), punto1.getZ() + punto2.getZ());
        }


    }
}
