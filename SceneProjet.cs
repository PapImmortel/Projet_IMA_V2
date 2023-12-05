using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_IMA
{
    class SceneProjet
    {
        private List<Lampe> listeLampe;
        private List<Item> listItem;
        private V3 positionCamera;
        private Couleur couleurAmbiant;

        public SceneProjet(V3 positionCamera)
        {
            this.listeLampe = new List<Lampe>() { };
            this.listItem = new List<Item>() { };
            this.positionCamera = positionCamera;
            this.couleurAmbiant = new Couleur(Couleur.White) * 0.1f;
        }

        public List<Lampe> getListeLampe()
        {
                return this.listeLampe;        
        } 

        public void addLampe(Lampe pLampe) 
        { 
            this.listeLampe.Add(pLampe); 
        }

        public List<Item> getListItem()
        {
            return this.listItem;
        }

        public void addListeItem(Item pItem)
        {
            this.listItem.Add(pItem);
        }

        public V3 PositionCamera { get { return this.positionCamera; } set { this.positionCamera = value; } }
        public Couleur CouleurAmbiante { get { return this.couleurAmbiant; } set { this.couleurAmbiant = value; } }

    }
}




