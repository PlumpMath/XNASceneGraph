using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatDevelopersEngine
{
    public class Camera
    {
        private string name;

        private Vector3 position;
        private Vector3 direction;
        private Vector3 up;

        private float fieldOfView;
        private float aspectRatio;
        private float nearPlane;
        private float farPlane;

        private Matrix projection;
        private Matrix view;
        
        public Camera(string name)
        {
            this.name = name;
            position = Vector3.Zero;
            direction = Vector3.Zero;
            up = Vector3.Up;                        
        }

        public string Name        
        {
            get { return name; }
            set { name = value; }
        }

        public Vector3 Position
        {
            get { return position; }           
        }
        public Vector3 Direction
        {
            get { return direction; }            
        }
        public Vector3 Up
        {
            get { return up; }
        }
        public float FieldOfView
        {
            get { return fieldOfView; }
        }
        public float AspectRatio
        {
            get { return aspectRatio; }
        }
        public float NearPlane
        {
            get { return nearPlane; }
        }
        public float FarPlane
        {
            get { return farPlane; }
        }
        public Matrix Projection
        {
            get { return projection; }
        }
        public Matrix View
        {
            get { return view; }
        }

        public void SetFrustrumPerspective(float fieldOfView, float aspectRatio, float nearPlane, float farPlane)
        {
            this.fieldOfView = fieldOfView;
            this.aspectRatio = aspectRatio;
            this.nearPlane = nearPlane;
            this.farPlane = farPlane;            
            projection = Matrix.CreatePerspectiveFieldOfView(this.fieldOfView, this.aspectRatio, this.nearPlane, this.farPlane);
        }

        public void LookAt(Vector3 lookAt, Vector3 up)
        {            
            this.direction=lookAt;
            this.up=up;
            view = Matrix.CreateLookAt(this.position, this.direction, this.up);            
        }

        public void SetLocation(Vector3 position)
        {
            this.position = position;
            view = Matrix.CreateLookAt(this.position, this.direction, this.up);   
        }

        

    }
}
