namespace Luxor.Security
{
    public enum ReferrerPolicy
    {
        None,
        NoReferrer,
        NoReferrerWhenDowngrade,
        SameOrigin,
        Origin,
        StrictOrigin,
        OriginWhenCrossOrigin,
        StrictOriginWhenCrossOrigin,
        UnsafeURL
    }
}