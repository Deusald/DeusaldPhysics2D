namespace SharpBox2D
{
    using System;
    using System.Collections.Generic;

    public class Timing
    {
        #region Public Api
        
        public float WaitForOneFrame() => _CoRoutine.WaitForOneFrame();

        public ICoHandle EmptyHandle => _CoRoutine.EmptyCoHandle;
        
        public float WaitForSeconds(float seconds) => _CoRoutine.WaitForSeconds(seconds);
        
        public ICoHandle RunCoRoutine(IEnumerator<float> newCoRoutine,
            CoSegment segment = CoSegment.Normal,
            CoTag coTag = default)
        {
            return _CoRoutine.RunCoRoutine(newCoRoutine, segment, coTag);
        }
        
        public float WaitUntilDone(IEnumerator<float> newCoRoutine,
            CoSegment segment = CoSegment.Normal, CoTag coTag = default)
        {
            return _CoRoutine.WaitUntilDone(newCoRoutine, segment, coTag);
        }
        
        public float WaitUntilDone(ICoHandle coHandle)
        {
            return _CoRoutine.WaitUntilDone(coHandle);
        }
        
        public float GetMyCoHandle(CoRoutine.GetCoHandleCallback returner)
        {
            return _CoRoutine.GetMyCoHandle(returner);
        }
        
        public float WaitUntilTrue(CoRoutine.UntilConditionCallback condition)
        {
            return _CoRoutine.WaitUntilTrue(condition);
        }
        
        public float WaitUntilFalse(CoRoutine.UntilConditionCallback condition)
        {
            return _CoRoutine.WaitUntilFalse(condition);
        }
        
        public void KillCoRoutines(CoTag tag)
        {
            _CoRoutine.KillCoRoutines(tag);
        }
        
        public void PauseCoRoutines(CoTag tag)
        {
            _CoRoutine.PauseCoRoutines(tag);
        }
        
        public void ResumeCoRoutines(CoTag tag)
        {
            _CoRoutine.ResumeCoRoutines(tag);
        }

        #endregion Public Api

        public Timing(CoRoutine coRoutine)
        {
            _CoRoutine = coRoutine;
        }

        private readonly CoRoutine _CoRoutine;
    }
}