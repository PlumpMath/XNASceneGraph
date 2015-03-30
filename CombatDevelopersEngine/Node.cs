using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CombatDevelopersEngine
{
    public class Node:Spatial
    {
        
        private List<Spatial> children;
        
        public Node(string name):base(name)
        {            
            children = null;
            
        } 

        /// <summary>
        /// Adds a child to this node. If the child already has a parent, it is removed from that parent
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(Spatial child)
        {
            if (child != null)
            {
                if (child.GetParent() != this)
                {
                    if (child.GetParent() != null)
                    {
                        child.GetParent().RemoveChild(child);
                    }
                    child.SetParent(this);
                    if (children == null)
                    {
                        children = new List<Spatial>(1);
                    }                    
                    child.Load();
                    children.Add(child);
                }                
            }
        }

        /// <summary>
        /// Removes the given child from the node's list. This child will no longer be maintained
        /// </summary>
        /// <param name="child"></param>
        public void RemoveChild(Spatial child)
        {
            if (children != null)
            {
                if (child != null)
                {
                    if (child.GetParent() == this)
                    {
                        if(children.Remove(child))
                            child.SetParent(null);
                    }
                }
            }
        }

        /// <summary>
        /// Gets a child from the children's list at the given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Spatial GetChildAt(int index)
        {
            try
            {
                if (children != null)
                    return children.ElementAt<Spatial>(index);
            }
            catch (Exception exc) { }            
            return null;
        }

        /// <summary>
        /// Gets the size of the children's list
        /// </summary>
        /// <returns></returns>
        public int GetChildrenLength()
        {
            if(children!=null)
                return children.Count;
            return -1;
        }

        public void StartDrawing()
        {
            StartDrawing(this);
        }

        /// <summary>
        /// Starts drawing all the nodes in the scene graph in a recursive way
        /// </summary>
        /// <param name="spatial"></param>
        private void StartDrawing(Spatial spatial)
        {
            spatial.Draw();
            if(spatial is Node)        
            {
                Node node = (Node)spatial;
                if (node.children != null)
                {
                    foreach (Spatial spatialChild in node.children)
                    {
                        if(spatialChild.Camera==null)
                            spatialChild.Camera = node.Camera;
                        StartDrawing(spatialChild);
                    }
                }
            }
            
        }

        protected override void SetSpatialPosition()
        {
            UpdateChildrenTransformation(this);
        }
        
        protected override void SetSpatialRotation()
        {
            UpdateChildrenTransformation(this);
        }

        protected override void SetSpatialScale()
        {
            UpdateChildrenTransformation(this);
        }
        
        /// <summary>
        /// Updates the transformation of the children when the parent has been moved, scaled or rotated
        /// </summary>
        /// <param name="spatial"></param>
        protected void UpdateChildrenTransformation(Spatial spatial)
        {
            if (spatial != this)
            {
                spatial.ComputeTransformation();
            }
            if (spatial is Node)
            {
                Node node = (Node)spatial;
                if (node.children != null)
                {
                    foreach (Spatial spatialChild in node.children)
                    {
                        UpdateChildrenTransformation(spatialChild);
                    }
                }
            }
        }

        internal override void Load()
        {
        }
        internal override void Draw()
        {           
        }
        
    }
}
