﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NC.Common.Exceptions;
using NC.Common.Response;
using Newtonsoft.Json;

namespace NC.Common.Middleware
{
    /// <summary>
    /// 异常处理中间件
    /// </summary>
    public class ExceptionMiddleware
    {
        /// <summary>
        /// 日志工具
        /// </summary>
        private readonly ILogger<ExceptionMiddleware> logger;

        /// <summary>
        /// 处理 HTTP 请求委托
        /// </summary>
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> _logger)
        {
            this.logger = _logger;
            this.next = next;
        }

        /// <summary>
        /// 实现请求委托
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
                //await next.Invoke(context);
                //var features = context.Features;
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (exception is CustomNotFoundException) code = HttpStatusCode.NotFound;
            else if (exception is CustomUnauthorizedException) code = HttpStatusCode.Unauthorized;
            else if (exception is CustomException) code = HttpStatusCode.BadRequest;

            var msg = $"{exception.Message}{Environment.NewLine}{exception.InnerException}{exception.StackTrace}";

            // 日志记录
            logger.LogError(msg);

            // 自定义异常响应内容
            var result = JsonConvert.SerializeObject(new ApiResponse(msg));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);

            //context.Response.StatusCode = 500;
            //context.Response.ContentType = "text/json;charset=utf-8;";
            //string error = "";

            //void ReadException(Exception ex)
            //{
            //    error += string.Format("{0} | {1} | {2}", ex.Message, ex.StackTrace, ex.InnerException);
            //    if (ex.InnerException != null)
            //    {
            //        ReadException(ex.InnerException);
            //    }
            //}

            //ReadException(e);
            //if (environment.IsDevelopment())
            //{
            //    var json = new { message = e.Message, detail = error };
            //    error = JsonConvert.SerializeObject(json);
            //}
            //else
            //    error = "抱歉，出错了";

            //await context.Response.WriteAsync(error);
        }
    }
}
