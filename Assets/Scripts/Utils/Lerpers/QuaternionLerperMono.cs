using UnityEngine;

namespace Numetry.Tools.Lerper
{
    public class QuaternionLerperMono : AbstractLerperMono<Quaternion>
    {
        #region PRIVATE_FIELDS
        private QuaternionLerper lerper = null;
        #endregion

        #region PROPERTIES
        public QuaternionLerper Lerper
        {
            get
            {
                if (lerper == null)
                {
                    lerper = new QuaternionLerper(lerpTime, smoothType);
                }

                return lerper;
            }
        }
        #endregion

        #region UNITY_CALLS
        protected override void Update()
        {
            Lerper.Update();
        }
        #endregion

        #region OVERRIDE
        public override Quaternion GetValue()
        {
            return Lerper.CurrentValue;
        }

        public override bool HasReached()
        {
            return Lerper.Reached;
        }

        public override bool IsOn()
        {
            return Lerper.On;
        }

        public override void SetValues(Quaternion start, Quaternion end, float time, bool on = false)
        {
            Lerper.SetValues(start, end, time, on);
        }
        #endregion
    }
}