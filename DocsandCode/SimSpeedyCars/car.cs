using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SpeedyCars
{
    public class Car
    {
        float Velocity = 0.0f; //Speed car in m/s
        float Acceleration = 0.0f; //Speed change in m/s2
        float DeAcceleration = 0.0f; //Speed change in m/s2
        float Length = 0.0f; //length car in m

        /// <summary>
        /// Car object.
        /// </summary>
        /// <param name="velocity"></param>
        /// Speed of the car in m/s
        /// <param name="acceleration"></param>
        /// Ability for the car to accelerate in m/s2
        /// <param name="deacceleration"></param>
        /// Ability for the car to deaccelerate in m/s2
        /// <param name="length"></param>
        /// How long the car is in m.
        public Car(float velocity, float acceleration, float deacceleration, float length)
        {
            Velocity = velocity;
            Acceleration = acceleration;
            DeAcceleration = deacceleration;
            Length = length;            
        }

        /// <summary>
        /// Updates the car's current velocity.
        /// </summary>
        /// <param name="time"></param>
        /// The amount of time the car is accelerating or deaccelerating for.
        /// <param name="accelerate"></param>
        /// Whether the car is accelerating, if bool = true then the velocity of the car increases.
        /// if bool = false then the velocity of the car decreases.
        public void updateVelocity(float time, bool accelerate)
        {
            if (accelerate)
            {
                Velocity = Velocity + Acceleration * time;
            }
            else if (!accelerate)
            {
                Velocity = Velocity - DeAcceleration * time;
            }
            else
            {
                throw new Exception("No true or false statement given for accelerate in car.updateVelocity.");
            }
        }

        /// <summary>
        /// Simple get current velocity of the car object.
        /// </summary>
        /// <returns></returns>
        public float getVelocity()
        {
            return Velocity;
        }
        
        /// <summary>
        /// Simple get current lenght of the car object.
        /// </summary>
        /// <returns></returns>
        public float getLength()
        {
            return Length;
        }
    }

    public class Road
    {
        float roadLength; //distance in m.
        List<float> stopLights; //distance from start in m.

        /// <summary>
        /// Road object.
        /// </summary>
        /// <param name="length"></param>
        /// How long the road is in m.
        /// <param name="lights"></param>
        /// A list of all the stoplights on this road, the number listed represents the distance in m from the start of the road to that stoplight.
        public Road(float length, List<float> lights)
        {
            roadLength = length;
            stopLights = lights;
        }

        /// <summary>
        /// Simple get stoplights list from the road object.
        /// </summary>
        /// <returns></returns>
        public List<float> getLights()
        {
            return stopLights;
        }

        /// <summary>
        /// Gives the distance to the next upcoming stoplight, using the current distance traveled on the road object.
        /// </summary>
        /// <param name="distance"></param>
        /// How far along the calling object is on the road.
        /// <returns></returns>
        /// Distance between object and the next stoplight it'll encounter on the road.
        public float getNextLightDistance(float distance)
        {
            Debug.WriteLine("getNextLightDistance called:");
            if(distance < roadLength)
            {
                int i = 0;
                float pick = stopLights[i];
                float result;
                foreach (float lightDistance in stopLights)
                {
                    if(lightDistance > distance)
                    {
                        pick = stopLights[i];
                        break;
                    }
                    i += 1;
                }
                result = pick - distance;
                return result;

            }
            else
            {
                throw new Exception("Input distance greater than length of the road.");
            }
        }
    }

    public static class Calculate
    {
        /// <summary>
        /// Calculates the effective length of a car object. By multiplying the velocity times how much distance in
        /// seconds each car is supposed to keep for their predecessor, plus their own car length.
        /// </summary>
        /// <param name="velocity"></param>
        /// The speed in m/s of the car.
        /// <param name="carLength"></param>
        /// The length of the car in m.
        /// <param name="distanceTime"></param>
        /// The time in seconds a car is to legally keep between themselves and the car in front of them.
        /// <returns></returns>
        /// The car's effective length in m.
        public static float CarLength(float velocity, float carLength, float distanceTime)
        {
            float result = velocity * distanceTime + carLength; // m/s * s + m = m
            return result;
        }
    }
}
