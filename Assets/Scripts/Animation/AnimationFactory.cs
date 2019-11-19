using System;

public static class AnimationFactory
{
    public static Type MakeAnimation(AnimationType animationType)
    {
        switch (animationType)
        {
            case AnimationType.Shake:
                return typeof(Shake);
            case AnimationType.ScaleIn:
                return typeof(ScaleIn);
            case AnimationType.ScaleOut:
                return typeof(ScaleOut);
            case AnimationType.MoveTopDown:
                return typeof(MoveAnimation);
            default:
                return null;
        }
    }
}