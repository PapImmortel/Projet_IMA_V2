using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_IMA
{
    class SceneProjet
    {
        private List<Lampe> listLight;

        private V3 positionCamera;

        private Couleur couleurAmbiant = new Couleur(Couleur.White);
        
        private List<Item> listItem;


        public SceneProjet(V3 pPositionCamera)
        {
            this.listLight = new List<Lampe>() { };
            this.positionCamera = pPositionCamera;
            this.couleurAmbiant = new Couleur(Couleur.White) * 0.1f;
            this.listItem = new List<Item>() { };

        }
        public List<Lampe> getListLampe()
        {
                return this.listLight;        
        } 
        public void addListLight(Lampe pLampe) 
        { 
            this.listLight.Add(pLampe); 
        }

        public List<Item> getListItem()
        {
            return this.listItem;
        }
        public void addListItem(Item pItem)
        {
            this.listItem.Add(pItem);
        }
        public V3 modifPositionCamera { get { return this.positionCamera; } set { this.positionCamera = value; } }
        public Couleur modifCouleurAmbiante { get { return this.couleurAmbiant; } set { this.couleurAmbiant = value; } }

    }
}




