using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Televison
    {

        private Vector3 centroDeMasa;


        public Televison(Vector3 centroDeMasa)
        {
            this.centroDeMasa = centroDeMasa;
        }

        public void generarVertices() {
           //dibujarPlano();

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(1f, 1f, 1f);        // caja delantera
            GL.Vertex3(-1f, 0.3f, 0.3f);
            GL.Vertex3(1f, 0.3f, 0.3f);
            GL.Vertex3(1f, -0.3f,0.3f);
            GL.Vertex3(-1f, -0.3f, 0.3f);
            GL.End();



            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0f, 0f, 0f);          // caja trasera
            GL.Vertex3(-1f, 0.3f, 0.0f);
            GL.Vertex3(1f, 0.3f, 0.0f);
            GL.Vertex3(1f, -0.3f, 0.0f);
            GL.Vertex3(-1f, -0.3f, 0.0f);
            GL.End();


            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0f, 1f, 0f);
            GL.Vertex3(-1f, 0.3f, 0.3f);
            GL.Vertex3(1f, 0.3f, 0.3f);      // tapa superior
            GL.Vertex3(1f, 0.3f, 0.0f);
            GL.Vertex3(-1f, 0.3f, 0.0f);
            
            GL.End();


            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0f, 0f, 1f);
            GL.Vertex3(1f, -0.3f, 0.3f);
            GL.Vertex3(-1f, -0.3f, 0.3f);         // tapa inferior
            GL.Vertex3(-1f, -0.3f, 0.0f);
            GL.Vertex3(1f, -0.3f, 0.0f);
            GL.End();


            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0f, 0f, 1f);
            GL.Vertex3(-0.5f, -0.3f, 0.3f);
            GL.Vertex3(0.5f, -0.3f, 0.3f);          // Soporte Delantero
            GL.Vertex3(0.5f, -0.5, 0.3f);
            GL.Vertex3(-0.5f, -0.5f, 0.3f);
            GL.End();



            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0f, 0f, 1f);
            GL.Vertex3(-0.5f, -0.3f, 0f);
            GL.Vertex3(0.5f, -0.3f, 0f);          // Soporte Trasero
            GL.Vertex3(0.5f, -0.5, 0f);
            GL.Vertex3(-0.5f, -0.5f, 0f);
            GL.End();



            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0f, 0f, 1f);                // Soporte Inferior
            GL.Vertex3(0.5f, -0.5, 0.3f);
            GL.Vertex3(-0.5f, -0.5f, 0.3f);
            GL.Vertex3(-0.5f, -0.5f, 0f);
            GL.Vertex3(0.5f, -0.5, 0f);
            GL.End();


        }



        public void dibujarPlano() {
            generarLineas();
            generarEjeX();
            generarEjeY();
        
        }


        public void generarLineas() {
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(0, 1f, 1f);
            GL.Vertex3(0f, 5f, 0f);
            GL.Vertex3(0f, -5f, 0f);            // Genera el Plano X:Y 
            GL.Vertex3(-5f, 0f, 0f);
            GL.Vertex3(5f, 0f, 0f);
            GL.End();
        }


        public void generarEjeX() {
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(0, 1f, 1f);
            for (int i = 1; i <= 5; i++) {

                GL.Vertex3((float)i, 0.5f, 0f); 
                GL.Vertex3((float)i, -0.5f, 0f);

                GL.Vertex3((float)i*-1, 0.5f, 0f);
                GL.Vertex3((float)i*-1, -0.5f, 0f);
            }
            GL.End();
        }

        public void generarEjeY() {
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(0, 1f, 1f);
            for (int i = 1; i <= 5; i++)
            {

                GL.Vertex3(0.5f,(float)i, 0f);
                GL.Vertex3(-0.5f,(float)i, 0f);

                GL.Vertex3(0.5f,(float)i * -1, 0f);
                GL.Vertex3(-0.5f,(float)i * -1, 0f);
            }
            GL.End();
        }

      


        public Vector3 getCentroDeMasa() {
            return this.centroDeMasa;
        }
    }
}
