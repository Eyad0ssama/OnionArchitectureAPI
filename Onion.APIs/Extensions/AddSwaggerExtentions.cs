namespace Onion.APIs.Extensions
{
    public static class AddSwaggerExtentions
    {
        public static WebApplication UseSwaggerMiddlwares(this WebApplication app)
        {
            
            
                app.UseSwagger();
                app.UseSwaggerUI();
            
            return app;
        }
    }
}
