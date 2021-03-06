﻿using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class pong : PhysicsGame
{
    Vector nopeusYlos = new Vector(0, 200);
    Vector nopeusAlas = new Vector(0, -200);

    PhysicsObject pallo;
    PhysicsObject maila1;
    PhysicsObject maila2;

    PhysicsObject vasenreuna;
    PhysicsObject oikeareuna;

    IntMeter pelaajan1pisteet;
    IntMeter pelaajan2pisteet;

    public override void Begin()
    {
        // TODO: Kirjoita ohjelmakoodisi tähän

        Keyboard.Listen(Key.Escape, ButtonState.Released, Exit, "Lopeta peli");
        LuoKentta();
        AloitaPeli();
        aseteohjaimet();
        LisääLaskurit();

    }
    void LuoKentta()
    {
        pallo = new PhysicsObject(40.0, 40.0); Add(pallo);
        pallo.Shape = Shape.Circle;
        pallo.X = -200.00;
        pallo.Y = 0.0;
        pallo.Restitution = 1.0;
        Add(pallo);

        vasenreuna = Level.CreateLeftBorder();
        vasenreuna.Restitution = 1.0;
        vasenreuna.KineticFriction = 0.0;
        vasenreuna.IsVisible = false;

        oikeareuna = Level.CreateLeftBorder();
        oikeareuna.Restitution = 1.0;
        oikeareuna.KineticFriction = 0.0;
        oikeareuna.IsVisible = false;
        Level.Background.Color = Color.Black;

        AddCollisionHandler(pallo, KasittelePallonTormays);


        maila1 = LuoMaila(Level.Left + 20.0, 0.0);
        maila2 = LuoMaila(Level.Right - 20.0, 0.0);

        Level.CreateBorders(1.0, false);
        Level.Background.Color = Color.Black;


        Camera.ZoomToLevel();

    }
    void AloitaPeli()
    {
        Vector impulssi = new Vector(500.0, 0.0);
        pallo.Hit(impulssi);
    }
    PhysicsObject LuoMaila(double x, double y)
    {
        PhysicsObject maila = PhysicsObject.CreateStaticObject(20.0, 100.0);
        maila.Shape = Shape.Rectangle;
        maila.X = x;
        maila.Y = y;
        maila.Restitution = 1.0;
        Add(maila);
        return maila;


    }

    void aseteohjaimet()
    {
        Keyboard.Listen(Key.A, ButtonState.Down, AsetaNopeus, "pelaaja 1: liikuta mailaa ylös", maila1, nopeusYlos);

        Keyboard.Listen(Key.A, ButtonState.Released, AsetaNopeus, null, maila1, Vector.Zero);
        Keyboard.Listen(Key.Z, ButtonState.Down, AsetaNopeus, "Pelaaja 1: Liikuta mailaa alas", maila1, nopeusAlas);
        Keyboard.Listen(Key.Z, ButtonState.Released, AsetaNopeus, null, maila1, Vector.Zero);

        Keyboard.Listen(Key.Up, ButtonState.Down, AsetaNopeus, "pelaaja 2: liikuta mailaa ylös", maila2, nopeusYlos);
        Keyboard.Listen(Key.Up, ButtonState.Released, AsetaNopeus, null, maila2, Vector.Zero);
        Keyboard.Listen(Key.Down, ButtonState.Down, AsetaNopeus, "pelaaja 2: Liikuta mailaa alas", maila2, nopeusAlas);
        Keyboard.Listen(Key.Down, ButtonState.Released, AsetaNopeus, null, maila2, Vector.Zero);

        Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, "Näytä ohjeet");

        Keyboard.Listen(Key.Escape, ButtonState.Released, Exit, "Lopeta peli");

    }

    void AsetaNopeus(PhysicsObject maila, Vector nopeus)
    {
        if ((nopeus.Y > 0) && (maila.Top > Level.Top))
        {
            maila.Velocity = Vector.Zero;
            return;
        }
        if ((nopeus.Y > 0) && (maila.Top > Level.Top))
        {
            maila.Velocity = Vector.Zero;
            return;
        }


        maila.Velocity = nopeus;
    }

    void LisääLaskurit()
    {
        pelaajan1pisteet = LuoPisteLaskuri(Screen.Left + 100.0, Screen.Top - 100.0);
        pelaajan2pisteet = LuoPisteLaskuri(Screen.Right - 100.0, Screen.Top - 100.0);
    }


    IntMeter LuoPisteLaskuri(double x, double y)
    {
        IntMeter laskuri = new IntMeter(0);
        laskuri.MaxValue = 10;

        Label naytto = new Label();
        naytto.BindTo(laskuri);
        naytto.X = x;
        naytto.Y = y;
        naytto.TextColor = Color.White;
        naytto.BorderColor = Level.Background.Color;
        naytto.Color = Level.Background.Color;
        Add(naytto);

        return laskuri;
    }
    void KasittelePallonTormays(PhysicsObject pallo, PhysicsObject kohde)
    {
        if (kohde == oikeareuna)
        {
            pelaajan1pisteet.Value += 1;
        }
        else if (kohde == vasenreuna)
        {
            pelaajan2pisteet.Value += 1;
        }
    }
}

