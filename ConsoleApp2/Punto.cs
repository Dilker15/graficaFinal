using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace ConsoleApp2
{
    public class Punto
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }


        public Punto(float x, float y, float z) { 
           this.x= x;
           this.y= y;  
           this.z= z;
        }

        

      
        
        public void setX(float x) { this.x = x; }

        public void setY(float y) {  this.y = y; }

        public void setZ(float z) { this.z = z; }
        public float getX() {
            return this.x;
        }

        public float getY() { return this.y; }
        

        public float getZ() { return this.z;}


        public string toString()
        {
            String cadena = "";
            cadena += "x: " + x + "\n";
            cadena += "y: " + y + "\n";
            cadena += "z: " + z + "\n";
            return cadena;

        }
    }
}
