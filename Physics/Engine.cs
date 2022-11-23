using rso.Base;
using rso.gameutil;
using System;
using System.Collections.Generic;

namespace rso.physics
{
    public abstract class CEngine
    {
        protected CTick _CurTick = null;
        public Int64 Tick { get; private set; } = 0; // ���� ó���� ���� ������ Tick
        public static Single ContactOffset { get; private set; }
        public static Int32 FPS { get; private set; }
        public static Int64 UnitTick { get; private set; }
        public static Single DeltaTime { get; private set; }

        public CList<CCollider2D> Objects { get; private set; } = new CList<CCollider2D>();
        public CList<CMovingObject2D> MovingObjects { get; private set; } = new CList<CMovingObject2D>();
        public List<CPlayerObject2D> Players { get; private set; } = new List<CPlayerObject2D>();

        public CEngine(Int64 CurTick_, Single ContactOffset_, Int32 FPS_)
        {
            _CurTick = new CTick(CurTick_);
            Tick = CurTick_;
            ContactOffset = ContactOffset_;
            FPS = FPS_;
            UnitTick = 10000000 / FPS;
            DeltaTime = 1.0f / FPS;
        }
        public abstract void Update();
        protected void _Update(Int64 ToTick_)
        {
            for (; Tick < ToTick_; Tick += UnitTick)
            {
                foreach (var i in MovingObjects)
                    i.fFixedUpdate?.Invoke(Tick);

                // �ܺο��� Velocity�� ������ ��� ���� �ݿ��ϱ� ���� �浹 ó�� ���� �÷��̾��� fixedUpdate ó��
                // �׷��� ������ ��ź�� �浹 �Ͽ� �ӵ��� �����Ǿ����
                foreach (var p in Players)
                    p.fFixedUpdate?.Invoke(Tick);

                for (Int32 pi = 0; pi < Players.Count - 1; ++pi)
                {
                    for (Int32 ti = pi + 1; ti < Players.Count; ++ti)
                        Players[pi].CheckOverlapped(Tick, Players[ti]);
                }

                foreach (var p in Players)
                {
                    for (var it = MovingObjects.Begin(); it;)
                    {
                        if (p.CheckOverlapped(Tick, it.Data))
                        {
                            var itCheck = it;
                            it.MoveNext();
                            RemoveMovingObject(itCheck);
                        }
                        else
                        {
                            it.MoveNext();
                        }
                    }

                    for (var it = Objects.Begin(); it;)
                    {
                        if (p.CheckOverlapped(Tick, it.Data))
                        {
                            var itCheck = it;
                            it.MoveNext();
                            RemoveObject(itCheck);
                        }
                        else
                        {
                            it.MoveNext();
                        }
                    }
                }

                fFixedUpdate?.Invoke();
            }
        }
        public CList<CCollider2D>.SIterator AddObject(CCollider2D Object_)
        {
            return Objects.Add(Object_);
        }
        public void RemoveObject(CList<CCollider2D>.SIterator Iterator_)
        {
            foreach (var i in Players)
                i.NotOverlapped(Tick, Iterator_.Data);

            Objects.Remove(Iterator_);
        }
        public CList<CMovingObject2D>.SIterator AddMovingObject(CMovingObject2D Object_)
        {
            return MovingObjects.Add(Object_);
        }
        public void RemoveMovingObject(CList<CMovingObject2D>.SIterator Iterator_)
        {
            foreach (var i in Players)
                foreach (var c in Iterator_.Data.Colliders)
                    i.NotOverlapped(Tick, c);

            MovingObjects.Remove(Iterator_);
        }

        // RemovePlayer �� �߰����� ����
        // �ܺο��� ����� Collision �ݹ��� ȣ���ϴµ� �ű⿡�� RemovePlayer �� ��� OtherPlayer�� ���� CollisionCallback�� ȣ�� �� �� ����
        public void AddPlayer(CPlayerObject2D Player_) // Player �� ������ �� ���� (�����Ϸ��� Collision, Trigger �ݹ鿡�� �����Ϸ� �ϴ� ��� ��ȯ�� true �� �� ������ �Ұ���, ���� �����Ϸ��� ��� ����)
        {
            Players.Add(Player_);
        }
        public void Start()
        {
            _CurTick.Start();
        }
        public void Stop()
        {
            _CurTick.Stop();
        }
        public bool IsStarted()
        {
            return _CurTick.IsStarted();
        }
        public Action fFixedUpdate;
    }
}