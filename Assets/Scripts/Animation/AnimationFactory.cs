public static class AnimationFactory
{
    public static AnimationBehaviour MakeAnimation(AnimationType animationType)
    {
        switch (animationType)
        {
            case AnimationType.Shake:
                return new Shake();
            case AnimationType.FadeIn:
                return new FadeIn();
            case AnimationType.FadeOut:
                return new FadeOut();
            case AnimationType.ScaleIn:
                return new ScaleIn();
            case AnimationType.ScaleOut:
                return new ScaleOut();
            default:
                return null;
        }
    }
}