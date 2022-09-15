namespace BookStoreApp.Api.Controllers;

public static class Extensions
{
    public static ActionResult ServerError(this ControllerBase controller, string? message = null)
    {
        // message = message ?? "There was an error completing your request. Please try again later.";
        message ??= "There was an error completing your request. Please try again later.";
        return controller.StatusCode(500, message);
    }
}
