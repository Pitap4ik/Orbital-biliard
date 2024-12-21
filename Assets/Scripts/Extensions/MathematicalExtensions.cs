using System;
using UnityEngine;

namespace MathematicalExtensions{
    public static class Trigonometry
    {
        public static float GetDistance(Vector2 objectPosition){
            return MathF.Sqrt(MathF.Pow(objectPosition.x, 2) + MathF.Pow(objectPosition.y, 2));
        }

        public static float GetDistance2(Vector2 objectPosition, Vector2 pointPosition){
            return GetDistance(new Vector2(objectPosition.x - pointPosition.x, objectPosition.y - pointPosition.y));
        }

        public static float GetAngleToTargetByAtan(Vector2 objectPosition, Vector2 targetPosition){
            float oppositeQuotinent = (objectPosition.y - targetPosition.y);
            float adjacentQuotinent = (objectPosition.x - targetPosition.x);
            float angle = MathF.Atan(oppositeQuotinent / adjacentQuotinent);
            return angle;
        }

        public static float ConvertToDegrees(float angleRadians){
            return angleRadians / MathF.PI * 180f;
        }

        public static float GetAngleToTargetBySin(float oppositeQuotinent, float hypothenus){
            float angle = MathF.Asin(oppositeQuotinent / hypothenus);
            return angle;
        }

        public static float GetHypothenusBySin(float oppositeQuotinent, float angle){
            return oppositeQuotinent / MathF.Sin(angle);
        }

        public static float GetAngleToTargetByAtan2(Vector2 objectPosition, Vector2 targetPosition){
            float oppositeQuotinent = objectPosition.y - targetPosition.y;
            float adjacentQuotinent = objectPosition.x - targetPosition.x;
            float angle = MathF.Atan2(oppositeQuotinent, adjacentQuotinent);
            return angle;
        }
    }
}