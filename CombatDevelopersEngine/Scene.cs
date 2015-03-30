using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;


namespace CombatDevelopersEngine
{
    public class Scene
    {
        private Camera camera;
        private Node rootNode;
       
        public Scene(Camera camera)
        {            
            if (camera != null)
            {
                this.camera = camera;
                rootNode = new Node("rootNode");
                rootNode.Camera = this.camera;                             
            }
            else
                throw new Exception("The camera cannot be null");            
        }

        public Camera Camera
        {
            get { return camera; }
            set 
            {
                if (value == null)
                {
                    throw new Exception("The camera cannot be null");
                }
                camera = value;
                rootNode.Camera = camera;
            }
        }
        public Node RootNode
        {
            get { return rootNode; }
        }

       

        public void Draw()
        {
            rootNode.StartDrawing();
            //Aquí se debe llamar al Draw de los particle systems, lights, etc
        }

        
    }
}
