using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class FysiikkaPeli1 : PhysicsGame
{
    PhysicsObject Pelaaja;
    PhysicsObject pallo;
    Timer aikaLaskuri;
    AssaultRifle PelaajanAse;

    
    Image taustaKuva = LoadImage("gg wp");
    void AmmusOsui(PhysicsObject ammus, PhysicsObject kohde)
    {
        if (kohde.Tag == "pallo")
        {
            ammus.Destroy();
            kohde.Destroy();
        }
        ammus.Destroy();
    }
    void AmmuAseella()
    {
        PelaajanAse.Shoot();
    }

    void LuoaikaLaskuri()
    {
        Timer aikaLaskuri = new Timer();
        aikaLaskuri.Start();

        Label aikaNayttö = new Label();
        aikaNayttö.TextColor = Color.White;
        aikaNayttö.DecimalPlaces = 1;
        aikaNayttö.BindTo(aikaLaskuri.SecondCounter);
        Add(aikaNayttö);
        
    }

    


    void PelaajaAlas()
    {
        Pelaaja.Hit(new Vector(0, -600));
    }
    void PelaajaVasemmalle()
    {
        Pelaaja.Hit(new Vector(-600, 0));
    }
    void PelaajaOikealle()
    {
        Pelaaja.Hit(new Vector(600, 0));
    }
    
    void PelaajaYlos()
    {
        Pelaaja.Hit(new Vector(0, 600));
    }

    void LuoKentta()
    {
        Level.CreateBorders(false);
    }

    void LuoPallo()
    {

        for (int i = 0; i < 10; i++)
        {
            pallo = new PhysicsObject(70, 70);
            Vector Suunta = Vector.FromLengthAndAngle(500, pallo.Angle);

            pallo.Hit(Suunta);
            pallo.Position = RandomGen.NextVector(200, 500);
            pallo.Velocity = RandomGen.NextVector(100, 500);
            pallo.Shape = Shape.Circle;
            pallo.Tag = "pallo";
            pallo.KineticFriction = 0.0;
            pallo.Restitution = 1.0;
            pallo.Image = LoadImage("pallo");
            Add(pallo);
        }


        
    }

   
    public override void Begin()
    {


        

       // TODO: Kirjoita ohjelmakoodisi tähän
        IsFullScreen = true;
        Level.Background.Image = taustaKuva;
        Level.Background.FitToLevel();

        Pelaaja = new PhysicsObject(100.0, 100.0);
        Pelaaja.Shape = Shape.Circle;
        Pelaaja.Color = Color.BloodRed;
        Pelaaja.Image = LoadImage("pelaaja");
        Add(Pelaaja);                          
        Pelaaja.Mass = 80;  
        PelaajanAse = new AssaultRifle(20, 5);
        PelaajanAse.Ammo.Value = 9065;
        PelaajanAse.ProjectileCollision = AmmusOsui;
        Pelaaja.Add(PelaajanAse);

        PelaajaYlos();
        LuoPallo();
        LuoKentta();
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");

        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Up, ButtonState.Down, PelaajaYlos, "PelaajaMeneeYlos");
        Keyboard.Listen(Key.Down, ButtonState.Down, PelaajaAlas, "PelaajaMeneeAlas");
        Keyboard.Listen(Key.Left, ButtonState.Down, PelaajaVasemmalle, "PelaajaMeneevasemmalle");
        Keyboard.Listen(Key.Right, ButtonState.Down, PelaajaOikealle, "PelaajaMeneeOikealle");
        Keyboard.Listen(Key.Space, ButtonState.Down, AmmuAseella, "ammu");

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

            Tormaaja.Destroy();
            PelaajanAse.Destroy();

        }
    }
}