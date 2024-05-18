using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ConsoleApp2
{
    public partial class Game
    {

        private GameWindow miVentana;
        private float anguloDeVision;
        private Escenario escenario;
        private Objeto tele;
        private Objeto florero;
        private Objeto equipoMusica;
        private String json;
        private String rutaEquipo = "C:\\Users\\hp\\Documents\\ObjetosOpenTK\\equipo.json";
        private String rutaFlorero = "C:\\Users\\hp\\Documents\\ObjetosOpenTK\\florero.json";
        private String rutaTele = "C:\\Users\\hp\\Documents\\ObjetosOpenTK\\tele.json";
        private String rutaEscenario = "C:\\Users\\hp\\Documents\\ObjetosOpenTK\\escenario.json";
        private Accion accion;
        private Objeto pelota;

        private float camX = 0, camY=0, camZ=0;
        
        public Game(GameWindow myWindow) {
            this.miVentana = myWindow;
            
            miVentana.Load += MiVentana_Load;
           
            miVentana.RenderFrame += MiVentana_RenderFrame;
            miVentana.UpdateFrame += MiVentana_UpdateFrame;
            miVentana.KeyUp += MiVentana_KeyUp;
            miVentana.KeyDown += MiVentana_KeyDown;
            miVentana.Closing += MiVentana_Closing;
           
            tele = new Objeto(new Punto(0,0,0));
            florero = new Objeto(new Punto(0,0,0));
            equipoMusica = new Objeto(new Punto(0,0,0));
            pelota = new Objeto(new Punto(0, 0, 0));
            //this.cargarPelota();
           
            this.cargarObjetoTv();
            this.cargarObjetoEquipo();
            this.cargarObjetoFlorero();

            accion = new Accion(pelota, 0.5f, 45, -9.8f);
            
            escenario =new Escenario(new Punto(0,0,0));

            //escenario.escalar(new Punto(2,1,1));
            //escenario.rotar(45, new Punto(0, 0, 1));
            // tele.rotar(45, new Punto(0, 1, 0));
            //florero.trasladar(new Punto(0, 0, 0));
            equipoMusica.getParte("cerebro").trasladar(new Punto(0, 0, -4));
            equipoMusica.getParte("cerebro").rotar(30, new Punto(1, 0, 0));
            //equipoMusica.getParte("parlanteDerecho").trasladar(new Punto(0, 0,-8));

            escenario.addObjeto("televison",tele);
            escenario.addObjeto("Equipo",equipoMusica);
            escenario.addObjeto("Florero",florero);
            escenario.addObjeto("pelota", this.pelota);

            
           
             
        }

     


        public void serializarObjeto(String ruta,Objeto obj) {
            try
            {
                json = JsonConvert.SerializeObject(obj);
                File.WriteAllText(ruta, json);
                Console.WriteLine("El objeto ha sido serializado y guardado en el archivo: " + ruta);
            }
            catch (Exception error) {
                Console.WriteLine("Error al Serializar El objeto en la ruta : " + ruta);
                Console.WriteLine("Error al Serializar El objeto : " + error);
            }
           
        }


        public Objeto deserializarObjeto(String ruta)
        {
            if (File.Exists(ruta))
            {
                try
                {
                    string jsonContent = File.ReadAllText(ruta);
                    return JsonConvert.DeserializeObject<Objeto>(jsonContent);
                   
                }
                catch (Exception error)
                {
                    
                    Console.WriteLine("Error al deserializar: "+ ruta +"  --->" + error);
                }
            }
            return null;
        }

       


        private void MiVentana_KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if(e.Keyboard.IsKeyDown(OpenTK.Input.Key.Right)){
                //actualizarVisionX();
            }
            if (e.Keyboard.IsKeyDown(OpenTK.Input.Key.Left))
            {
               // actualizarVisionMenosX();
            }
        }

        private void MiVentana_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }


        public void ActualizarVisionZ() {
            camZ = (float)Math.Cos(anguloDeVision * Math.PI / 180.0) * 3.0f;
            camY = (float)Math.Cos(anguloDeVision * Math.PI / 180.0) * 3.0f;
        }


        public void actualizarVisionX() {
            camX = (float)Math.Sin(anguloDeVision * Math.PI / 180.0) * 3.0f;
            camY = (float)Math.Cos(anguloDeVision * Math.PI / 180.0) * 3.0f;
            camZ = 0;
        }

        public void actualizarVisionMenosX()
        {
            camX = (float)Math.Sin(anguloDeVision * Math.PI / -180.0) * 3.0f;
            camY = (float)Math.Cos(anguloDeVision * Math.PI / 180.0) * 3.0f;
            camZ = 0;
        }


            public void actualizarVisionY() { 
        
        }

        private void MiVentana_KeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {

            anguloDeVision += (float)0.02 * 90.0f;

            if (anguloDeVision > 360.0f)
            {
                anguloDeVision -= 360.0f;
            }

        }

        

        private void MiVentana_UpdateFrame(object sender, FrameEventArgs e)
        {
           //anguloDeVision += (float)0.02 * 90.0f;
            
            if (anguloDeVision > 360.0f)
            {
                anguloDeVision -= 360.0f;
            }
           // Console.WriteLine("Actualizando Ventana");
            
        }

        private void MiVentana_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            accion.Actualizar((float)e.Time);
            // Configurar una proyección en perspectiva con un ángulo de visión más amplio
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60f), (float)miVentana.Width / miVentana.Height, 0.1f, 100f);
            GL.LoadMatrix(ref perspective);


            /*GL.MatrixMode(MatrixMode.Modelview);
            Matrix4 rotation = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(anguloDeVision));*/
            //Parte a = this.equipoMusica.getParte("cerebro");
            //Console.WriteLine("esta es la parte --->> CEREBRO ---->>>  : " + a.ToString());
            escenario.dibujar();
            actualizarPanorama();
            miVentana.SwapBuffers();

        }

        private void actualizarPanorama()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

           
            float camX = (float)Math.Sin(anguloDeVision * Math.PI / 180.0) * 3.0f;
            float camZ = (float)Math.Cos(anguloDeVision * Math.PI / 180.0) * 3.0f;
            Vector3 cameraPosition = new Vector3(camX, 1, camZ); 
            Vector3 cameraTarget = new Vector3(0, 0, 0); // El objeto está en el origen
            Vector3 cameraUp = new Vector3(0, 1, 0);

            Matrix4 lookAt = Matrix4.LookAt(cameraPosition, cameraTarget, cameraUp);
           
            GL.LoadMatrix(ref lookAt);
        }

        private void MiVentana_Load(object sender, EventArgs e)
        {
            GL.ClearColor(0f,0f,0f,1f);
            GL.Enable(EnableCap.DepthTest);
            Console.WriteLine("Se Cargo La Ventana");
        }


        public void cargarObjetoTv()
        {
            if (File.Exists(rutaTele))
            {
                try
                {
                    this.tele = this.deserializarObjeto(rutaTele);
                    Console.WriteLine("Se derserializo el objeto de la ruta: " + rutaTele);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("No se pudo deserializar el archivo: " + rutaTele);
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                this.iniciarPuntosTele();
                
                Console.WriteLine("Se cargo desde el codigo el  objeto: Tele ");
            }
        }


        public void cargarObjetoFlorero()
        {
            if (File.Exists(rutaFlorero))
            {
                try
                {
                    this.florero = this.deserializarObjeto(rutaFlorero);
                    Console.WriteLine("Se derserializo el objeto de la ruta: " + rutaFlorero);
                }
                catch (Exception e)
                {
                    Console.WriteLine("No se pudo deserializar el archivo :" + rutaFlorero);
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                this.iniciarPuntosFlorero();
                Console.WriteLine("Se cargo desde el codigo el  objeto: Florero ");
            }
        }


        public void cargarObjetoEquipo()
        {
            if (File.Exists(rutaEquipo))
            {
                try
                {
                    this.equipoMusica = this.deserializarObjeto(rutaEquipo);
                    Console.WriteLine("Se derserializo el objeto de la ruta: " + rutaEquipo);
                }
                catch (Exception e)
                {
                    Console.WriteLine("No se pudo deserializar el archivo : " + rutaEquipo);
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                this.iniciarPuntosEquipo();
                Console.WriteLine("Se cargo desde el codigo el  objeto: Equipo ");
            }
        }

        public void iniciarPuntosEquipo()
        {


            // parlante Izquierdo
            // cara frontal

            Parte equipo = new Parte(new Punto(0, 0, 0));


            Cara caraUnica = new Cara();

            caraUnica.addPunto(new Punto(1.2f, -0.2f, 0.3f));
            caraUnica.addPunto(new Punto(1.4f, -0.2f, 0.3f));
            caraUnica.addPunto(new Punto(1.4f, 0.5f, 0.3f));
            caraUnica.addPunto(new Punto(1.2f, 0.5f, 0.3f));
            equipo.agregarCara("fronal", caraUnica);
            //equipo.addCara("frontal",caraUnica);



            // cara trasera
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(1.2f, -0.2f, 0f));
            caraUnica.addPunto(new Punto(1.4f, -0.2f, 0f));
            caraUnica.addPunto(new Punto(1.4f, 0.5f, 0f));
            caraUnica.addPunto(new Punto(1.2f, 0.5f, 0f));
            equipo.agregarCara("trasera", caraUnica);

            // lateral izquierdo

            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(1.2f, -0.2f, 0.3f));
            caraUnica.addPunto(new Punto(1.2f, -0.2f, 0.0f));
            caraUnica.addPunto(new Punto(1.2f, 0.5f, 0.0f));
            caraUnica.addPunto(new Punto(1.2f, 0.5f, 0.3f));
            equipo.agregarCara("izquierda", caraUnica);
            //lateral derecho


            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(1.4f, -0.2f, 0.3f));
            caraUnica.addPunto(new Punto(1.4f, -0.2f, 0.0f));
            caraUnica.addPunto(new Punto(1.4f, 0.5f, 0.0f));
            caraUnica.addPunto(new Punto(1.4f, 0.5f, 0.3f));
            equipo.agregarCara("derecha", caraUnica);

            //parte inferior

            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(1.2f, -0.2f, 0.3f));
            caraUnica.addPunto(new Punto(1.4f, -0.2f, 0.3f));
            caraUnica.addPunto(new Punto(1.4f, -0.2f, 0.0f));
            caraUnica.addPunto(new Punto(1.2f, -0.2f, 0.0f));
            equipo.agregarCara("inferior", caraUnica);




            // parte superior

            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(1.2f, 0.5f, 0.3f));
            caraUnica.addPunto(new Punto(1.4f, 0.5f, 0.3f));
            caraUnica.addPunto(new Punto(1.4f, 0.5f, 0.0f));
            caraUnica.addPunto(new Punto(1.2f, 0.5f, 0.0f));
            equipo.agregarCara("superior", caraUnica);
            this.equipoMusica.addParte("parlanteIzquierdo", equipo);

            //---------------------- AQUI ES UNA PARTE-> PARLANTE IZQUIERDO ------------------

            // cerebro
            // cara delantera
            caraUnica = new Cara();
            equipo = new Parte();
            caraUnica.addPunto(new Punto(1.45f, -0.2f, 0.3f));
            caraUnica.addPunto(new Punto(1.8f, -0.2f, 0.3f));
            caraUnica.addPunto(new Punto(1.8f, 0.3f, 0.3f));
            caraUnica.addPunto(new Punto(1.45f, 0.3f, 0.3f));
            equipo.agregarCara("cerebroFrontal", caraUnica);

            // cara Trasera

            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(1.45f, -0.2f, 0.0f));
            caraUnica.addPunto(new Punto(1.8f, -0.2f, 0.0f));
            caraUnica.addPunto(new Punto(1.8f, 0.3f, 0.0f));
            caraUnica.addPunto(new Punto(1.45f, 0.3f, 0.0f));
            equipo.agregarCara("cerebroTraseara", caraUnica);


            // parte Inferior
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(1.45f, -0.2f, 0.3f));
            caraUnica.addPunto(new Punto(1.8f, -0.2f, 0.3f));
            caraUnica.addPunto(new Punto(1.8f, -0.2f, 0.0f));
            caraUnica.addPunto(new Punto(1.45f, -0.2f, 0.0f));
            equipo.agregarCara("cerebroInferior", caraUnica);


            // parte Superior
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(1.45f, 0.3f, 0.3f));
            caraUnica.addPunto(new Punto(1.8f, 0.3f, 0.3f));
            caraUnica.addPunto(new Punto(1.8f, 0.3f, 0.0f));
            caraUnica.addPunto(new Punto(1.45f, 0.3f, 0.0f));
            equipo.agregarCara("superiorCerebro", caraUnica);

            // parte izquierda
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(1.45f, -0.2f, 0.3f));
            caraUnica.addPunto(new Punto(1.45f, -0.2f, 0.0f));
            caraUnica.addPunto(new Punto(1.45f, 0.3f, 0.0f));
            caraUnica.addPunto(new Punto(1.45f, 0.3f, 0.3f));
            equipo.agregarCara("izquierdaCerebro", caraUnica);

            // parte Derecha
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(1.8f, -0.2f, 0.3f));
            caraUnica.addPunto(new Punto(1.8f, -0.2f, 0.0f));
            caraUnica.addPunto(new Punto(1.8f, 0.3f, 0.0f));
            caraUnica.addPunto(new Punto(1.8f, 0.3f, 0.3f));
            equipo.agregarCara("derechaCerebro", caraUnica);
            this.equipoMusica.addParte("cerebro", equipo);

            //---------------------- AQUI ES UNA PARTE -> CEREBRO ------------------

            // parlante Derecho

            // cara frontal
            caraUnica = new Cara();
            equipo = new Parte();
            caraUnica.addPunto(new Punto(1.85f, -0.2f, 0.3f));
            caraUnica.addPunto(new Punto(2f, -0.2f, 0.3f));
            caraUnica.addPunto(new Punto(2f, 0.5f, 0.3f));
            caraUnica.addPunto(new Punto(1.85f, 0.5f, 0.3f));
            equipo.agregarCara("pDelanteroDerecho", caraUnica);


            // cara trasera

            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(1.85f, -0.2f, 0.0f));
            caraUnica.addPunto(new Punto(2f, -0.2f, 0.0f));
            caraUnica.addPunto(new Punto(2f, 0.5f, 0.0f));
            caraUnica.addPunto(new Punto(1.85f, 0.5f, 0.0f));
            equipo.agregarCara("pTraseraDerecho", caraUnica);


            // lateral izquierdo
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(1.85f, -0.2f, 0.3f));
            caraUnica.addPunto(new Punto(1.85f, -0.2f, 0.0f));
            caraUnica.addPunto(new Punto(1.85f, 0.5f, 0.0f));
            caraUnica.addPunto(new Punto(1.85f, 0.5f, 0.3f));
            equipo.agregarCara("pIzquierdoDerecho", caraUnica);

            //lateral derecho

            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(2f, -0.2f, 0.3f));
            caraUnica.addPunto(new Punto(2f, -0.2f, 0.0f));
            caraUnica.addPunto(new Punto(2f, 0.5f, 0.0f));
            caraUnica.addPunto(new Punto(2f, 0.5f, 0.3f));
            equipo.agregarCara("pDerechoDerecho", caraUnica);


            //parte inferior


            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(1.85f, -0.2f, 0.3f));
            caraUnica.addPunto(new Punto(2f, -0.2f, 0.3f));
            caraUnica.addPunto(new Punto(2f, -0.2f, 0.0f));
            caraUnica.addPunto(new Punto(1.85f, -0.2f, 0.0f));
            equipo.agregarCara("pInferiorDerecho", caraUnica);



            // parte superior

            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(1.85f, 0.5f, 0.3f));
            caraUnica.addPunto(new Punto(2f, 0.5f, 0.3f));
            caraUnica.addPunto(new Punto(2f, 0.5f, 0.0f));
            caraUnica.addPunto(new Punto(1.85f, 0.5f, 0.0f));
            equipo.agregarCara("pSuperiorDerecho", caraUnica);

            this.equipoMusica.addParte("parlanteDerecho", equipo);

            this.serializarObjeto(rutaEquipo, this.equipoMusica);

        }


        public void iniciarPuntosFlorero()
        {
            Cara caraUnica = new Cara();
            Parte parteFlorero = new Parte();
            caraUnica.addPunto(new Punto(-0.4f, 0.4f, 0.3f));
            caraUnica.addPunto(new Punto(0.4f, 0.4f, 0.3f));
            caraUnica.addPunto(new Punto(0.6f, 0.9f, 0.3f));
            caraUnica.addPunto(new Punto(-0.6f, 0.9f, 0.3f));
            parteFlorero.agregarCara("caraDelantera", caraUnica);
            //florero.addCara(caraUnica);

            // cara frontal del florero
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(-0.4f, 0.4f, 0.0f));
            caraUnica.addPunto(new Punto(0.4f, 0.4f, 0.0f));
            caraUnica.addPunto(new Punto(0.6f, 0.9f, 0.0f));
            caraUnica.addPunto(new Punto(-0.6f, 0.9f, 0.0f));
            parteFlorero.agregarCara("caraTrasera", caraUnica);
            //florero.addCara(caraUnica);

            // cara inferior del florero
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(-0.4f, 0.4f, 0.3f));
            caraUnica.addPunto(new Punto(-0.4f, 0.4f, 0.0f));
            caraUnica.addPunto(new Punto(0.4f, 0.5f, 0.0f));
            caraUnica.addPunto(new Punto(0.4f, 0.5f, 0.3f));
            parteFlorero.agregarCara("caraInferior", caraUnica);
            //florero.addCara(caraUnica);

            // cara lateral izquierda del florero

            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(-0.4f, 0.4f, 0.3f));
            caraUnica.addPunto(new Punto(-0.4f, 0.4f, 0.0f));
            caraUnica.addPunto(new Punto(-0.6f, 0.9f, 0.0f));
            caraUnica.addPunto(new Punto(-0.6f, 0.9f, 0.3f));
            parteFlorero.agregarCara("caraIzquierda", caraUnica);
            //florero.addCara(caraUnica);

            // cara lateral derecha del florero
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(0.4f, 0.4f, 0.3f));
            caraUnica.addPunto(new Punto(0.4f, 0.4f, 0.0f));
            caraUnica.addPunto(new Punto(0.6f, 0.9f, 0.0f));
            caraUnica.addPunto(new Punto(0.6f, 0.9f, 0.3f));
            parteFlorero.agregarCara("caraDeracha", caraUnica);
            //florero.addCara(caraUnica);
            florero.addParte("total", parteFlorero);

            this.serializarObjeto(rutaFlorero, this.florero);

        }



        public void iniciarPuntosTele()
        {
            Parte parteTv = new Parte();

            Cara caraUnica = new Cara();
            caraUnica.addPunto(new Punto(-1f, 0.3f, 0.3f));
            caraUnica.addPunto(new Punto(1f, 0.3f, 0.3f));
            caraUnica.addPunto(new Punto(1f, -0.3f, 0.3f));
            caraUnica.addPunto(new Punto(-1f, -0.3f, 0.3f));
            parteTv.agregarCara("caraFronta", caraUnica);
            //tele.addCara(caraUnica);

            caraUnica = new Cara();
            // Definir los vértices para la cara trasera
            caraUnica.addPunto(new Punto(-1f, 0.3f, 0.0f));
            caraUnica.addPunto(new Punto(1f, 0.3f, 0.0f));
            caraUnica.addPunto(new Punto(1f, -0.3f, 0.0f));
            caraUnica.addPunto(new Punto(-1f, -0.3f, 0.0f));
            parteTv.agregarCara("caraTrasera", caraUnica);


            // Definir los vértices para la tapa superior
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(-1f, 0.3f, 0.3f));
            caraUnica.addPunto(new Punto(1f, 0.3f, 0.3f));
            caraUnica.addPunto(new Punto(1f, 0.3f, 0.0f));
            caraUnica.addPunto(new Punto(-1f, 0.3f, 0.0f));
            parteTv.agregarCara("caraSuperior", caraUnica);


            // Definir los vértices para la tapa inferior
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(1f, -0.3f, 0.3f));
            caraUnica.addPunto(new Punto(-1f, -0.3f, 0.3f));
            caraUnica.addPunto(new Punto(-1f, -0.3f, 0.0f));
            caraUnica.addPunto(new Punto(1f, -0.3f, 0.0f));
            parteTv.agregarCara("parteInferior", caraUnica);
            this.tele.addParte("pantalla", parteTv);
            //parteTv.setCentroDeMasa(new Punto(0.5f, 2.0f, 0.3f));
            // aqui se debe agregar esta parte al objeto en general 


            // Definir los vértices para el soporte delantero
            parteTv = new Parte();
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(-0.5f, -0.3f, 0.3f));
            caraUnica.addPunto(new Punto(0.5f, -0.3f, 0.3f));
            caraUnica.addPunto(new Punto(0.5f, -0.5f, 0.3f));
            caraUnica.addPunto(new Punto(-0.5f, -0.5f, 0.3f));
            parteTv.agregarCara("soporteFrontal", caraUnica);




            // Definir los vértices para el soporte trasero
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(-0.5f, -0.3f, 0.0f));
            caraUnica.addPunto(new Punto(0.5f, -0.3f, 0.0f));
            caraUnica.addPunto(new Punto(0.5f, -0.5f, 0.0f));
            caraUnica.addPunto(new Punto(-0.5f, -0.5f, 0.0f));
            parteTv.agregarCara("soporteTrasero", caraUnica);



            // Definir los vértices para el soporte inferior
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(0.5f, -0.5f, 0.3f));
            caraUnica.addPunto(new Punto(-0.5f, -0.5f, 0.3f));
            caraUnica.addPunto(new Punto(-0.5f, -0.5f, 0.0f));
            caraUnica.addPunto(new Punto(0.5f, -0.5f, 0.0f));
            parteTv.agregarCara("soporteInferior", caraUnica);
            //parteTv.setCentroDeMasa(new Punto(0.5f, 2.0f, 0.3f));
            this.tele.addParte("soporte", parteTv);


            //-------------------PELOTA ---------------------------


            Parte partePelota = new Parte();
            float half = 0.1f / 2.0f;
            float offsetX = 3f;
            // Cara frontal
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(-half-offsetX, half, half));   // Superior izquierda
            caraUnica.addPunto(new Punto(half - offsetX, half, half));    // Superior derecha
            caraUnica.addPunto(new Punto(half - offsetX, -half, half));   // Inferior derecha
            caraUnica.addPunto(new Punto(-half - offsetX, -half, half));  // Inferior izquierda
            partePelota.agregarCara("frente", caraUnica);

            // Cara trasera
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(-half - offsetX, half, -half));
            caraUnica.addPunto(new Punto(half -offsetX, half, -half));
            caraUnica.addPunto(new Punto(half - offsetX, -half, -half));
            caraUnica.addPunto(new Punto(-half - offsetX, -half, -half));
            partePelota.agregarCara("trasera", caraUnica);

            // Cara superior
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(-half - offsetX, half, half));
            caraUnica.addPunto(new Punto(half - offsetX, half, half));
            caraUnica.addPunto(new Punto(half - offsetX, half, -half));
            caraUnica.addPunto(new Punto(-half - offsetX, half, -half));
            partePelota.agregarCara("superior", caraUnica);

            // Cara inferior
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(-half - offsetX, -half, half));
            caraUnica.addPunto(new Punto(half - offsetX, -half, half));
            caraUnica.addPunto(new Punto(half - offsetX, -half, -half));
            caraUnica.addPunto(new Punto(-half - offsetX, -half, -half));
            partePelota.agregarCara("inferior", caraUnica);

            // Cara izquierda
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(-half - offsetX, half, half));
            caraUnica.addPunto(new Punto(-half - offsetX, half, -half));
            caraUnica.addPunto(new Punto(-half - offsetX, -half, -half));
            caraUnica.addPunto(new Punto(-half - offsetX, -half, half));
            partePelota.agregarCara("izquierd", caraUnica);

            // Cara derecha
            caraUnica = new Cara();
            caraUnica.addPunto(new Punto(half - offsetX, half, half));
            caraUnica.addPunto(new Punto(half - offsetX, half, -half));
            caraUnica.addPunto(new Punto(half - offsetX, -half, -half));
            caraUnica.addPunto(new Punto(half - offsetX, -half, half));
            partePelota.agregarCara("derecha", caraUnica);
            this.pelota.addParte("frontal", partePelota);
            this.serializarObjeto(rutaTele, this.tele);
        }

           
        



        public void cargarPelota() {
           

        }


    }
}
