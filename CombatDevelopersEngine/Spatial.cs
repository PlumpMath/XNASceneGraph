using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CombatDevelopersEngine
{
    public abstract class Spatial
    {
        //The name of the spatial
        protected string name;
        //A reference to the parent of the spatial. Note: the parent of the root node is always null
        protected Node parent;
        //The camera to display the content
        protected Camera camera;
        //Indicates if the spatial is visible or not
        protected bool visible;
        //This is the matrix that allows to do any scalation, rotation, translation to the spatial
        protected Matrix transformation;       
        //Allows to do any scalation to the spatial
        protected Matrix scaleMatrix;
        //Allows to do any rotation to the spatial
        protected Matrix rotationMatrix;
        //Allows to do any translation to the spatial
        protected Matrix positionMatrix;
        //The absolute position of the spatial
        protected Vector3 globalPosition;
        //The local position of the spatial
        protected Vector3 localPosition;
        //The absolute rotation of the spatial
        protected Quaternion globalRotation;
        //The local rotation of the spatial
        protected Quaternion localRotation;
        //The absolute scalation of the spatial
        protected Vector3 globalScale;
        //The local scalation of the spatial
        protected Vector3 localScale;
        

        public Spatial(string name)
        {
            this.name = name;
            parent = null;
            camera = null;
            visible = true;

            localPosition = Vector3.Zero;            
            localScale = Vector3.One;            
            localRotation = Quaternion.Identity;

            Matrix.CreateScale(ref localScale, out scaleMatrix);
            Matrix.CreateFromQuaternion(ref localRotation, out rotationMatrix);
            Matrix.CreateTranslation(ref localPosition, out positionMatrix);            

            ComputeTransformation();
        }

        /// <summary>
        /// Gets the name of the spatial
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        
        /// <summary>
        /// Gets or sets the camera
        /// </summary>
        protected internal Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        /// <summary>
        /// Gets or sets if the spatial is visible or not
        /// </summary>
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        /// <summary>
        /// Gets the absolute scalation of the spatial
        /// </summary>
        /// <returns></returns>
        public Vector3 GetGlobalScale()
        {
            return globalScale;
        }
        /// <summary>
        /// Gets the local scalation of the spatial
        /// </summary>
        /// <returns></returns>
        public Vector3 GetLocalScale()
        {
            return localScale;
        }
                
        /// <summary>
        /// Sets the local scalation of the spatial with the given vector 
        /// </summary>
        /// <param name="localScale"></param>
        public void SetLocalScale(Vector3 localScale)
        {

            this.localScale = localScale;
            SetLocalScale();
            SetSpatialScale();
            
        }

        /// <summary>
        /// Sets the local scalation of the spatial with the given x,y,z 
        /// </summary>
        /// <param name="localScale"></param>
        public void SetLocalScale(float x, float y, float z)
        {

            localScale.X = x;
            localScale.Y = y;
            localScale.Z = z;
            SetLocalScale();
            SetSpatialScale();

        }

        /// <summary>
        /// Sets the local scalation of the spatial in a uniform way with the given factor
        /// </summary>
        /// <param name="localScale"></param>
        public void SetLocalScale(float scaleFactor)
        {

            localScale = Vector3.One * scaleFactor;
            SetLocalScale();
            SetSpatialScale();
        }


        /// <summary>
        /// Calculates the scale matrix of the spatial
        /// </summary>
        private void SetLocalScale()
        {
            Matrix.CreateScale(ref this.localScale, out scaleMatrix);
            ComputeTransformation();            
        }

        /// <summary>
        /// Gets the absolute rotation of the spatial
        /// </summary>
        /// <returns></returns>
        public Quaternion GetGlobalRotation()
        {
            return globalRotation;
        }
        
        /// <summary>
        /// Gets the local rotation of the spatial
        /// </summary>
        /// <returns></returns>
        public Quaternion GetLocalRotation()
        {
            return localRotation;
        }
        
        /// <summary>
        /// Sets the local rotation of the spatial with the given Quaternion
        /// </summary>
        /// <param name="localRotation"></param>
        public void SetLocalRotation(Quaternion localRotation)
        {
            this.localRotation = localRotation;
            SetLocalRotation();
            SetSpatialRotation();         
        }

        /// <summary>
        /// Calculates the rotation matrix of the spatial
        /// </summary>
        private void SetLocalRotation()
        {
            Matrix.CreateFromQuaternion(ref this.localRotation, out rotationMatrix);
            ComputeTransformation();
        }

        /// <summary>
        /// Gets the absolute position of the spatial
        /// </summary>
        /// <returns></returns>
        public Vector3 GetGlobalPosition()
        {
            return globalPosition;
        }
        
        /// <summary>
        /// Gets the local position of the spatial
        /// </summary>
        /// <returns></returns>
        public Vector3 GetLocalPosition()
        {
            return localPosition;
        }    
        
        /// <summary>
        /// Sets the local position of the spatial with the given vector
        /// </summary>
        /// <param name="localPosition"></param>
        public void SetLocalPosition(Vector3 localPosition)
        {
            this.localPosition = localPosition;
            SetLocalPosition();
            SetSpatialPosition();
        }

        /// <summary>
        /// Sets the local position of the spatial with the given x,y,z
        /// </summary>
        /// <param name="localPosition"></param>
        public void SetLocalPosition(float x, float y, float z)
        {
            localPosition.X = x;
            localPosition.Y = y;
            localPosition.Z = z;
            SetLocalPosition();
            SetSpatialPosition();
        }

        /// <summary>
        /// Calculates the position matrix of the spatial
        /// </summary>
        private void SetLocalPosition()
        {
            Matrix.CreateTranslation(ref this.localPosition, out positionMatrix);
            ComputeTransformation();
        }
        


        /// <summary>
        /// Gets the parent of the spatial
        /// </summary>
        public Node GetParent()
        {
            return parent;
        }
        /// <summary>
        /// Sets the parent of the spatial
        /// </summary>
        /// <param name="parent"></param>
        internal void SetParent(Node parent)
        {
            if (parent != null)
            {
                this.parent = parent;                                               
                ComputeTransformation();
            }
        }
      
            
        /// <summary>
        /// Calculates the transformation of the spatial in order to apply all its translations, rotations and scalations,
        /// including also the parent's transformation. The global transformations are also calculated
        /// </summary>
        internal void ComputeTransformation()
        {            
            if (parent != null)
                transformation = scaleMatrix * rotationMatrix * positionMatrix * parent.transformation;
            else
                transformation = scaleMatrix * rotationMatrix * positionMatrix;
                       
            transformation.Decompose(out globalScale, out globalRotation, out globalPosition);
        }

        abstract protected void SetSpatialPosition();
        abstract protected void SetSpatialRotation();
        abstract protected void SetSpatialScale();
                
        abstract internal void Load();
        abstract internal void Draw();               
    }
}
