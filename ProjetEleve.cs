using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace Projet_IMA
{
    static class ProjetEleve
    {
        public static void Go()
        {

            //////////////////////////////////////////////////////////////////////////
            ///
            ///     Sphère en 3D
            /// 
            //////////////////////////////////////////////////////////////////////////

            SceneProjet notreScene = new SceneProjet(new V3(BitmapEcran.GetWidth() / 2, -BitmapEcran.GetWidth(), BitmapEcran.GetHeight() / 2));
            /*float positionCamera3DX = BitmapEcran.GetWidth() / 2;
            float positionCamera3DY = -BitmapEcran.GetWidth();
            float positionCamera3DZ = BitmapEcran.GetHeight() / 2;

            V3 positionCamera3D = new V3(positionCamera3DX, positionCamera3DY, positionCamera3DZ);*/
            //Couleur CLampe = Couleur.White;
            //V3 lampePrincipaleFluxDir3D = new V3(1, -1, 1) / IMA.Sqrtf(3);
            //V3 lFill3D = new V3(-1, -1, 1) / IMA.Sqrtf(3);

            Lampe lampePrincipaleFluxDir3D = new Lampe(0.7f, new V3(1, -1, 1) / IMA.Sqrtf(3));
            Lampe lFill3D = new Lampe(0.3f, new V3(-1, -1, 1) / IMA.Sqrtf(3));

            notreScene.addListLight(lampePrincipaleFluxDir3D);
            notreScene.addListLight(lFill3D);
            //List<V3> listLight = new List<V3>();
            //listLight.Add(lampePrincipaleFluxDir3D);
            //listLight.Add(lFill3D);
            /*Couleur couleurAmbiant = new Couleur(Couleur.White);
            couleurAmbiant = couleurAmbiant * 0.1f;*/
            int kSpecularPower = 100;
            float kBumpinessSphere = 0.02f;
            float pas = 0.005f;
            



            //texture
            V3 CentreSphere = new V3(300, 200, 300);
            float vRayon = 150;
            Texture sphereTexture = new Texture("gold.jpg");
            Texture sphereBumpiness = new Texture("bump38.jpg");
            Sphere notreSphere = new Sphere(CentreSphere,sphereTexture, sphereBumpiness,vRayon);
            for (float u = 0 ; u < 2 * IMA.PI; u += pas)  // echantillonage fnt paramétrique
            {
                for (float v = -IMA.PI / 2; v < IMA.PI / 2; v += pas)
                {


                    /*// calcul des coordoonées dans la scène 3D
                    float nX3D = IMA.Cosf(v) * IMA.Cosf(u);
                    float nY3D = IMA.Cosf(v) * IMA.Sinf(u);
                    float nZ3D = IMA.Sinf(v);
                    float x3D = Rayon * nX3D + CentreSphere.x;
                    float y3D = Rayon * nY3D + CentreSphere.y;
                    float z3D = Rayon * nZ3D + CentreSphere.z;
                    V3 point3D = new V3(x3D, y3D, z3D);
                    V3 n3D = new V3(nX3D, nY3D, nZ3D);

                    // projection orthographique => repère écran

                    float uNorm = u / (2 * IMA.PI);
                    float vNorm = (v / IMA.PI) + 0.5f;
                    Couleur CSphere = notreSphere.sphereTexture.LireCouleur(uNorm, vNorm);
                    
                    
                    //bumpmapping
                    sphereBumpiness.Bump(uNorm, vNorm, out float dhdu, out float dhdv);
                    //T2 = dhdu*(N^DM/Dv)  T3=dhdv*(DM/Du^N)


                    V3 dmdu = new V3(  (IMA.Cosf(vNorm)  * (-IMA.Sinf(uNorm))),  IMA.Cosf(uNorm) * IMA.Cosf(vNorm), 0);
                    V3 dmdv = new V3(  (-IMA.Sinf(vNorm) * IMA.Cosf(uNorm)),   (-IMA.Sinf(vNorm) * IMA.Sinf(uNorm)),  IMA.Cosf(vNorm));

                    V3 nBump3D = n3D + (kBumpinessSphere * (dhdu * V3.prod_vect(n3D, dmdv) + (dhdv * V3.prod_vect(dmdu, n3D))));
                    n3D = nBump3D;
                    n3D.Normalize();
                    V3 d3D = positionCamera3D - point3D;
                    d3D.Normalize();*/
                    V3[] nosVariables = notreSphere.dessinVariable(u,v,kBumpinessSphere,notreScene.modifPositionCamera);
/*                    Couleur couleurFinale = new Couleur(0, 0, 0);
*/
                    Couleur couleurFinale = Illumination.éclairageObjet(notreScene.getListLampe(),notreSphere.getCouleurText(u,v), nosVariables[0], nosVariables[1], kSpecularPower);
                    /*foreach (V3 lampe in listLight)
                    {
                        // diffus
                        float prodScalNL = V3.prod_scal(n3D, lampe);
                        float cosAlpha = prodScalNL / (lampe.Norm() * n3D.Norm());
                        if (prodScalNL >= 0)
                            {
                                couleurFinale += (CSphere * CLampe) * prodScalNL; // ajout diffus
                            }

                        //spec
                        V3 r3D = 2 * cosAlpha * n3D - lampe;
                        float prodScalRD = V3.prod_scal(r3D, d3D);

                        

                        if (prodScalNL >= 0)
                        {
                            couleurFinale += CLampe * (float)Math.Pow(prodScalRD, kSpecularPower);//ajout spéculaire
                        }

                    }*/

                    /*int x_ecran = (int)(x3D);
                    int y_ecran = (int)(z3D);*/

                    int x_ecran = (int)nosVariables[2].x;
                    int y_ecran = (int)nosVariables[2].z;

                    BitmapEcran.DrawPixel(x_ecran, y_ecran, couleurFinale + notreScene.modifCouleurAmbiante);
                }

            }


            //////////////////////////////////////////////////////////////////////////
            ///
            ///     Rectangle 3D  + exemple texture
            /// 
            //////////////////////////////////////////////////////////////////////////


            

            int kSpecularPowerRect = 100;
            float kBumpinessRect = 0.008f;


            V3 Origine = new V3(500, 200, 300);
            V3 Coté1 = new V3(300, 000, 000);
            V3 Coté2 = new V3(000, 200, 000);
            //texture

            Texture rectangleTexture = new Texture("gold.jpg");
            Texture rectangleBumpiness = new Texture("bump38.jpg");

            Pavé notreRectangle = new Pavé(Origine,Coté1,Coté2,rectangleTexture,rectangleBumpiness);

            pas = 0.002f;
            for (float u = 0; u < 1; u += pas)  // echantillonage fnt paramétrique
            {
                for (float v = 0; v < 1; v += pas)
                {
                    /*V3 P3D = Origine + u * Coté1 + v * Coté2;

                    // projection orthographique => repère écran    

                    Couleur CRect = rectangleTexture.LireCouleur((float)u, (float)v);


                    V3 n3D = new V3(0, 0, 60000);
                    rectangleBumpiness.Bump(u, v, out float dhdu, out float dhdv);



                    V3 dmdu = new V3(Coté1.x, Coté1.y, Coté1.z);
                    V3 dmdv = new V3(Coté2.x, Coté2.y, Coté2.z);

                    V3 nBump3D = n3D + (kBumpinessRec * (dhdu * V3.prod_vect(n3D, dmdv) + (dhdv * V3.prod_vect(dmdu, n3D))));

                    n3D = nBump3D;
                    n3D.Normalize();
                    Couleur couleurFinale = new Couleur(0, 0, 0);
                    V3 d3D = new V3(positionCamera3D.x - P3D.x, positionCamera3D.y - P3D.y, positionCamera3D.z - P3D.z);
                    d3D.Normalize();*/
                    V3[] nosVariables = notreRectangle.dessinVariable(u, v, kBumpinessRect, notreScene.modifPositionCamera);

                    Couleur couleurFinale = Illumination.éclairageObjet(notreScene.getListLampe(), notreSphere.getCouleurText(u, v), nosVariables[0], nosVariables[1], kSpecularPowerRect);


                    /*foreach (V3 lampe in listLight)
                    {
                        float prodScalNL = V3.prod_scal(n3D, lampe);
                        float cosAlpha = prodScalNL / (lampe.Norm() * n3D.Norm());
                        V3 r3D = 2 * cosAlpha * n3D - lampe;
                        float prodScalRD = V3.prod_scal(r3D, d3D);

                        if (prodScalNL >= 0)
                        {
                            couleurFinale += (CRect * CLampe) * prodScalNL; // ajout diffus
                        }

                        if (prodScalRD >= 0)
                        {
                            couleurFinale += CLampe * (float)Math.Pow(prodScalRD, kSpecularPowerRect);//ajout spéculaire
                        }
                    }*/
                    /*int x_ecran = (int)(P3D.x);
                    int y_ecran = (int)(P3D.y);*/
                    int x_ecran = (int)(nosVariables[2].x);
                    int y_ecran = (int)(nosVariables[2].y);
                    BitmapEcran.DrawPixel(x_ecran, y_ecran, couleurFinale + notreScene.modifCouleurAmbiante);


                }

            }
            // Gestion des textures
            // Texture T1 = new Texture("brick01.jpg");
            // Couleur c = T1.LireCouleur(u, v);

        }
    }
}
