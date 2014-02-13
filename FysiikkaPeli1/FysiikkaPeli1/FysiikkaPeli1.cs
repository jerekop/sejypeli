using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class FysiikkaPeli1 : PhysicsGame{
    Tank tankki1;
        Tank tankki2;
        Vector vauhti1;
        Vector vauhti2;

    public override void Begin()
    {
     LuoKentta ();

     LuoTankki1 ();
     LuoTankki2();
     AsetaOhjaimet();

     vauhti1 = new Vector(90, 0);
     vauhti2 = new Vector(-90, 0);
    
        // TODO: Kirjoita ohjelmakoodisi tähän
        Level.CreateBottomBorder();

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");


    }

        void LuoKentta ()
        {
            Level.CreateBottomBorder();
            Gravity = new Vector(0, -1500);

        }
         void LuoTankki1 ()
         {
            Tank tankki1 = new Tank(200, 70);
            Add(tankki1);
            tankki1.Y = -250;
            tankki1.X = 350;
            tankki1.Mass = 1000;
            tankki1.Cannon.Angle = Angle.FromDegrees(120);
             Add(tankki1);
         }
          void LuoTankki2 ()
          {
            Tank tankki2 = new Tank(200, 70);
            tankki2.Y = -250;
            tankki2.X = -350;
            tankki2.Mass = 1000;
            tankki2.Cannon.Angle = Angle.FromDegrees(120);
            Add(tankki2);
          }

          void LiikutaTankki2Vasen()
          {
              tankki2.Velocity = tankki2.Velocity + vauhti1;
          }

          void LiikutaTankki1Oikea()
          {
              tankki1.Velocity = tankki1.Velocity + vauhti1;
          }

          void Liikutatankki1Vasen()
          {
              tankki1.Velocity = tankki1.Velocity + vauhti2;
          }

          void LiikutaTankki2Oikea()
          {
              tankki2.Velocity = tankki2.Velocity + vauhti2;
          }
          void AsetaOhjaimet()
          {
              Keyboard.Listen(Key.Escape, ButtonState.Pressed, Exit, "Poistu");

              Keyboard.Listen(Key.Left, ButtonState.Down, LiikutaTankki2Vasen, null);
              Keyboard.Listen(Key.Right, ButtonState.Down, LiikutaTankki2Oikea, null);

              Keyboard.Listen(Key.A, ButtonState.Down, LiikutaTankki1Vasen, null);
              Keyboard.Listen(Key.S, ButtonState.Down, LiikutaTankki1Oikea, null);

              Keyboard.Listen(Key.LeftShift, ButtonState.Down, keulitankki1, null);

          }
          void keulitankki1()
          {
              tankki1.ApplyTorque(200000000);
          }
    }

