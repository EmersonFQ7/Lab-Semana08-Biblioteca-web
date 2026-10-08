using Biblioteca.Web.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------------
// Servicios MVC
// ---------------------------------------------------------------------
builder.Services.AddControllersWithViews();

// ---------------------------------------------------------------------
// Inyeccion de dependencias: Repositorios con Dapper
// Se registran como Scoped (una instancia por request HTTP)
// Lo exige el punto 7 del lab: "Registrarlas en Program.cs e inyectarlas
// por constructor. Los controladores no contienen SQL ni abren conexiones"
// ---------------------------------------------------------------------
builder.Services.AddScoped<LibroRepositorio>();
builder.Services.AddScoped<SocioRepositorio>();
builder.Services.AddScoped<AutorRepositorio>();
builder.Services.AddScoped<PrestamoRepositorio>();

var app = builder.Build();

// ---------------------------------------------------------------------
// Pipeline HTTP
// ---------------------------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

// Ruta por defecto: va al listado de libros al iniciar
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Libros}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();