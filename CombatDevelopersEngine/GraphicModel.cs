using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CombatDevelopersEngine
{
    public class GraphicModel:Spatial
    {

       
        protected Model model;
        protected Matrix[] absoluteBoneTransforms;           
        protected ContentManager content;
        protected string asset;
       
        public GraphicModel(string name):base(name)        
        {             
            model = null;        
            absoluteBoneTransforms = null;
            content = null;
            asset = string.Empty;
            
        }
               
        /// <summary>
        /// Gets the model
        /// </summary>
        public Model Model
        {
            get { return model; }
        }
        /// <summary>
        /// Gets the absolute bone transforms of the model
        /// </summary>
        public Matrix[] AbsoluteBoneTransforms
        {
            get { return absoluteBoneTransforms; }
        }

        /// <summary>
        /// Gets or sets the content where the model is located
        /// </summary>
        public ContentManager Content
        {
            get { return content; }
            set { content = value; }
        }
        /// <summary>
        /// Gets or sets the asset path where the model is located
        /// </summary>
        public string Asset
        {
            get { return asset; }
            set { asset = value; }
        }
       
        internal override void Load()
        {
            model = content.Load<Model>(asset);
            absoluteBoneTransforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(absoluteBoneTransforms);            
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                }
            }
        }

        internal override void Draw()
        {
            if (visible)
            {
                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.World = absoluteBoneTransforms[mesh.ParentBone.Index] * transformation;
                        effect.Projection = camera.Projection;
                        effect.View = camera.View;
                    }
                    mesh.Draw();
                }
            }
        }

        protected override void SetSpatialPosition()
        {                 
        }
        protected override void SetSpatialRotation()
        {
        }
        protected override void SetSpatialScale()
        {
        }
        
    }
}
