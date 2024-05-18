using ConsoleApp2;
using OpenTK;
using System;

public class Accion
{
    private Objeto objeto;
    private float velocidadInicial;
    private float angulo;
    private float gravedad;
    private float tiempoTranscurrido;

    public Accion(Objeto objeto, float velocidadInicial, float angulo, float gravedad)
    {
        this.objeto = objeto;
        this.velocidadInicial = velocidadInicial;
        this.angulo = MathHelper.DegreesToRadians(angulo);
        this.gravedad = gravedad;
        this.tiempoTranscurrido = 0.0f;
    }

    public void Actualizar(float deltaTime)
    {
        tiempoTranscurrido += deltaTime;

       
        float posX = velocidadInicial * (float)Math.Cos(angulo) * tiempoTranscurrido;
        float posY = velocidadInicial * (float)Math.Sin(angulo) * tiempoTranscurrido - 0.2f * gravedad * tiempoTranscurrido ;
       /* if (posX > -2) { 
            posY =objeto.getCentroDeMasa().getY();
        }*/
      
        objeto.trasladar(new Punto(posX, posY, 0));
        Console.WriteLine("CEntro del Objeto",objeto.getCentroDeMasa().getX());
    }

    
}
