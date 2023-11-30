using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Projet_IMA
{
    public struct Couleur
    {
        public float R, V, B;	// composantes R,V,B comprises entre 0 et 1

        /////////////////////          constantes            ///////////////////////////
        
        public static Couleur Red     = new Couleur(1, 0, 0);
        public static Couleur Green   = new Couleur(0, 1, 0);
        public static Couleur Blue    = new Couleur(0, 0, 1);
        public static Couleur Black   = new Couleur(0, 0, 0);
        public static Couleur White   = new Couleur(1, 1, 1);
        public static Couleur Cyan    = new Couleur(0, 1, 1);
        public static Couleur Magenta = new Couleur(1, 0, 1);
        public static Couleur Yellow  = new Couleur(1, 1, 0);
        public static Couleur Grotte = new Couleur((float)0.8, (float)0.6, (float)0.4);
        public static Couleur cercle = new Couleur((float)0.2, (float)0.5, (float)0.4);


        /////////////////////          constructeurs          //////////////////////////

        public Couleur(float R, float V, float B)
        {
            this.R = R;
            this.V = V;
            this.B = B;
        }

        public Couleur(Couleur c)
        {
            this.R = c.R;
            this.V = c.V;
            this.B = c.B;
        }

        
        /////////////////////          Conversions          //////////////////////////

        // convertion  0-1 <-> 0 255

        public void From255(byte RR, byte VV, byte BB)   // construit une couleur à partir des valeurs 0-255
        {
            R = (float)(RR / 255.0);
            V = (float)(VV / 255.0);
            B = (float)(BB / 255.0);
        }

        public void To255(out byte RR, out byte VV, out byte BB)
        {
            RR = (byte)(R * 255);
            VV = (byte)(V * 255);
            BB = (byte)(B * 255);
        }

        // convertion  0-1 <-> Windows Color 

        static public  Couleur FromWindowsColor(System.Drawing.Color c) 
        {
            Couleur cc = new Couleur();
            cc.R = (float) (c.R / 255.0);
            cc.V = (float) (c.G / 255.0);
            cc.B = (float) (c.B / 255.0);
            return cc;
        }


        public System.Drawing.Color ToWindowsColor()
        {
            Clamp();
            byte RR, VV, BB;
            To255(out RR, out VV, out BB);
            return Color.FromArgb(RR, VV, BB);
        }


        /////////////////////          MISC          //////////////////////////

        // force values to be <= 1
        public void Clamp()
        {
            if (R > 1.0) R = 1.0f;
            if (V > 1.0) V = 1.0f;
            if (B > 1.0) B = 1.0f;
        }


        // pour obtenir le niveau de gris équivalent
        // utile pour le Bump Map

        public float GreyLevel()						
        {
            return (R + B + V) / 3.0f;
        }

        /////////////////////          opérateurs surchargés          ////////////////////////// 

        // + -
        public static Couleur operator + (Couleur a, Couleur b)          {         return new Couleur(a.R + b.R, a.V + b.V, a.B + b.B);  }
        public static Couleur operator - (Couleur a, Couleur b)          {         return new Couleur(a.R - b.R, a.V - b.V, a.B - b.B);  }
        public static Couleur operator - (Couleur a)                     {         return new Couleur(-a.R, -a.V, -a.B);                 }

        // * membre à membre
        public static Couleur operator * (Couleur a, Couleur b)          {         return new Couleur(a.R * b.R, a.V * b.V, a.B * b.B);  }

        // couleur & constante
        public static Couleur operator * (float a, Couleur b)            {         return new Couleur(a * b.R, a * b.V, a * b.B);         }
        public static Couleur operator * (Couleur b, float a)            {         return new Couleur(a * b.R, a * b.V, a * b.B);         }
        public static Couleur operator / (Couleur b, float a)            {         return new Couleur(b.R / a, b.V / a, b.B / a);         }
    }
}



    

    				
