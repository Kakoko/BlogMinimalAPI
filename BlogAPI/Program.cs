var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Our Datastore

var blogs = new List<Blog>()
{
    new Blog(){ Id = 1, Title = "Intro to Minimal API" , Content
    = "We are going to Implement minimal API"},
    new Blog(){ Id = 2, Title = "Intro to API" , Content
    = "We are going to Implement API's with Controllers"}
};


// Get ALl Blogs

app.MapGet("api/blog" , () => Results.Ok(blogs));

//Get A blog by Id
app.MapGet("api/blog/{id}", (int id) =>
{
    var BlogPost = blogs.FirstOrDefault(b => b.Id == id);

    if(BlogPost == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(BlogPost);
});

//Create a blog
app.MapPost("api/blog", (Blog blog) =>
{
    blogs.Add(blog);

    return Results.Ok();
});

//Update a blog

app.MapPut("api/blog", (Blog blog) =>
{
    var BlogPost = blogs.FirstOrDefault(p => p.Id == blog.Id);
    if(BlogPost == null)
    {
        return Results.NotFound();
    }

    BlogPost.Title = blog.Title;
    BlogPost.Content = blog.Content;

    return Results.Ok();
});

//Delete a blog
app.MapDelete("api/blog", (int id) =>
{
    var blogPost = blogs.FirstOrDefault(p => p.Id == id);
    if(blogPost == null)
    {
        return Results.NotFound();
    }

    blogs.Remove(blogPost);

    return Results.Ok();
});
app.Run();


class Blog
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}