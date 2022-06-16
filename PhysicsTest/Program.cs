using System;
using rso.physics;
using UnityEngine;

namespace PhysicsTest
{
    class Program
    {
        static void Main(String[] args)
        {
            var Begin = new SPoint(0, 0);
            var End = new SPoint(10, 5);

            while (!CPhysics.MoveTowards(Begin, End, 0.5f))
            {
                Console.WriteLine(Begin.X + " " + Begin.Y);
            }
            Console.WriteLine(Begin.X + " " + Begin.Y);

            return;




            var MyCenter = new SPoint(0.7059453529691696f, 0.95074967547869216f);
            var TargetCenter = new SPoint(0.69332838119407825f, 0.78701901149100739f);

            var Vel0 = new SPoint(0.19000000000000197f, -0.38262210845947314f);
            var Theta0 = Math.Atan2(Vel0.Y, Vel0.X);

            // My Scalar
            var Scalar = Vel0.GetScalar();

            // Get Me To Target Vector
            var LinkVector = TargetCenter.GetSub(MyCenter);

            // 나의 속도벡터를 나와 대상을 연결한 직선과 나란한 성분A와 A와 수직이 B를 구하고
            // A, B의 X,Y 성분을 구하고, B-A 를 구하면 이것이 BalloonFight 의 충돌후의 내 Vector

            var LinkTheta = CPhysics.ThetaOfTwoVectors(LinkVector, Vel0);
            var ScalarA = Mathf.Abs(Mathf.Cos(LinkTheta) * Scalar);
            var ThetaA = Mathf.Atan2(LinkVector.Y, LinkVector.X);
            var VecA = new SPoint(Mathf.Cos(ThetaA) * ScalarA, Mathf.Sin(ThetaA) * ScalarA);

            var ScalarB = Mathf.Abs(Mathf.Sin(LinkTheta) * Scalar);
            var ThetaB = ThetaA - (Mathf.PI * 0.5f);
            var VecB = new SPoint(Mathf.Cos(ThetaB) * ScalarB, Mathf.Sin(ThetaB) * ScalarB);

            var BouncedVec = VecB.GetSub(VecA);
            
            Console.WriteLine(VecA.X + " " + VecA.Y);
            Console.WriteLine(VecB.X + " " + VecB.Y);
            Console.WriteLine(BouncedVec.X + " " + BouncedVec.Y);

            return;

            if (CPhysics.IsOverlappedRectRect(new SRect(0.0f, 1.0f, 0.0f, 1.0f), new SRect(1.0f, 3.0f, 0.0f, 1.0f)))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");

            return;

            var CollisionInfo = new SCollisionInfo();
            if (CPhysics.IsCollidedRectRect2(new SRect(0.0f, 2.0f, 0.0f, 1.0f), new SRect(1.0f, 3.0f, 0.0f, 1.0f), new SPoint(1.0f, 0.0f), new SPoint(0.0f, 0.0f), CollisionInfo))
                Console.WriteLine("true" + CollisionInfo.Time);
            else
                Console.WriteLine("false" + CollisionInfo.Time);

            SPoint p = new SPoint(0.00000001f, 0.0f);
            SLine l = new SLine(new SPoint(0.0f, 1.0f), new SPoint(2.0f, 1.0f));
            var o = CPhysics.SymmetryPoint(p, l);
            Console.WriteLine(o.X + " " + o.Y);
        }
    }
}
