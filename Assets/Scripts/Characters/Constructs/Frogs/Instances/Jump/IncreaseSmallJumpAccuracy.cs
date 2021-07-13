using Utils;

namespace Frogs.Instances.Jumps
{
    public class IncreaseSmallJumpAccuracy : IJump01Modifier
    {
        public readonly float minJump01 = .15f;
        public readonly float smallJumpThreshhold01 = 0.3f;

        public IncreaseSmallJumpAccuracy(float minJump01, float smallJumpThreshhold01)
        {
            Normalise.ThrowExceptionIfNotNormal01(minJump01);
            Normalise.ThrowExceptionIfNotNormal01(smallJumpThreshhold01);

            this.minJump01 = minJump01;
            this.smallJumpThreshhold01 = smallJumpThreshhold01;
        }

        public float Modify(float jump01)
        {
            Normalise.ThrowExceptionIfNotNormal01(jump01);

            bool minimumJump = jump01 < smallJumpThreshhold01;
            if (minimumJump) jump01 = minJump01;

            return jump01;
        }
    }
}
