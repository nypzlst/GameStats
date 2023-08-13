namespace Stats.LifeTime
{
    public class CounterMiddleware
    {
        public RequestDelegate next;
        int i = 0;
        public CounterMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context,ICounter icounter, CounterService counterService)
        {
            i++;
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync($"Request {i}, Counter : {icounter.Value}, Service : {counterService.counter.Value} ");
        }
    }
}
