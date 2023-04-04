using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDBApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Service db
string db = builder.Configuration.GetConnectionString("MyDB");
builder.Services.AddDbContext<BootcampContext>(o => o.UseSqlServer(db));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Api
app.MapGet("/product", (BootcampContext context) =>
{
    return context.Products;
});

// Create
app.MapPost("/product", (BootcampContext context, Product p) =>
{
    // Insert
    context.Products.Add(p);
    context.SaveChanges();

    return p;
});


// Detail
app.MapGet("/product/{id}", (BootcampContext context, int id) =>
{
    var product = context.Products.Where(o => o.Id == id).FirstOrDefault();
    return product;
});


// Edit
app.MapPost("/product/{id}", (BootcampContext context, int id, [FromBody] Product p) =>
{
    var product = context.Products.Where(o => o.Id == id).FirstOrDefault();
    if (product!=null)
    {
        //Update
        if (!string.IsNullOrEmpty(p.Name))
            product.Name = p.Name;
        product.Price = p.Price;
        product.Stock = p.Stock;

        context.Products.Update(product);
        context.SaveChanges();
    }
    
    return product;
});

app.MapDelete("/product/{id}", (BootcampContext context, int id) =>
{
    var product = context.Products.Where(o => o.Id == id).FirstOrDefault();
    if(product != null)
    {
        context.Products.Remove(product);
        context.SaveChanges();
    }
    return product;
});

app.Run();