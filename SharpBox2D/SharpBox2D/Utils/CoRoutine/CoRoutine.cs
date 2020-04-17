// MIT License

// SharpBox2D:
// Copyright (c) 2020 Adam "Deusald" Orliński

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace SharpBox2D
{
    using System;
    using System.Collections.Generic;

    public class CoRoutine
    {
        #region Public Types

        public delegate bool UntilConditionCallback();

        public delegate void GetCoHandleCallback(ICoHandle coHandle);

        #endregion Public Types

        #region Public Variables

        public readonly   ICoHandle                 EmptyCoHandle;
        internal readonly Dictionary<float, object> OrdersByCoRoutines;

        #endregion Public Variables

        #region Public Methods

        public CoRoutine()
        {
            _NextCoId                 = 1;
            _NextOrderId              = -1;
            _CurrentExecutedHandle    = null;
            OrdersByCoRoutines        = new Dictionary<float, object>(100);
            _AllCoRoutines            = new Dictionary<uint, CoHandle>(100);
            _NormalCoRoutines         = new LinkedList<CoHandle>();
            _PhysicsCoRoutines        = new LinkedList<CoHandle>();
            _UnscaledNormalCoRoutines = new LinkedList<CoHandle>();

            CoHandle emptyCoHandle = new CoHandle(null, this, CoSegment.Normal, new CoTag(0), 0);
            emptyCoHandle.Kill();
            EmptyCoHandle = emptyCoHandle;
        }

        public void Update(float deltaTime)
        {
            MoveCoRoutines(_NormalCoRoutines.First, deltaTime);
        }

        public void UpdateUnscaled(float unscaledDeltaTime)
        {
            MoveCoRoutines(_UnscaledNormalCoRoutines.First, unscaledDeltaTime);
        }

        public void PhysicsUpdate(float fixedDeltaTime)
        {
            MoveCoRoutines(_PhysicsCoRoutines.First, fixedDeltaTime);
        }

        public void Reset()
        {
            foreach (var coHandle in _AllCoRoutines.Values)
                coHandle.Kill();

            _NextOrderId           = -1;
            _CurrentExecutedHandle = null;
            OrdersByCoRoutines.Clear();
            _AllCoRoutines.Clear();
            _NormalCoRoutines.Clear();
            _PhysicsCoRoutines.Clear();
            _UnscaledNormalCoRoutines.Clear();
        }

        internal void RemoveFromAllCoRoutines(uint id)
        {
            _AllCoRoutines.Remove(id);
        }

        #region Enumerator Api

        public ICoHandle RunCoRoutine(IEnumerator<float> newCoRoutine, CoSegment segment = CoSegment.Normal,
            CoTag coTag = default)
        {
            CoHandle coHandle = new CoHandle(newCoRoutine, this, segment, coTag, GetNextCoId);
            AddNewCoHandle(coHandle);
            return coHandle;
        }

        public float WaitForOneFrame()
        {
            return 0f;
        }

        public float WaitForSeconds(float seconds)
        {
            return seconds;
        }

        public float WaitUntilDone(IEnumerator<float> newCoRoutine, CoSegment segment = CoSegment.Normal,
            CoTag coTag = default)
        {
            CoHandle innerCoHandle = new CoHandle(newCoRoutine, this, segment, coTag, GetNextCoId);
            AddNewCoHandle(innerCoHandle);

            float id = GetNextOrderId;
            OrdersByCoRoutines[id] = innerCoHandle;
            return id;
        }

        public float WaitUntilDone(ICoHandle coHandle)
        {
            if (!_AllCoRoutines.ContainsKey(coHandle.Id))
                return 0f;

            CoHandle innerCoHandle = _AllCoRoutines[coHandle.Id];
            float    id            = GetNextOrderId;
            OrdersByCoRoutines[id] = innerCoHandle;
            return id;
        }

        public float GetMyCoHandle(GetCoHandleCallback returner)
        {
            float id = GetNextOrderId;
            OrdersByCoRoutines[id] = returner;
            return id;
        }

        public float WaitUntilTrue(UntilConditionCallback condition)
        {
            float id = GetNextOrderId;

            if (!MathUtils.IsFloatZero(id % 2f))
                id = GetNextOrderId;

            OrdersByCoRoutines[id] = condition;
            return id;
        }

        public float WaitUntilFalse(UntilConditionCallback condition)
        {
            float id = GetNextOrderId;

            if (MathUtils.IsFloatZero(id % 2f))
                id = GetNextOrderId;

            OrdersByCoRoutines[id] = condition;
            return id;
        }

        public void KillCoRoutines(CoTag tag)
        {
            foreach (var coRoutine in _AllCoRoutines.Values)
            {
                if (coRoutine.CoTag != tag)
                    continue;

                coRoutine.Kill();
            }
        }

        public void PauseCoRoutines(CoTag tag)
        {
            foreach (var coRoutine in _AllCoRoutines.Values)
            {
                if (coRoutine.CoTag != tag)
                    continue;

                coRoutine.IsPaused = true;
            }
        }

        public void ResumeCoRoutines(CoTag tag)
        {
            foreach (var coRoutine in _AllCoRoutines.Values)
            {
                if (coRoutine.CoTag != tag)
                    continue;

                coRoutine.IsPaused = false;
            }
        }

        #endregion Enumerator Api

        #endregion Public Methods

        #region Private Variables

        private uint                     _NextCoId;
        private float                    _NextOrderId;
        private LinkedListNode<CoHandle> _CurrentExecutedHandle;

        private uint  GetNextCoId    => _NextCoId++;
        private float GetNextOrderId => _NextOrderId--;

        private readonly Dictionary<uint, CoHandle> _AllCoRoutines;
        private readonly LinkedList<CoHandle>       _NormalCoRoutines;
        private readonly LinkedList<CoHandle>       _PhysicsCoRoutines;
        private readonly LinkedList<CoHandle>       _UnscaledNormalCoRoutines;

        #endregion Private Variables

        #region Private Methods

        private void MoveCoRoutines(LinkedListNode<CoHandle> first, float deltaTime)
        {
            for (LinkedListNode<CoHandle> node = first; node != null;)
            {
                _CurrentExecutedHandle = node;

                if (!node.Value.InnerCreatedAndMoved)
                    node.Value.MoveCoRoutine(deltaTime);

                LinkedListNode<CoHandle> next = node.Next;
                node.Value.InnerCreatedAndMoved = false;

                if (!node.Value.IsAlive)
                    node.List?.Remove(node);

                node = next;
            }

            _CurrentExecutedHandle = null;
        }

        private void AddNewCoHandle(CoHandle coHandle)
        {
            if (_CurrentExecutedHandle == null)
            {
                switch (coHandle.CoSegment)
                {
                    case CoSegment.Normal:
                    {
                        _NormalCoRoutines.AddLast(coHandle);
                        break;
                    }
                    case CoSegment.Physics:
                    {
                        _PhysicsCoRoutines.AddLast(coHandle);
                        break;
                    }
                    case CoSegment.UnscaledNormal:
                    {
                        _UnscaledNormalCoRoutines.AddLast(coHandle);
                        break;
                    }
                }
            }
            else
                _CurrentExecutedHandle = _CurrentExecutedHandle.List?.AddAfter(_CurrentExecutedHandle, coHandle);

            _AllCoRoutines[coHandle.Id] = coHandle;
        }

        #endregion Private Methods
    }
}