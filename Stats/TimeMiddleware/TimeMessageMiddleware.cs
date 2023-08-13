namespace Stats.TimeMiddleware
{
    public class TimeMessageMiddleware
    {
        RequestDelegate next;

        public TimeMessageMiddleware(RequestDelegate next)
        {
            this.next = next; 
        }
        public async Task InvokeAsync(HttpContext context, ITime itime)
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync($"<h1> Time is {itime?.GetTime()}");
        }
    }
}
