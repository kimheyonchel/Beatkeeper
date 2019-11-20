using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Utill
{
    #region # 베지어곡선
    public static Vector3 GetPointOnBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1f - t;
        float t2 = t * t;
        float u2 = u * u;
        float u3 = u2 * u;
        float t3 = t2 * t;

        Vector3 result =
            (u3) * p0 +
            (3f * u2 * t) * p1 +
            (3f * u * t2) * p2 +
            (t3) * p3;

        return result;
    }

    public static Vector3 GetPointOnBezierCurve(Vector3[] vec, float t)
    {
        float u = 1f - t;
        float t2 = t * t;
        float u2 = u * u;
        float u3 = u2 * u;
        float t3 = t2 * t;

        Vector3 result =
            (u3) * vec[0] +
            (3f * u2 * t) * vec[1]+
            (3f * u * t2) * vec[2] +
            (t3) * vec[3];

        return result;
    }
    #endregion

    #region # 포물선
    public struct LaunchData
    {
        public readonly Vector3 initiaVelocity;
        public readonly float timeToTarget;
        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initiaVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }

    public static LaunchData CalculateLaunchVelocity(Transform Target, Transform Ball, float Height, float Gravity)
    {
        float displacementY = Target.position.y - Ball.position.y;
        Vector3 displacementXZ = new Vector3(Target.position.x - Ball.transform.position.x, 0, Target.position.z - Ball.position.z);
        float time = Mathf.Sqrt(-2 * Height / Gravity) + Mathf.Sqrt(2 * (displacementY - Height) / Gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * Gravity * Height);
        Vector3 velocityXZ = displacementXZ / time;
        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(Gravity), time);
    }

    public static Vector3 CalculateLaunchVelocity(Vector3 currentPos, Vector3 targetPos, float initialAngle)
    {
        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 planarTarget = new Vector3(targetPos.x, 0, targetPos.z);
        Vector3 planarPosition = new Vector3(currentPos.x, 0, currentPos.z);

        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = currentPos.y - targetPos.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (targetPos.x > currentPos.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        return finalVelocity;
    }
    #endregion

    #region 점프
    [System.Serializable]
    public struct JumpRegionData
    {
        public float jumpSpeed;
        public float jumpHeight;
    }
    [System.Serializable]
    public struct JumpData
    {
        public Vector3 flyInitVec;
        public float flyDistance;
        public float flyTotalTime;
        public float flySpeed;
        public float flyDeltaTime;
        public float flyHeight;
        public bool  isFly;
        
        public JumpData(Vector3 flyInitVec, float flyDistance, float flyTotalTime,
            float flyDeltaTime, float flySpeed = 2, float flyHeight = 5, bool isFly = false)
        {
            this.flyInitVec = flyInitVec;
            this.flyDistance = flyDistance;
            this.flyTotalTime = flyTotalTime;
            this.flySpeed = flySpeed;
            this.flyDeltaTime = flyDeltaTime;
            this.flyHeight = flyHeight;
            this.isFly = isFly;
        }

        public float ResultTotalTime()
        {
            return this.flyDistance / this.flySpeed;
        }
        public static float ResultTotalTime(JumpData jump)
        {
            return jump.flyDistance / jump.flySpeed;
        }

    }
    #endregion
}