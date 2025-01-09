using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Validations;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Map("/super", () => "Hello world");
app.UseRouting();
//Group all controller in one map


app.UseEndpoints(endpoints =>
{
    endpoints.Map("/Map1/{vinod}", async (context) =>
    {
        string v = Convert.ToString(context.Request.RouteValues["vinod"]);
        await context.Response.WriteAsync($"vinod routing{v}") ; 
    });

    endpoints.Map("/", async (context) =>
    {
     
        await context.Response.WriteAsync($"vinod routing");
    });
    endpoints.Map("/Map2/{t}", async (context) =>
    {
        string? store = Convert.ToString(context.Request.RouteValues["t"]);
        await context.Response.WriteAsync($"vinod routing2 {store}");
    });
    endpoints.Map("/Timer/{t:datetime}", async (context) =>
    {
        DateTime? store = Convert.ToDateTime(context.Request.RouteValues["t"]);
        await context.Response.WriteAsync($"vinod date {store}");
    });
    endpoints.MapFallback(async (context) =>
    {
        await context.Response.WriteAsync($"Http request not found {context.Request.Path}");
    });

});








app.Run(
    async (HttpContext context) =>
    {
        StreamReader forBody = new StreamReader(context.Request.Body);
        string body = await forBody.ReadToEndAsync();

        Dictionary<string,StringValues> qdic = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);
        if (qdic.ContainsKey("name"))
        {
            var id = qdic["name"][0];
            await context.Response.WriteAsync($"<p>{id}</p>");
        }

        var i = context.Request.Query["id"];
        context.Response.Headers["hey"] = "bye";
        context.Response.Headers["Date"] ="u23o87te";
        context.Response.Cookies.Append("vinod","raj");
        await context.Response.WriteAsync($"<p>hi</p>");
    });      





app.Run();

////app.Run(async (HttpContext context) => {
////    context.Response.Headers["vinod"] = "raj";
////    if (context.Request.Query.ContainsKey("id"))
////    {
////        string id = context.Request.Query["id"];
////        string id1 = context.Request.Query["id1"];
////        string id2 = context.Request.Query["id2"];
////        await context.Response.WriteAsync($"<p>{id}</p>");
////        await context.Response.WriteAsync($"<p>{id1}</p>");
////    }
////    await context.Response.WriteAsync("i am vinod");

////}
////    );
///


//for single map to controller
//app.Map("/", () => "Hello, World!");
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//Simple way  to map all controller

//app.MapControllers();


//app.Use(async (context, next) =>
//{
//    Microsoft.AspNetCore.Http.Endpoint endpoint = context.GetEndpoint();
//    await context.Response.WriteAsync($"{endpoint}");
//    await next(context);
//});


//app.Use(async (context, next) =>
//{
//    Microsoft.AspNetCore.Http.Endpoint endpoint = context.GetEndpoint();
//    await context.Response.WriteAsync($"hey : {endpoint}");
//    await next(context);
//});
//app.MapControllers();

