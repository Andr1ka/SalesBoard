using LanguageExt.Common;

namespace WebApi.Extentions
{
    public static class ResultExtensions
    {
        public static IResult ToResponse<TResult>(this Result<TResult> result)
        {
            return result.Match(
                (value) =>
                {
                    return Results.Ok(value);
                },
                (exeption) =>
                {
                    return Results.BadRequest(exeption.Message);
                }
            );

        }
    }
}
