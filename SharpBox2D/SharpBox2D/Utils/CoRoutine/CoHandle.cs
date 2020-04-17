namespace SharpBox2D
{
    using System.Collections.Generic;

    internal class CoHandle : ICoHandle
    {
        #region Public Variables

        public uint      Id        { get; }
        public CoSegment CoSegment { get; }
        public CoTag     CoTag     { get; }

        public bool IsAlive => _CoState != CoState.End;

        public bool IsPaused { get; set; }

        public bool InnerCreatedAndMoved { get; set; }

        #endregion Public Variables

        #region Public Methods

        internal CoHandle(IEnumerator<float> enumerator, CoRoutine coRoutine, CoSegment coSegment, CoTag coTag,
            uint coId)
        {
            Id             = coId;
            CoSegment      = coSegment;
            CoTag          = coTag;
            _SecondsToWait = 0f;
            _CoState       = CoState.Running;
            _Enumerator    = enumerator;
            _CoRoutine     = coRoutine;
        }

        #region Equals

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((CoHandle) obj);
        }

        public override int GetHashCode()
        {
            return (int) Id;
        }

        public static bool operator ==(CoHandle left, CoHandle right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CoHandle left, CoHandle right)
        {
            return !Equals(left, right);
        }

        #endregion Equals

        #region Main Methods

        public void Kill()
        {
            _CoState = CoState.End;
        }

        public void MoveCoRoutine(float deltaTime)
        {
            if (IsPaused)
                return;

            switch (_CoState)
            {
                case CoState.WaitingForSeconds:
                {
                    _SecondsToWait -= deltaTime;

                    if (_SecondsToWait > 0f)
                        break;

                    _CoState = CoState.Running;
                    MoveCoRoutine(0f);
                    break;
                }

                case CoState.WaitingForAnotherCoRoutineToFinish:
                {
                    if (_WaitForThisCoRoutineToEnd != null && _WaitForThisCoRoutineToEnd.IsAlive)
                        break;

                    _CoState = CoState.Running;
                    MoveCoRoutine(0f);
                    break;
                }

                case CoState.WaitingForTrue:
                {
                    if (_Condition != null && !_Condition())
                        break;

                    _CoState = CoState.Running;
                    MoveCoRoutine(0f);
                    break;
                }

                case CoState.WaitingForFalse:
                {
                    if (_Condition != null && _Condition())
                        break;

                    _CoState = CoState.Running;
                    MoveCoRoutine(0f);
                    break;
                }

                case CoState.Running:
                {
                    if (!_Enumerator.MoveNext())
                    {
                        _CoState = CoState.End;
                        break;
                    }

                    SetNextState(_Enumerator.Current, deltaTime);
                    break;
                }
            }

            if (_CoState == CoState.End)
                _CoRoutine.RemoveFromAllCoRoutines(Id);
        }

        #endregion Main Methods

        #endregion Public Methods

        #region Private Types

        private enum CoState
        {
            End,
            Running,
            WaitingForSeconds,
            WaitingForAnotherCoRoutineToFinish,
            WaitingForTrue,
            WaitingForFalse
        }

        #endregion Private Types

        #region Private Variables

        private          CoRoutine.UntilConditionCallback _Condition;
        private          CoHandle                         _WaitForThisCoRoutineToEnd;
        private          float                            _SecondsToWait;
        private          CoState                          _CoState;
        private readonly CoRoutine                        _CoRoutine;
        private readonly IEnumerator<float>               _Enumerator;

        #endregion Private Variables

        #region Private Methods

        #region Moving CoRoutine

        private void SetNextState(float value, float deltaTime)
        {
            if (MathUtils.IsFloatZero(value))
                return;

            if (value > 0f)
            {
                _CoState       = CoState.WaitingForSeconds;
                _SecondsToWait = value;
                return;
            }

            if (!_CoRoutine.OrdersByCoRoutines.ContainsKey(value))
                return;

            object orderObject = _CoRoutine.OrdersByCoRoutines[value];
            _CoRoutine.OrdersByCoRoutines.Remove(value);

            if (orderObject is CoHandle coHandle)
            {
                _CoState                   = CoState.WaitingForAnotherCoRoutineToFinish;
                _WaitForThisCoRoutineToEnd = coHandle;
                if (CoSegment == coHandle.CoSegment)
                {
                    coHandle.InnerCreatedAndMoved = true;
                    coHandle.MoveCoRoutine(deltaTime);
                }

                return;
            }

            if (orderObject is CoRoutine.UntilConditionCallback function)
            {
                _CoState   = MathUtils.IsFloatZero(value % 2f) ? CoState.WaitingForTrue : CoState.WaitingForFalse;
                _Condition = function;
                return;
            }

            if (orderObject is CoRoutine.GetCoHandleCallback action)
            {
                action.Invoke(this);
            }
        }

        #endregion Moving CoRoutine

        #region Equals

        private bool Equals(CoHandle other)
        {
            return Id == other.Id;
        }

        #endregion Equals

        #endregion Private Methods
    }
}