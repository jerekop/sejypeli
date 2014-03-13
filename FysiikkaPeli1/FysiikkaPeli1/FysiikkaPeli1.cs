using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class FysiikkaPeli1 : PhysicsGame
{  PhysicsObject Pelaaja;
PhysicsObject pallo;
    


   







     void PelaajaAlas()
    {
         Pelaaja.Hit(new Vector(0, -600));
    }


     void PelaajaVasemmalle()
     {
         Pelaaja.Hit(new Vector(-500, 0));
     }


     void PelaajaOikealle()
     {
         Pelaaja.Hit(new Vector(500, 0));
     }



    void PelaajaYlos()
    {
        Pelaaja.Hit(new Vector(0, 600));
    }

    void LuoKentta()
    {
        Level.CreateBorders();

        
    }



    void LuoPallo()
    {
       /* Vector isku1 = new Vector(100, 100);
        Vector isku2 = new Vector(100, 100);
        Vector isku3 = new Vector(100, 100);
        Vector isku4 = new Vector(100, 100);
        Vector isku5 = new Vector(100, 100);
        Vector isku6 = new Vector(100, 100);
        Vector isku7 = new Vector(100, 100);
        Vector isku8 = new Vector(100, 100);
        Vector isku9 = new Vector(100, 100);*/
        for (int i = 0; i < 9; i++)   
        {


            pallo = new PhysicsObject(50, 50);
            Vector Suunta = Vector.FromLengthAndAngle(500, pallo.Angle);

            pallo.Hit(Suunta);
            pallo.Position = RandomGen.NextVector(100, 500);
            pallo.Velocity = RandomGen.NextVector(100, 500);
            pallo.Shape = Shape.Circle;
            pallo.Tag = "pallo";
            pallo.KineticFriction = 0.0;
            pallo.Restitution = 1.0;
            Add(pallo);





            


        }
    }




    public override void Begin()
    {
        // TODO: Kirjoita ohjelmakoodisi tähän

        Keyboard.Listen(Key.Up, ButtonState.Down, PelaajaYlos, "PelaajaMeneeYlos");
        Keyboard.Listen(Key.Down, ButtonState.Down, PelaajaAlas, "PelaajaMeneeAlas");
        Keyboard.Listen(Key.Left, ButtonState.Down, PelaajaVasemmalle, "PelaajaMeneevasemmalle");
        Keyboard.Listen(Key.Right, ButtonState.Down, PelaajaOikealle, "PelaajaMeneeOikealle");
        

        Pelaaja = new PhysicsObject(20.0, 20.0);
        Pelaaja.Shape = Shape.Circle;
        Pelaaja.Color = Color.BloodRed;
        Add(Pelaaja);

        Pelaaja.Mass = 80;

        PelaajaYlos();
        LuoPallo();
        LuoKentta();

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");



        Camera.ZoomToLevel();

        AddCollisionHandler(Pelaaja, PelaajaTormaa);




    }
    void PelaajaTormaa(PhysicsObject Tormaaja, PhysicsObject Kohde)
    {
        if (Kohde.Tag == "pallo")
        {


            Explosion rajahdys = new Explosion(50);
            rajahdys.Position = Pelaaja.Position;
            Add(rajahdys);
            Pelaaja.IgnoresExplosions = true;
            Kohde.Destroy();
            Tormaaja.Destroy();
        }
    }
}