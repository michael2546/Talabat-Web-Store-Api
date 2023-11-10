﻿namespace Talabat.APIs.Extentions
{
    public static class AddSwaggerExtension
    {
        public static WebApplication UseSwaggerMiddlewares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
